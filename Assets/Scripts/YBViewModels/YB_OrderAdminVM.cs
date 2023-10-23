using imady.NebuUI;
using System;

namespace YBCarRental3D
{
    //119 Admin the order (Admin feature )- InputView
    public class YB_OrderAdminVM : YB_ViewModelBasis<YB_Rent>
    {
        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
        YB_RentManager rentManagerPtr = YB_ManagerFactory.RentMgr;
        YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;


        #region ===== view properties =====
        YB_Car car;
        YB_User user;
        [NbuViewProperty] public string Id => base.principalObject.Id.ToString();
        [NbuViewProperty] public int UserId => base.principalObject.UserId;
        [NbuViewProperty] public string CustomerName => $"{user.FirstName} {user.FamilyName}";
        [NbuViewProperty] public string CarInfo => $"{car.Make} {car.Model} {car.Year}";
        [NbuViewProperty] public string DateOfOrder => base.principalObject.DateOfOrder.ToString();
        [NbuViewProperty] public string Status => base.principalObject.Status.ToString();
        [NbuViewProperty] public string RentDays => base.principalObject.RentDays.ToString();
        [NbuViewProperty] public string RentStart => base.principalObject.RentStart.ToString();
        [NbuViewProperty] public string OrderCost => (base.principalObject.RentDays * car.DayRentPrice).ToString();
        [NbuViewProperty] public string UserName => user.UserName;
        [NbuViewProperty] public string Balance => user.Balance.ToString("c2");
        #endregion ===== view properties =====

        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_Rent;
            car = await carManagerPtr.GetCar(this.principalObject.CarId);
            user = await userManagerPtr.GetUser(this.principalObject.UserId);

            base.onViewForwarded(fromView);
        }

        string reviewType;
        public async override void onSubmit()
        {
            if (!ValidateOrder(this.principalObject))
            {
                base.ybWindow.PopPrompt(this.viewDef.Title, "You must choose order at PENDING status.");
                return;
            }

            if (reviewType == "APPROVE")
            {
                await rentManagerPtr.ApproveOrder(this.principalObject);
                base.ybWindow.PopPrompt(this.viewDef.Title, "You have approved the order!", YBGlobal.ADMIN_MAIN_VIEW);
            };
            if (reviewType == "REJECT")
            {
                await rentManagerPtr.RejectOrder(this.principalObject);
                base.ybWindow.PopPrompt(this.viewDef.Title, "You have rejected the order.", YBGlobal.ADMIN_MAIN_VIEW);
            };

            bool ValidateOrder(YB_Rent order)
            {
                if (order.Status != YB_Rent.YB_Rental_Status_Pending)
                    return false;
                else
                    return true;
            }
        }

        public override void OnButtonClicked(YB_ViewItemBasis button)
        {
            this.reviewType = button.Content;
            base.OnButtonClicked(button);
        }


    }
}