using System;

namespace YBCarRental3D
{
    public class YB_RentManager : YB_ManagerBasis<YB_Rent>
    {
        public YB_RentManager(string baseUrl) : base(baseUrl, "YBRents")
        {
        }

        public bool PlaceOrder(int userId, int carId, DateTime startDate, int days)
        {
            DateTime rawtime;

            DateTime today = DateTime.Now;


            YB_Rent order = new YB_Rent();
            order.UserId = userId;
            order.CarId = carId;
            order.RentStart = startDate;
            order.RentDays = days;
            order.DateOfOrder = today;
            order.Status = YB_Rent.YB_Rental_Status_Pending;

            return base.Add(order).Result;
        }
        public bool ApproveOrder(YB_Rent rentalOrder)
        {
            if (rentalOrder.Status != YB_Rent.YB_Rental_Status_Pending) return false;
            try
            {
                rentalOrder.Status = YB_Rent.YB_Rental_Status_Approved;
                return base.Update(rentalOrder).Result;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool RejectOrder(YB_Rent rentalOrder)
        {
            if (rentalOrder.Status != YB_Rent.YB_Rental_Status_Pending) return false;
            try
            {
                rentalOrder.Status = YB_Rent.YB_Rental_Status_Rejected;
                return base.Update(rentalOrder).Result;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool ApproveOrder(int orderId)
        {
            var orderPtr = base.Get(orderId).Result;

            return this.ApproveOrder(orderPtr);
        }
        public bool RejectOrder(int orderId)
        {
            var orderPtr = base.Get(orderId).Result;

            return this.RejectOrder(orderPtr);
        }
    }

}