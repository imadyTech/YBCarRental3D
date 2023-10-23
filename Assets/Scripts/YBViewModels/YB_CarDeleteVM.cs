using imady.NebuUI;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

namespace YBCarRental3D
{
    //118 YB_CarDeleteVm- ListView
    public class YB_CarDeleteVm : YB_ViewModelBasis<YB_Car>
    {
        #region ===== view properties =====
        [NbuViewProperty]
        public int Id   =>this.principalObject.Id;
        [NbuViewProperty]
        public string Make => this.principalObject.Make;
        [NbuViewProperty]
        public string Model => this.principalObject.Model;
        [NbuViewProperty]
        public int Year => this.principalObject.Year;
        [NbuViewProperty]
        public int Mileage => this.principalObject.Mileage;
        [NbuViewProperty]
        public bool IsAvailable => this.principalObject.IsAvailable;
        [NbuViewProperty]
        public int MinRentPeriod => this.principalObject.MinRentPeriod;
        [NbuViewProperty]
        public int MaxRentPeriod => this.principalObject.MaxRentPeriod;
        [NbuViewProperty]
        public float DayRentPrice => this.principalObject.DayRentPrice;
        #endregion ===== view properties =====



        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_Car;
            base.onViewForwarded(fromView);
        }

        public async override void onSubmit()
        {
            bool deleteResult = await YB_ManagerFactory.CarMgr.DeleteCar(this.principalObject);

            if (deleteResult)
                ybWindow.PopPrompt(this.viewDef.Title, "Car was successfully deleted!", this.viewDef.GotoView);
            else
                ybWindow.PopPrompt(this.viewDef.Title, "Car Delete was not successful.", YBGlobal.ADMIN_MAIN_VIEW);
        }
    };
}

