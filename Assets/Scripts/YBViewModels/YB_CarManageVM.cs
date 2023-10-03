
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


        public override void onViewForwarded(YB_DataBasis fromData)
        {
            this.principalObject = (YB_Car)fromData;
        }
        public override void onSubmit(Dictionary<string, string> valuesMapPtr)
        {

            if (valuesMapPtr.ContainsKey("Id")) Id = int.Parse(valuesMapPtr["Id"]);
            if (valuesMapPtr.ContainsKey("Make")) Make = valuesMapPtr["Make"];
            if (valuesMapPtr.ContainsKey("Model")) Model = valuesMapPtr["Model"];
            if (valuesMapPtr.ContainsKey("Year")) Year = int.Parse(valuesMapPtr["Year"]);
            if (valuesMapPtr.ContainsKey("Mileage")) Mileage = int.Parse(valuesMapPtr["Mileage"]);
            if (valuesMapPtr.ContainsKey("MinRentPeriod")) MinRentPeriod = int.Parse(valuesMapPtr["MinRentPeriod"]);
            if (valuesMapPtr.ContainsKey("MaxRentPeriod")) MaxRentPeriod = int.Parse(valuesMapPtr["MaxRentPeriod"]);
            if (valuesMapPtr.ContainsKey("DayRentPrice")) DayRentPrice = float.Parse(valuesMapPtr["DayRentPrice"]);

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
                Window.PopPrompt("The car has been successfully updated!", YBGlobal.ADMIN_MAIN_VIEW);
            }
            else
            {
                Window.PopPrompt("There is some issue to add your car information.", null);
            }
        }

    };

}

