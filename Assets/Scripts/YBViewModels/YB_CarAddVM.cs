using imady.NebuUI;
using System;

namespace YBCarRental3D
{
    //113 order details - DetailsView
    public class YB_CarAddVM : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarAddVM() : base() { }

        YB_UserManager userManagerPtr   = YB_ManagerFactory.UserMgr;
        YB_CarManager carManagerPtr     = YB_ManagerFactory.CarMgr;



        #region ===== View properties =====
        [NbuViewProperty]
        public string UserRoles => userManagerPtr.CurrentUser.UserRoles;
        [NbuViewProperty]
        public string UserName => userManagerPtr.CurrentUser.UserName;
        [NbuViewProperty]
        public string Make { get; set; }
        [NbuViewProperty]
        public string Model { get; set; }
        [NbuViewProperty]
        public int Year { get; set; }
        [NbuViewProperty]
        public int Mileage { get; set; }
        [NbuViewProperty]
        public bool IsAvailable { get; set; }
        [NbuViewProperty]
        public int MinRentPeriod { get; set; }
        [NbuViewProperty]
        public int MaxRentPeriod { get; set; }
        [NbuViewProperty]
        public float DayRentPrice { get; set; }
        [NbuViewProperty]
        public string UnityModelName { get; set; }
        #endregion ===== View properties =====



        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            //bypass base function
        }

        public async override void onSubmit()
        {
            if (base.Has_ViewPropertyValue("Model")) Make = base.Get_ViewPropertyValue("Make");
            if (base.Has_ViewPropertyValue("Model")) Model = base.Get_ViewPropertyValue("Model"); ;
            if (base.Has_ViewPropertyValue("Year")) Year = int.Parse(base.Get_ViewPropertyValue("Year"));
            if (base.Has_ViewPropertyValue("Mileage")) Mileage = int.Parse(base.Get_ViewPropertyValue("Mileage"));
            if (base.Has_ViewPropertyValue("MinRentPeriod")) MinRentPeriod = int.Parse(base.Get_ViewPropertyValue("MinRentPeriod"));
            if (base.Has_ViewPropertyValue("MaxRentPeriod")) MaxRentPeriod = int.Parse(base.Get_ViewPropertyValue("MaxRentPeriod"));
            if (base.Has_ViewPropertyValue("DayRentPrice")) DayRentPrice = float.Parse(base.Get_ViewPropertyValue("DayRentPrice"));
            if (base.Has_ViewPropertyValue("UnityModelName")) UnityModelName = base.Get_ViewPropertyValue("UnityModelName");

            YB_Car car = new YB_Car();
            car.Make = Make;
            car.Model = Model;
            car.Year = Year;
            car.Mileage = Mileage;
            car.IsAvailable = IsAvailable;
            car.MinRentPeriod = MinRentPeriod;
            car.MaxRentPeriod = MaxRentPeriod;
            car.DayRentPrice = DayRentPrice;
            car.UnityModelName = UnityModelName;

            bool result = await carManagerPtr.AddCar(car);
            if (result)
            {
                ybWindow.PopPrompt(this.viewDef.Title, "The car has been successfully added!", YBGlobal.ADMIN_MAIN_VIEW);
            }
            else
            {
                ybWindow.PopPrompt(this.viewDef.Title, "There is some issue to add your car information.");
            }
        }
    }
}

