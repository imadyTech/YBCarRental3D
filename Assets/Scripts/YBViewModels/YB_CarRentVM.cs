

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

        YB_UserManager  userManagerPtr = YB_ManagerFactory.UserMgr;
        YB_RentManager  rentManagerPtr = YB_ManagerFactory.RentMgr;
        YB_CarManager   carManagerPtr = YB_ManagerFactory.CarMgr;


        public override void onViewForwarded(YB_DataBasis fromData)
        {
            this.principalObject = (YB_Car)(fromData);
        }


        public override string Get_PropertyValue(string bindName)
        {
            string value = "";
            if (bindName == "UserName")
            {
                value = userManagerPtr.CurrentUser().UserName;
                return value;
            }
            if (bindName == "Balance")
            {
                value = (userManagerPtr.CurrentUser().Balance).ToString();
                return value;
            }
            return base.Get_PropertyValue(bindName);
        }


        public override void onSubmit(Dictionary<string, string> valuesMapPtr)
        {
            int daysToRent = 0;
            int carId = 0;

            DateTime startDate = DateTime.Now;

            string key = "DaysToRent";
            if (valuesMapPtr.ContainsKey(key))
                daysToRent = int.Parse(valuesMapPtr[key]);
            key = "Id";
            if (valuesMapPtr.ContainsKey(key))
                carId = int.Parse((valuesMapPtr)[key]);

            YB_Car car      = carManagerPtr.GetCar(carId);
            YB_Rent order   = new YB_Rent();
            int totalPrice = (int) (car.DayRentPrice * daysToRent);

            if (userManagerPtr.CurrentUser().Balance >= totalPrice)
            {
                bool result = rentManagerPtr.PlaceOrder((userManagerPtr.CurrentUser()).Id,
                    carId,
                    startDate,
                    daysToRent);
                //order success, deduct account balance
                if (result)
                    userManagerPtr.CurrentUser().Balance -= totalPrice;
                userManagerPtr.Update(userManagerPtr.CurrentUser());
            }
            else
            {
                Window.PopPrompt("You don't have enough money to fulfill the order.", YBGlobal.USER_MAIN_VIEW);
            }
        }
    };

}