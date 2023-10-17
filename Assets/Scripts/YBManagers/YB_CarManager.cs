using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_CarManager : YB_ManagerBasis<YB_Car>
    {
        public YB_CarManager(string baseUrl) : base(baseUrl, "YBCars")
        {
        }

        public async Task<bool> AddCar(YB_Car carPtr)
        {
            return await base.Add(carPtr);
        }

        public async Task<bool> DeleteCar(YB_Car carPtr)
        {
            return await base.Delete(carPtr.Id);
        }

        public async Task<bool> DeleteCar(int carId)
        {
            return await base.Delete(carId);
        }

        public async Task<bool> UpdateCar(YB_Car carPtr)
        {
            return await base.Update(carPtr);
        }

        public async Task<YB_Car> GetCar(int carId)
        {
            return await base.Get(carId);
        }

        public async Task<IEnumerable<YB_Car>> ListCars(int pageNum, int pageSize)
        {
            return await base.GetList(pageNum, pageSize);
        }
    }
}