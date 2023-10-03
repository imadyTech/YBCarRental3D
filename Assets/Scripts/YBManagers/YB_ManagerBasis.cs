using System;
using System.Collections.Generic;

namespace YBCarRental3D
{
    public class YB_ManagerBasis<TData>
    {
        public YB_APIManager<TData> apiManager;

        public void LoadAll()
        {
            apiManager.LoadAll();
        }

        public TData Get(int id)
        {
            return apiManager.Get(id);
        }

        public Dictionary<int, TData> GetAll()
        {
            return apiManager.GetAll();
        }

        public bool Add(TData data)
        {
            try
            {
                apiManager.Add(data);
                return true;
            }
            catch (Exception e)
            {
                throw new YB_PersistorError(e.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return apiManager.Delete(id);
            }
            catch (Exception e)
            {
                throw new YB_PersistorError(e.Message);
            }
        }

        public bool Update(TData data)
        {
            try
            {
                return apiManager.Update(data);
            }
            catch (Exception e)
            {
                throw new YB_PersistorError(e.Message);
            }
        }
    }
}