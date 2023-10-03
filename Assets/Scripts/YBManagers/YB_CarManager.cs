using System;
using System.Collections.Generic;

namespace YBCarRental3D
{
    public class YB_CarManager : YB_ManagerBasis<YB_Car>
    {
        public YB_CarManager(string v) : base() { }


        public bool AddCar(YB_Car carPtr)
        {
            var existingCar = base.Get(carPtr.Id);
            if (existingCar != null)
            {
                return false; //already exist
            }
            //carPtr.Id = this.CreateIncrementId();
            return this.Add(carPtr);
        }

        public bool DeleteCar(YB_Car carPtr)
        {
            var existingCar = this.Get(carPtr.Id);
            if (existingCar ==null)
            {
                return false; //car doesn't exist
            }
            return this.Delete(carPtr.Id);
        }

        public bool DeleteCar(int carId)
        {
            var existingCar = this.Get(carId);
            if (existingCar == null)
            {
                return false; //car doesn't exist
            }
            return this.Delete(carId);
        }

        public bool UpdateCar(YB_Car carPtr)
        {
            var existingCar = this.Get(carPtr.Id);
            if (existingCar == null)
            {
                return false; //car doesn't exist
            }
            return this.Update(carPtr);
        }

        public YB_Car GetCar(int carId)
        {
            return this.Get(carId);
        }

        public Dictionary<int, YB_Car> ListCars()
        {
            return base.GetAll();
        }
    }
}