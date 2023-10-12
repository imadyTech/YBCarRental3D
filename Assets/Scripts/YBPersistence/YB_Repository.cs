using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace YBCarRental3D
{
    public class YB_Repository
    {
        const string lineBreakConnector = "&";       //if a line in the repo file is ended with '&', then the next line will be concatenated.
        const string commentsSeparator = "#";        //for any line end with a '#' will be treated as comments, and won't be processed.
        string       repositoryFilePath = "";
        private Uri  repositoryUri;
        public bool  isReady = false;                //status indicator
        /// <summary>
        /// a TData record is serielized to a string and here caches all the records strings.
        /// Note: for simplicity reason, this data cache was designed to be exposed as public.
        /// </summary>
        public Dictionary<int, string> allRecordLines;

        public YB_Repository()
        {
            allRecordLines = new Dictionary<int, string>();
        }
        public YB_Repository(string url) : this()
        {
            this.repositoryFilePath = url;
            ReadAllLines();
        }

        public YB_Repository(Uri viewRepoUrl, Action afterLoad): this()
        {
            this.repositoryUri = viewRepoUrl;
            ReadAllLinesFromDownload(afterLoad);
        }

        ~YB_Repository()
        {
            //delete &allRecordLines;						//clear the allocated memory
        }

        void ReadAllLines()
        {
            //clear the cache
            if (allRecordLines?.Count > 0) allRecordLines.Clear();

            string line;
            StringBuilder cachedlineBuilder = new StringBuilder();                                                //temp variables.
            try
            {
                using (StreamReader sr = new StreamReader(repositoryFilePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        //Console.WriteLine(sr.ReadLine());
                        line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line) || line[^1..] == commentsSeparator)      //check if ended with a '#' or empty line
                            continue;
                        if (line[^1..] == lineBreakConnector)                                   //check if ended with a '&'
                        {
                            //line = line.Remove(line.Length - 1);
                            cachedlineBuilder.Append(line);
                            cachedlineBuilder.Length--;
                            continue;
                        }
                        else
                        {
                            cachedlineBuilder.Append(line);
                            //Last
                            try                                         //99.99% certainty will be the end of a def
                            {
                                int index = extractIndex(cachedlineBuilder.ToString());
                                if (index >= 0)
                                {
                                    // Add the line to the map using the index as the key
                                    allRecordLines[index] = cachedlineBuilder.ToString();
                                }
                                cachedlineBuilder.Clear();                                                //clear cache for next def
                                continue;
                            }
                            catch (Exception e)
                            {
                                //throw YB_DeSerializeError();
                                Debug.LogError("Error: " + e.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"The process failed: {e.Message}");
            }
            isReady = true;
        }

        async void ReadAllLinesFromDownload( Action afterLoad)
        {
            string fileContents;
            UnityWebRequest www = UnityWebRequest.Get(repositoryUri);
            www.timeout = 30;
            await www.SendWebRequest();

            if (www.result==UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error downloading file: " + www.error);
                return;
            }
            else
            {
                // File downloaded successfully
                fileContents = www.downloadHandler.text;
            }

            if (allRecordLines?.Count > 0) allRecordLines.Clear();

            string line;
            StringBuilder cachedlineBuilder = new StringBuilder();                                                //temp variables.
            try
            {

                using (StringReader sr = new StringReader(fileContents))
                {

                    while (sr.Peek()>=0)
                    {
                        //Console.WriteLine(sr.ReadLine());
                        line = sr.ReadLine();

                        if (string.IsNullOrEmpty(line) || line[^1..] == commentsSeparator)      //check if ended with a '#' or empty line
                            continue;
                        if (line[^1..] == lineBreakConnector)                                   //check if ended with a '&'
                        {
                            //line = line.Remove(line.Length - 1);
                            cachedlineBuilder.Append(line);
                            cachedlineBuilder.Length--;
                            continue;
                        }
                        else
                        {
                            cachedlineBuilder.Append(line);
                            //Last
                            try                                         //99.99% certainty will be the end of a def
                            {
                                int index = extractIndex(cachedlineBuilder.ToString());
                                if (index >= 0)
                                {
                                    // Add the line to the map using the index as the key
                                    allRecordLines[index] = cachedlineBuilder.ToString();
                                }
                                cachedlineBuilder.Clear();                                                //clear cache for next def
                                continue;
                            }
                            catch (Exception e)
                            {
                                //throw YB_DeSerializeError();
                                Debug.LogError("Error: " + e.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"The process failed: {e.Message}");
            }
            isReady = true;
            afterLoad();
        }

        void GetLine()
        {
            try
            {
                using (StreamReader sr = new StreamReader(repositoryFilePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        void AddLine(string line)
        {
            // Delete the file if it exists.
            if (File.Exists(repositoryFilePath))
            {
                File.Delete(repositoryFilePath);
            }

            //Create the file.
            using (FileStream fs = File.Create(repositoryFilePath))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());
                }
            }

            static void AddText(FileStream fs, string value)
            {
                byte[] info = new UTF8Encoding(true).GetBytes(value);
                fs.Write(info, 0, info.Length);
            }

        }

        int extractIndex(string idLine)
        {
            int index = -1;
            var pair = idLine.Split(':');
            string key = pair[0].Trim();
            string value = pair[1].Trim();
            try
            {
                int semicolonIndex = value.IndexOf(';');

                int.TryParse(value.Substring(0, semicolonIndex), out index);
            }
            catch (Exception e)
            {

            }

            return index;

        }
    }
}
