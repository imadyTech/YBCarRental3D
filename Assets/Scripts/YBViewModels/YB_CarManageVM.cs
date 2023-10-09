
using System.Collections.Generic;
using UnityEditor.Rendering;

namespace YBCarRental3D
{
    //114 update or delete car - InputView
    public class YB_CarManageVM : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarManageVM() : base()
        {
        }

        YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public bool IsAvailable { get; set; }
        public int MinRentPeriod { get; set; }
        public int MaxRentPeriod { get; set; }
        public float DayRentPrice { get; set; }


        public override void onSubmit()
        {
            if (base.Has_PropertyValue("Id")) Id = int.Parse(base.Get_PropertyValue("Id"));
            if (base.Has_PropertyValue("Make")) Make = base.Get_PropertyValue("Make");
            if (base.Has_PropertyValue("Model")) Model = base.Get_PropertyValue("Model");
            if (base.Has_PropertyValue("Year")) Year = int.Parse(base.Get_PropertyValue("Year"));
            if (base.Has_PropertyValue("Mileage")) Mileage = int.Parse(base.Get_PropertyValue("Mileage"));
            if (base.Has_PropertyValue("MinRentPeriod")) MinRentPeriod = int.Parse(base.Get_PropertyValue("MinRentPeriod"));
            if (base.Has_PropertyValue("MaxRentPeriod")) MaxRentPeriod = int.Parse(base.Get_PropertyValue("MaxRentPeriod"));
            if (base.Has_PropertyValue("DayRentPrice")) DayRentPrice = float.Parse(base.Get_PropertyValue("DayRentPrice"));

            YB_Car car = new YB_Car();
            car.Id = Id;
            car.Make = Make;
            car.Model = Model;
            car.Year = Year;
            car.Mileage = Mileage;
            car.IsAvailable = IsAvailable;
            car.MinRentPeriod = MinRentPeriod;
            car.MaxRentPeriod = MaxRentPeriod;
            car.DayRentPrice = DayRentPrice;

            bool result = carManagerPtr.UpdateCar(car);
            if (result)
            {
                ybWindow.PopPrompt(this.viewDef.Title, "The car has been successfully updated!", YBGlobal.ADMIN_MAIN_VIEW);
            }
            else
            {
                ybWindow.PopPrompt(this.viewDef.Title, "There is some issue to add your car information.", null);
            }
        }

    };

}

