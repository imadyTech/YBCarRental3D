using System;

namespace YBCarRental3D
{
    public class YB_RentManager : YB_ManagerBasis<YB_Rent>
    {
        private string v;

        public YB_RentManager(string v)
        {
            this.v = v;
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

            apiManager.Add(order);
            return true;
        }
        public bool ApproveOrder(YB_Rent rentalOrder)
        {
            if (rentalOrder.Status != YB_Rent.YB_Rental_Status_Pending) return false;
            try
            {
                rentalOrder.Status = YB_Rent.YB_Rental_Status_Approved;
                base.Update(rentalOrder);
                return true;
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
                base.Update(rentalOrder);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool ApproveOrder(int orderId)
        {
            var orderPtr = base.Get(orderId);

            return this.ApproveOrder(orderPtr);
        }
        public bool RejectOrder(int orderId)
        {
            var orderPtr = base.Get(orderId);

            return this.RejectOrder(orderPtr);
        }
    }

}