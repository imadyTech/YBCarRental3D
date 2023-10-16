
using System.Threading;
using UnityEngine.UIElements;

namespace YBCarRental3D
{
	public class YB_Car : YB_DataBasis
	{
		public YB_Car()
		{
			Id = -1;
			Make = "";
			Model = "";
			Year = -1;
			Mileage = -1;
			IsAvailable = true;
			MinRentPeriod = 1;
			MaxRentPeriod = 30;
			DayRentPrice = 0;

			persistentSeparator = ';';
		}
		public YB_Car(string defString):this()
		{
			base.serializedString = defString;
            Deserialize(defString);
		}
		~YB_Car() { }

		public string	Make;
		public string	Model;
		public int		Year;
		public int		Mileage;
		public bool		IsAvailable;					//is the car available now?
		public int		MinRentPeriod;					//day
		public int		MaxRentPeriod;					//day
		public float	DayRentPrice;                   //rental price per day;


        public void Deserialize(string line)
        {
            base.SplitLine(line);

            if (base.HasValue("Id"))				base.Id = int.Parse(FindValue("Id"));
            if (base.HasValue("Make"))				Make= FindValue("Make");
            if (base.HasValue("Model"))				Model= FindValue("Model");
            if (base.HasValue("Year"))				Year = int.Parse(FindValue("Year"));
            if (base.HasValue("Mileage"))			Mileage = int.Parse(FindValue("Mileage"));
            if (base.HasValue("IsAvailable"))		IsAvailable= FindValue("IsAvailable")=="1";
            if (base.HasValue("MinRentPeriod"))		MinRentPeriod= int.Parse(FindValue("MinRentPeriod"));
            if (base.HasValue("MaxRentPeriod"))		MaxRentPeriod= int.Parse(FindValue("MaxRentPeriod"));
            if (base.HasValue("DayRentPrice"))		DayRentPrice= int.Parse(FindValue("DayRentPrice"));
        }

    };
}
