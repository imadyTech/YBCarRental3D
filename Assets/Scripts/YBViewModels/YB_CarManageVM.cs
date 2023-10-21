
using imady.NebuUI;
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


        #region ===== view properties =====
        [NbuViewProperty]
        public int Id
        {
            get => this.principalObject.Id;
            set { this.principalObject.Id = value; }
        }
        [NbuViewProperty]
        public string Make
        {
            get => this.principalObject.Make;
            set { this.principalObject.Make = value; }
        }
        [NbuViewProperty]
        public string Model
        {
            get => this.principalObject.Model;
            set { this.principalObject.Model = value; }
        }
        [NbuViewProperty]
        public int Year
        {
            get => this.principalObject.Year;
            set { this.principalObject.Year = value; }
        }
        [NbuViewProperty]
        public int Mileage
        {
            get => this.principalObject.Mileage;
            set { this.principalObject.Mileage = value; }
        }
        [NbuViewProperty]
        public bool IsAvailable
        {
            get => this.principalObject.IsAvailable;
            set { this.principalObject.IsAvailable = value; }
        }
        [NbuViewProperty]
        public int MinRentPeriod
        {
            get => this.principalObject.MinRentPeriod;
            set { this.principalObject.MinRentPeriod = value; }
        }
        [NbuViewProperty]
        public int MaxRentPeriod
        {
            get => this.principalObject.MaxRentPeriod;
            set { this.principalObject.MaxRentPeriod = value; }
        }
        [NbuViewProperty]
        public float DayRentPrice
        {
            get => this.principalObject.DayRentPrice;
            set { this.principalObject.DayRentPrice = value; }
        }
        #endregion ===== view properties =====

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_Car;
            base.onViewForwarded(fromView);

            base.RenderView();
        }


        public async override void onSubmit()
        {

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

            bool result = await carManagerPtr.UpdateCar(car);
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

