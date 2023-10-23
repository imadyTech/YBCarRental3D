using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_RentManager : YB_ManagerBasis<YB_Rent>
    {
        public YB_RentManager(string baseUrl) : base(baseUrl, "YBRents")
        {
        }

        public async Task<bool> PlaceOrder(int userId, int carId, DateTime startDate, int days)
        {
            DateTime today = DateTime.Now;
            YB_Rent order = new YB_Rent();
            order.UserId = userId;
            order.CarId = carId;
            order.RentStart = startDate;
            order.RentDays = days;
            order.DateOfOrder = today;
            order.Status = YB_Rent.YB_Rental_Status_Pending;

            return await base.Add(order);
        }
        public async Task<bool> ApproveOrder(YB_Rent order)
        {
            if (order.Status != YB_Rent.YB_Rental_Status_Pending) 
                return false;
            try
            {
                var requestString = $"{this.apiContext.BaseApiUrl}/approve/{order.Id}";
                var result = await apiContext.GetRequest(requestString);
                return result.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<bool> RejectOrder(YB_Rent order)
        {
            if (order.Status != YB_Rent.YB_Rental_Status_Pending) 
                return false;
            try
            {
                var requestString = $"{this.apiContext.BaseApiUrl}/reject/{order.Id}";
                var result = await apiContext.GetRequest(requestString);
                return result.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<IEnumerable<YB_Rent>> ListOrders(int pageNum, int pageSize)
        {
            return await base.GetList(pageNum, pageSize);
        }
    }

}