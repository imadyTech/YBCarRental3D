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
    public class YB_APIManager<TData>: NebuEventUnityObjectBase
    {
        public void Geturl(string url)
        {
            StartCoroutine(PostRequest(url));
        }

        public void Add<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TData Get(int id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, TData> GetAll()
        {
            throw new NotImplementedException();
        }

        public void LoadAll()
        {
            throw new NotImplementedException();
        }

        public bool Update<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        public IEnumerator PostRequest(string url)
        {
            WWWForm form = new WWWForm();
            form.AddField("Content-Type", "application/json");

            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                yield return webRequest.SendWebRequest();
                if (!string.IsNullOrEmpty(webRequest.error))
                {
                    Debug.LogError(webRequest.error);
                }
                else
                {
                    //Debug.Log(webRequest.downloadHandler.text);
                }
            }
        }

    }

}
