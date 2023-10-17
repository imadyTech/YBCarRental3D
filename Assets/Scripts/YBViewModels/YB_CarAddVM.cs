using System;

namespace YBCarRental3D
{
    //113 order details - DetailsView
    public class YB_CarAddVM : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarAddVM() : base() { }

        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
        YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;

        public string UserRoles => userManagerPtr.CurrentUser.UserRoles;
        public string UserName => userManagerPtr.CurrentUser.UserName;
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public bool IsAvailable { get; set; }
        public int MinRentPeriod { get; set; }
        public int MaxRentPeriod { get; set; }
        public float DayRentPrice { get; set; }


        public override void onViewForwarded(YB_ViewBasis fromView)
        {
        }
        public override string Get_PropertyValue(string bindName)
        {
            return base.Get_PropertyValue(bindName);
        }
        public async override void onSubmit()
        {
            throw new NotImplementedException();

            if (base.Has_PropertyValue("Model")) Make = base.Get_PropertyValue("Make");
            if (base.Has_PropertyValue("Model")) Model = base.Get_PropertyValue("Model"); ;
            if (base.Has_PropertyValue("Year")) Year = int.Parse(base.Get_PropertyValue("Year"));
            if (base.Has_PropertyValue("Mileage")) Mileage = int.Parse(base.Get_PropertyValue("Mileage"));
            if (base.Has_PropertyValue("MinRentPeriod")) MinRentPeriod = int.Parse(base.Get_PropertyValue("MinRentPeriod"));
            if (base.Has_PropertyValue("MaxRentPeriod")) MaxRentPeriod = int.Parse(base.Get_PropertyValue("MaxRentPeriod"));
            if (base.Has_PropertyValue("DayRentPrice")) DayRentPrice = float.Parse(base.Get_PropertyValue("DayRentPrice"));

            YB_Car car = new YB_Car();
            car.Make = Make;
            car.Model = Model;
            car.Year = Year;
            car.Mileage = Mileage;
            car.IsAvailable = IsAvailable;
            car.MinRentPeriod = MinRentPeriod;
            car.MaxRentPeriod = MaxRentPeriod;
            car.DayRentPrice = DayRentPrice;

            bool result = await carManagerPtr.AddCar(car);
            if (result)
            {
                ybWindow.PopPrompt(this.viewDef.Title, "The car has been successfully added!", YBGlobal.ADMIN_MAIN_VIEW);
            }
            else
            {
                ybWindow.PopPrompt(this.viewDef.Title, "There is some issue to add your car information.", null);
            }
        }
    }
}

