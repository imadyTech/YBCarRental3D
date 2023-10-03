

using System;
using System.Collections.Generic;

namespace YBCarRental3D {

	//103 - RegisterView
	public class YB_UserRegisterVM : YB_ViewModelBasis<YB_User>
	{
		public YB_UserRegisterVM () : base() {}

		
		YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;

        public override void onViewForwarded(YB_DataBasis fromData)		
		{
			//for Register, always starts from an empty user
			YB_User userPtr = new YB_User();
			userPtr.Balance = 0;
			userPtr.UserRoles = "user";
			userPtr.LoginStatus = false;
			this.principalObject = userPtr;
		}

        public override void onSubmit(Dictionary<string, string> valuesMapPtr)				
		{
			var userPtr = this.principalObject;

			try {
				//userPtr.Id				= std::stoi((valuesMapPtr)["Id"]);
				userPtr.UserName	= ((valuesMapPtr)["UserName"]);
				userPtr.FirstName	= ((valuesMapPtr)["FirstName"]);
				userPtr.FamilyName = ((valuesMapPtr)["FamilyName"]);
				userPtr.Password	= ((valuesMapPtr)["Password"]);
			}
			catch (Exception e)
			{
				Window.PopPrompt("Some issues in your input. Please check again.", null);
			}

			//Input verification
			if (userPtr.UserName.Length > 10 || userPtr.UserName.Length < 3) {
                Window.PopPrompt("UserName length must between 3 and 10 characters.", null);
				return;
			}
			if (userPtr.FirstName.Length > 12 || userPtr.FirstName.Length < 3) {
                Window.PopPrompt("First Name length must between 3 and 12 characters..", null);
				return;
			}
			if (userPtr.FamilyName.Length > 12 || userPtr.FamilyName.Length < 3) {
                Window.PopPrompt("Family Name length must between 3 and 12 characters.", null);
				return;
			}
			if (userPtr.Password.Length > 6 || userPtr.Password.Length < 1) {
                Window.PopPrompt("Must input a password no more than 6 digits or characters.", null);
				return;
			}

			try {
				if (userManagerPtr.UserRegister(userPtr)) {
                    Window.PopPrompt("The new user was successfully registered.", YBGlobal.MAIN_VIEW);
				}
				else
				{
                    Window.PopPrompt("The user registration was NOT successful.", YBGlobal.MAIN_VIEW);
				}
			}
			catch (Exception e)
			{
                Window.PopPrompt("Something goes wrong. Please check again.", null);
			}
		}
	};


}

