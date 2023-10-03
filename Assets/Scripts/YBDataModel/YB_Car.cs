
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
		~YB_Car() { }

		public string	Make;
		public string	Model;
		public int		Year;
		public int		Mileage;
		public bool		IsAvailable;					//is the car available now?
		public int		MinRentPeriod;					//day
		public int		MaxRentPeriod;					//day
		public float	DayRentPrice;					//rental price per day;

	};
}
