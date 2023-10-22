

using Newtonsoft.Json;

namespace YBCarRental3D
{

	public class YB_User : YB_DataBasis
	{
		public YB_User() { }
		public YB_User(string username, int password) : this() { }
		~YB_User() { }

        [JsonProperty] public string					UserName;                                       //max 12 alphabets; No verification in this application.
        [JsonProperty] public string					FirstName;
		[JsonProperty] public string					FamilyName;
		[JsonProperty] public string					Password;										//max 6 digits alphabet/numerics;
		[JsonProperty] public string					UserRoles;										//multiple roles are allowed, separated by "|"
		[JsonProperty] public bool						LoginStatus;									//true: logged in; false: logged out;
		[JsonProperty] public double					Balance;										//Account Balance allows user to hire a car (fail to rent if no sufficient balance).
	};

}
