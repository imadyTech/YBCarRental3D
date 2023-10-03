using System;

namespace YBCarRental3D
{

    /// <summary>
    /// The record of a rental order;
    /// </summary>
    public class YB_Rent : YB_DataBasis
    {
        public const string YB_Rental_Status_Pending   = "pending";
        public const string YB_Rental_Status_Approved  = "approved";
        public const string YB_Rental_Status_Rejected  = "rejected";
        public const string YB_Rental_Status_Completed = "completed";

        public YB_Rent() { }
        public YB_Rent(YB_User user, YB_Car car, DateTime start, int days) : this() { }
        public YB_Rent(int userId, int carId, DateTime start, int days) : this() { }
        ~YB_Rent() { }

        public int UserId;
        public int CarId;
        public DateTime RentStart;                      //rental start date
        public DateTime DateOfOrder;                    //date placed order
        public int RentDays;                        //total days of rental
        public string Status;                           //check YB_Global_Header for definition

    };
}

