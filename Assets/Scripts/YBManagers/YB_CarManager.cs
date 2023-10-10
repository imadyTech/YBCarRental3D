using System;
using System.Collections.Generic;

namespace YBCarRental3D
{
    public class YB_CarManager : YB_ManagerBasis<YB_Car>
    {
        public YB_CarManager(string baseUrl) : base(baseUrl, "YBCars")
        {
        }

        public bool AddCar(YB_Car carPtr)
        {
            return base.Add(carPtr).Result;
        }

        public bool DeleteCar(YB_Car carPtr)
        {
            return this.Delete(carPtr.Id).Result;
        }

        public bool DeleteCar(int carId)
        {
            return base.Delete(carId).Result;
        }

        public bool UpdateCar(YB_Car carPtr)
        {
            return base.Update(carPtr).Result;
        }

        public YB_Car GetCar(int carId)
        {
            return base.Get(carId).Result;
        }

        public Dictionary<int, YB_Car> ListCars()
        {
            throw new NotImplementedException();
            //return base.GetAll();
        }
    }
}