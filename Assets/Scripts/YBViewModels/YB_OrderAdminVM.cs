using System.Collections.Generic;

namespace YBCarRental3D
{
    //119 Admin the order (Admin feature )- InputView
    public class YB_OrderAdminVM : YB_ViewModelBasis<YB_Rent>
    {
        public YB_OrderAdminVM() : base() { }

        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
        YB_RentManager rentManagerPtr = YB_ManagerFactory.RentMgr;
        YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;


        public override void onViewForwarded(YB_DataBasis fromData)
        {
            this.principalObject = (YB_Rent)(fromData);
        }
        public override string Get_PropertyValue(string bindName)
        {
            string value = string.Empty;
            if (bindName == "UserName")
            {
                value = userManagerPtr.CurrentUser().UserName;
                return value;
            }
            if (bindName == "UserRoles")
            {
                value = (userManagerPtr.CurrentUser().UserRoles);
                return value;
            }
            if (bindName == "CarInfo")
            {
                var car = carManagerPtr.GetCar(principalObject.CarId);
                value = car.Make + " " + car.Model + " " + car.Year;
                return value;
            }
            if (bindName == "CustomerName")
            {
                value = userManagerPtr.CurrentUser().FirstName + " " + userManagerPtr.CurrentUser().FamilyName;
                return value;
            }
            if (bindName == "OrderCost")
            {
                var car = carManagerPtr.GetCar(principalObject.CarId);
                value = (principalObject.RentDays * car.DayRentPrice).ToString();
                return value;
            }
            return base.Get_PropertyValue(bindName);
        }
        public override void onSubmit(Dictionary<string, string> valuesMapPtr)
        {
            string reviewType = (valuesMapPtr)[YBGlobal.SUBMIT_BINDKEY_BUTTONCONTENT];
            int orderId = int.Parse((valuesMapPtr)["Id"]);
            if (reviewType == "APPROVE")
            {
                rentManagerPtr.ApproveOrder(orderId);
                Window.PopPrompt("You have approved the order!", YBGlobal.ADMIN_MAIN_VIEW);
            };
            if (reviewType == "REJECT")
            {
                rentManagerPtr.RejectOrder(orderId);
                Window.PopPrompt("You have rejected the order.", null);
            };
        }
    };

}