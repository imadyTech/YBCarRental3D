

using System;
using System.Collections.Generic;

namespace YBCarRental3D {

	//103 - RegisterView
	public class YB_UserRegisterVM : YB_ViewModelBasis<YB_User>
	{
		public YB_UserRegisterVM () : base() {}

		
		YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
			//for Register, always starts from an empty user
			YB_User userPtr = new YB_User();
			userPtr.Balance = 0;
			userPtr.UserRoles = "user";
			userPtr.LoginStatus = false;
			this.principalObject = userPtr;
		}

        public async override void onSubmit()				
		{
			var userPtr = this.principalObject;

			try {
				//userPtr.Id				= std::stoi((valuesMapPtr)["Id"]);
				userPtr.UserName	= base.Get_ViewPropertyValue("UserName");
				userPtr.FirstName	= base.Get_ViewPropertyValue("FirstName");
				userPtr.FamilyName  = base.Get_ViewPropertyValue("FamilyName");
				userPtr.Password	= base.Get_ViewPropertyValue("Password");
			}
			catch (Exception e)
			{
				ybWindow.PopPrompt(this.viewDef.Title, "Some issues in your input. Please check again.");
			}

			//Input verification (front end
			if (userPtr.UserName.Length > 10 || userPtr.UserName.Length < 3) {
                ybWindow.PopPrompt(this.viewDef.Title, "UserName length must between 3 and 10 characters.");
				return;
			}
			if (userPtr.FirstName.Length > 12 || userPtr.FirstName.Length < 3) {
                ybWindow.PopPrompt(this.viewDef.Title, "First Name length must between 3 and 12 characters..");
				return;
			}
			if (userPtr.FamilyName.Length > 12 || userPtr.FamilyName.Length < 3) {
                ybWindow.PopPrompt(this.viewDef.Title, "Family Name length must between 3 and 12 characters.");
				return;
			}
			if (userPtr.Password.Length > 6 || userPtr.Password.Length < 1) {
                ybWindow.PopPrompt(this.viewDef.Title, "Must input a password no more than 6 digits or characters.");
				return;
			}

			try {
				var result = await userManagerPtr.UserRegister(userPtr);

                if (result) {
                    ybWindow.PopPrompt(this.viewDef.Title, "The new user was successfully registered.", YBGlobal.MAIN_VIEW);
				}
				else
				{
                    ybWindow.PopPrompt(this.viewDef.Title, "The user registration was NOT successful.", YBGlobal.MAIN_VIEW);
				}
			}
			catch (Exception e)
			{
                ybWindow.PopPrompt(this.viewDef.Title, "Something goes wrong. Please check again.", null);
			}
		}
        public override void OnButtonClicked(YB_ViewItemBasis button)
        {
            if ((button as YB_ButtonItem).ButtonType == YBGlobal.Button_Type_Submit)
                this.onSubmit();
        }

    };


}

