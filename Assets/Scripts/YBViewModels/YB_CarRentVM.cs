

using System;
using System.Collections.Generic;

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


        public override void onViewForwarded(YB_ViewBasis fromView)
        {
        }


        public override string Get_PropertyValue(string bindName)
        {
            string value = "";
            if (bindName == "UserName")
            {
                value = userManagerPtr.CurrentUser.UserName;
                return value;
            }
            if (bindName == "Balance")
            {
                value = (userManagerPtr.CurrentUser.Balance).ToString();
                return value;
            }
            return base.Get_PropertyValue(bindName);
        }


        public override void onSubmit()
        {
            throw new NotImplementedException();

            int daysToRent = 0;
            int carId = 0;

            DateTime startDate = DateTime.Now;


            if (base.Has_PropertyValue("DaysToRent"))
                daysToRent = int.Parse(base.Get_PropertyValue("DaysToRent"));
            if (base.Has_PropertyValue("Id"))
                carId = int.Parse(base.Get_PropertyValue("Id"));

            YB_Car car = carManagerPtr.GetCar(carId);
            YB_Rent order = new YB_Rent();
            int totalPrice = (int)(car.DayRentPrice * daysToRent);

            if (userManagerPtr.CurrentUser.Balance >= totalPrice)
            {
                bool result = rentManagerPtr.PlaceOrder((userManagerPtr.CurrentUser).Id,
                    carId,
                    startDate,
                    daysToRent);
                //order success, deduct account balance
                if (result)
                    userManagerPtr.CurrentUser.Balance -= totalPrice;
                userManagerPtr.Update(userManagerPtr.CurrentUser);
            }
            else
            {
                ybWindow.PopPrompt(this.viewDef.Title, "You don't have enough money to fulfill the order.", YBGlobal.USER_MAIN_VIEW);
            }
        }
    };

}