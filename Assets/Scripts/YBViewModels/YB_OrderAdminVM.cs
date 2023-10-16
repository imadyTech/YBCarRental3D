using System;

namespace YBCarRental3D
{
    //119 Admin the order (Admin feature )- InputView
    public class YB_OrderAdminVM : YB_ViewModelBasis<YB_Rent>
    {
        public YB_OrderAdminVM() : base() { }

        YB_UserManager userManagerPtr   = YB_ManagerFactory.UserMgr;
        YB_RentManager rentManagerPtr   = YB_ManagerFactory.RentMgr;
        YB_CarManager carManagerPtr     = YB_ManagerFactory.CarMgr;

        int CarId = -1;
        int RentDays = 0;

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            CarId       = int.Parse( fromView.viewModel.Get_PropertyValue("CarId"));
            RentDays    = int.Parse( fromView.viewModel.Get_PropertyValue("RentDays"));
        }
        public override string Get_PropertyValue(string bindName)
        {
            string value = string.Empty;
            if (bindName == "UserName")
            {
                value = userManagerPtr.CurrentUser.UserName;
                return value;
            }
            if (bindName == "UserRoles")
            {
                value = (userManagerPtr.CurrentUser.UserRoles);
                return value;
            }
            if (bindName == "CarInfo")
            {
                var car = carManagerPtr.GetCar(this.CarId);
                value = car.Make + " " + car.Model + " " + car.Year;
                return value;
            }
            if (bindName == "CustomerName")
            {
                value = userManagerPtr.CurrentUser.FirstName + " " + userManagerPtr.CurrentUser.FamilyName;
                return value;
            }
            if (bindName == "OrderCost")
            {
                var car = carManagerPtr.GetCar(this.CarId);
                value = (this.RentDays * car.DayRentPrice).ToString();
                return value;
            }
            return base.Get_PropertyValue(bindName);
        }
        public override void onSubmit()
        {
            throw new NotImplementedException();

            int orderId = int.Parse(base.Get_PropertyValue("Id"));

            string reviewType = base.Get_PropertyValue(YBGlobal.SUBMIT_BINDKEY_BUTTONCONTENT);
            if (reviewType == "APPROVE")
            {
                rentManagerPtr.ApproveOrder(orderId);
                base.ybWindow.PopPrompt(this.viewDef.Title, "You have approved the order!", YBGlobal.ADMIN_MAIN_VIEW);
            };
            if (reviewType == "REJECT")
            {
                rentManagerPtr.RejectOrder(orderId);
                base.ybWindow.PopPrompt(this.viewDef.Title, "You have rejected the order.", null);
            };
        }
        public override void OnButtonClicked(YB_ViewItemBasis button)
        {
            if ((button as YB_ButtonItem).ButtonType == YBGlobal.Button_Type_Submit)
                this.onSubmit();
        }
    }
}