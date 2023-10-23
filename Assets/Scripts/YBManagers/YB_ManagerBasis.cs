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

        protected async Task<TData> Get(int id)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/{id}";
            var result = await apiContext.GetRequest(requestString);
            return JsonConvert.DeserializeObject<TData>(result.Body);
        }

        protected async Task<bool> Add(TData data)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/add";
            var contentString = JsonConvert.SerializeObject(data);

            try
            {
                var result = await apiContext.PostRequest(requestString, contentString);
                return result.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected async Task<bool> Delete(int id)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/delete/{id}";
            try
            {
                var result = await apiContext.DeleteRequest(requestString);
                return result.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected async Task<bool> Update(TData data)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/update";
            var contentString = JsonConvert.SerializeObject(data);

            try
            {
                var result = await apiContext.PutRequest(requestString, contentString);
                return result.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected async Task<IEnumerable<TData>> GetList(int pageNum, int pageSize)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/list";
            var postDataString = JsonConvert.SerializeObject(new 
            {
                PageSize = pageSize,
                PageNum = pageNum
            });

            try
            {
                var result = await apiContext.PostRequest(requestString, postDataString);
                if (result.Success && !string.IsNullOrEmpty(result.Body))
                {
                    var list = JsonConvert.DeserializeObject<IEnumerable<TData>>(result.Body);
                    return list;
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}