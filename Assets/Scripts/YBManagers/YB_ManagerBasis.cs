using imady.NebuUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using imady.NebuEvent;

namespace YBCarRental3D
{
    public class YB_ManagerBasis<TData> : NebuEventInterfaceObjectBase
    {
        protected YB_APIContext apiContext;
        protected string baseUrl, controllerName;

        protected YB_ManagerBasis(string baseUrl, string controllerName)
        {
            this.baseUrl = baseUrl;
            this.controllerName = controllerName;
            apiContext = new YB_APIContext();
            this.apiContext.BaseApiUrl = $"{baseUrl}/{controllerName}";
        }

        public async Task<TData> Get(int id)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/{id}";
            var result = await apiContext.GetRequest(requestString);
            return JsonConvert.DeserializeObject<TData>(result);
        }

        public async Task<bool> Add(TData data)
        {
            var requestString = $"{baseUrl}/Add";
            var contentString = JsonConvert.SerializeObject(data);

            try
            {
                var result = await apiContext.PostRequest(requestString, contentString);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var requestString = $"{baseUrl}/Delete/{id}";
            try
            {
                var result = apiContext.DeleteRequest(requestString);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Update(TData data)
        {
            var requestString = $"{baseUrl}/Update";
            var contentString = JsonConvert.SerializeObject(data);

            try
            {
                var result = await apiContext.PostRequest(requestString, contentString);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}