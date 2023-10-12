using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using imady.NebuEvent;
using imady.NebuUI;

namespace YBCarRental3D
{
    public class YB_APIContext
    {
        private string baseApiUrl;

        public string BaseApiUrl
        {
            get => baseApiUrl;
            set => baseApiUrl = value;
        }

        public YB_APIContext()
        {

        }

        public async Task<string> GetRequest(string uri)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                www.timeout = 30;
                await www.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (www.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        return www.error;
                    case UnityWebRequest.Result.DataProcessingError:
                        return www.error;
                    case UnityWebRequest.Result.ProtocolError:
                        return www.error;
                    case UnityWebRequest.Result.Success:
                        return www.downloadHandler.text;
                    default:
                        return string.Empty;
                }
            }
        }

        public async Task<string> PostRequest(string uri, string postdata)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(uri, postdata, "application/json"))
            {
                www.timeout = 30;
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    return www.error;
                }
                else
                {
                    string jsonResponse = www.downloadHandler.text;
                    return jsonResponse;
                }
            }
        }

        public async Task<string> DeleteRequest(string uri)
        {
            using (UnityWebRequest www = UnityWebRequest.Delete(uri))
            {
                www.timeout = 30;
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    return www.error;
                }
                else
                {
                    return www.result.ToString();
                }
            }
        }

        public async Task<string> UpdateRequest(string uri, string postdata)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(uri, postdata, "application/json"))
            {
                www.timeout = 30;
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    return www.error;
                }
                else
                {
                    return www.result.ToString();
                }
            }
        }

        //public async Task<string> PutRequest(string request)
        //{
        //    byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        //    using (UnityWebRequest www = UnityWebRequest.Put("https://www.my-server.com/upload", myData))
        //    {
        //        yield return www.SendWebRequest();

        //        if (www.result != UnityWebRequest.Result.Success)
        //        {
        //            Debug.Log(www.error);
        //        }
        //        else
        //        {
        //            Debug.Log("Upload complete!");
        //        }
        //    }
        //}
    }

}
