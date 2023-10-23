

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
        }

        private void Update()
        {
            if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape))
            {
                YB_Window.Instance.Back();
            }
        }
    }
}