

namespace YBCarRental3D
{

	public class YB_User : YB_DataBasis
	{
		public YB_User() { }
		public YB_User(string username, int password) : this() { }
		~YB_User() { }

		public string					UserName;										//max 12 alphabets; No verification in this application.
		public string					FirstName;
		public string					FamilyName;
		public string					Password;										//max 6 digits alphabet/numerics;
		public string					UserRoles;										//multiple roles are allowed, separated by "|"
		public bool						LoginStatus;									//true: logged in; false: logged out;
		public double					Balance;										//Account Balance allows user to hire a car (fail to rent if no sufficient balance).
	};

}
