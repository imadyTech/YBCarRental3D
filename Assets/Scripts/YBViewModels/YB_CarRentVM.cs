using imady.NebuUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace YBCarRental3D
{
    //112 rent a car - InputView
    public class YB_CarRentVM : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarRentVM() : base()
        {
        }

        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
        YB_RentManager rentManagerPtr = YB_ManagerFactory.RentMgr;
        YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;


        #region ===== view properties =====
        [NbuViewProperty]
        public string Id => base.principalObject.Id.ToString();
        [NbuViewProperty]
        public string Make => base.principalObject.Make;
        [NbuViewProperty]
        public string Model => base.principalObject.Model;
        [NbuViewProperty]
        public string Year => base.principalObject.Year.ToString();
        [NbuViewProperty]
        public string Mileage => base.principalObject.Mileage.ToString();
        [NbuViewProperty]
        public string MinRentPeriod => base.principalObject.MinRentPeriod.ToString();
        [NbuViewProperty]
        public string MaxRentPeriod => base.principalObject.MaxRentPeriod.ToString();
        [NbuViewProperty]
        public string DayRentPrice => base.principalObject.DayRentPrice.ToString("c2");
        [NbuViewProperty]
        public string IsAvailable => base.principalObject.IsAvailable ? "Yes" : "No";
        #endregion ===== view properties =====

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_Car;
            base.onViewForwarded(fromView);
            base.RenderView();
        }

        public async override void onSubmit()
        {
            int daysToRent = 0;
            //int carId = 0;
            DateTime startDate = DateTime.Now;


            if (base.Has_PropertyValue("DaysToRent"))
                daysToRent = int.Parse(base.Get_PropertyValue("DaysToRent"));
            //if (base.Has_PropertyValue("Id"))
            //    carId = int.Parse(base.Get_PropertyValue("Id"));
            //YB_Car car = await carManagerPtr.GetCar(this.principalObject.Id);

            int totalPrice = (int)(this.principalObject.DayRentPrice * daysToRent);

            if (userManagerPtr.CurrentUser.Balance >= totalPrice)
            {
                bool result = await rentManagerPtr.PlaceOrder(
                    (userManagerPtr.CurrentUser).Id,
                    this.principalObject.Id,
                    startDate,
                    daysToRent);
                //order success, deduct account balance
                if (result)
                    ybWindow.PopPrompt(this.viewDef.Title, "Your order has been successfully placed.", this.viewDef.GotoView);
                else
                    ybWindow.PopPrompt(this.viewDef.Title, "Your order was NOT placed due to some server issues.");
            }
            else
            {
                ybWindow.PopPrompt(this.viewDef.Title, "You don't have enough money to fulfill the order.");
            }
        }

        public override void OnButtonClicked(YB_ViewItemBasis button)
        {
            Debug.Log($"[Button Clicked] : {this.name} - {button.Id}");
            if ((button as YB_ButtonItem).ButtonType == YBGlobal.Button_Type_Submit)
            {
                this.onSubmit();
            }
        }

    };

}