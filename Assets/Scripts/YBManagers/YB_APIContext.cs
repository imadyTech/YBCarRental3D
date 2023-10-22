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
using System.Net;

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

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string jsonResponse = www.downloadHandler.text;
                    return jsonResponse;
                }
                else
                {
                    return www.error;
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


        public async Task<string> PutRequest(string request, string postdata)
        {
            using (UnityWebRequest www = UnityWebRequest.Put(request, postdata))
            {
                www.SetRequestHeader("Content-Type", "application/json");

                www.timeout = 30;
                await www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    return www.result.ToString();
                }
                else
                {
                    return www.error;
                }
            }
        }
    }

}
