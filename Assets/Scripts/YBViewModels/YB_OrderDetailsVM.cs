

using imady.NebuUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace YBCarRental3D
{
    //113 order details - DetailsView
    public class YB_OrderDetailsVM : YB_ViewModelBasis<YB_Rent>
    {
        public YB_OrderDetailsVM() : base() { }

        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
        YB_RentManager rentManagerPtr = YB_ManagerFactory.RentMgr;
        YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;


        #region ===== view properties =====
        YB_Car car;
        [NbuViewProperty] public string Id => base.principalObject.Id.ToString();
        [NbuViewProperty] public string CustomerName => $"{userManagerPtr.CurrentUser.FirstName} {userManagerPtr.CurrentUser.FamilyName}";
        [NbuViewProperty] public string CarInfo => $"{car.Make} {car.Model} {car.Year}";
        [NbuViewProperty] public string DateOfOrder => base.principalObject.DateOfOrder.ToString();
        [NbuViewProperty] public string Status => base.principalObject.Status.ToString();
        [NbuViewProperty] public string OrderCost => (base.principalObject.RentDays * car.DayRentPrice).ToString();
        [NbuViewProperty] public string UserName => userManagerPtr.CurrentUser.UserName;
        [NbuViewProperty] public string Balance => userManagerPtr.CurrentUser.Balance.ToString();
        #endregion ===== view properties =====


        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_Rent;
            base.onViewForwarded(fromView);

            car = await carManagerPtr.GetCar(base.principalObject.CarId);

            base.RenderView();
        }

        private void Update()
        {
            if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape))
            {
                YB_Window.Instance.Back();
            }
        }

        //public override string Get_PropertyValue(string bindName)
        //{
        //    string value = "";
        //    if (bindName == "UserName")
        //    {
        //        value = userManagerPtr.CurrentUser.UserName;
        //        return value;
        //    }
        //    if (bindName == "Balance")
        //    {
        //        value = (userManagerPtr.CurrentUser.Balance).ToString();
        //        return value;
        //    }
        //    if (bindName == "CarInfo")
        //    {
        //        var car = carManagerPtr.GetCar(principalObject.CarId);
        //        value = car.Make + " " + car.Model + " " + car.Year;
        //        return value;
        //    }
        //    if (bindName == "CustomerName")
        //    {
        //        value = userManagerPtr.CurrentUser.FirstName + " " + userManagerPtr.CurrentUser.FamilyName;
        //        return value;
        //    }
        //    if (bindName == "OrderCost")
        //    {
        //        var car = carManagerPtr.GetCar(principalObject.CarId);
        //        value = ((float)principalObject.RentDays * car.DayRentPrice).ToString();
        //        return value;
        //    }
        //    return base.Get_PropertyValue(bindName);
        //}


        //public async override void onSubmit()
        //{
        //    throw new NotImplementedException();

        //    int daysToRent = 0;
        //    int carId = 0;

        //    DateTime startDate=DateTime.Now;

        //    if (base.Has_PropertyValue("DaysToRent"))
        //        daysToRent = int.Parse(base.Get_PropertyValue("DaysToRent"));
        //    if (base.Has_PropertyValue("Id"))
        //        carId = int.Parse(base.Get_PropertyValue("Id"));

        //    YB_Car car = await carManagerPtr.GetCar(carId);
        //    YB_Rent order = new YB_Rent();
        //    int totalPrice = (int) car.DayRentPrice*daysToRent;

        //    if (userManagerPtr.CurrentUser.Balance >= totalPrice)
        //    {
        //        var result = await rentManagerPtr.PlaceOrder((userManagerPtr.CurrentUser).Id,
        //            carId,
        //            startDate,
        //            daysToRent);
        //    }
        //    else
        //    {
        //        ybWindow.PopPrompt(this.viewDef.Title, "Your order has been submitted!", YBGlobal.USER_MAIN_VIEW);
        //    }
        //}
    }
}