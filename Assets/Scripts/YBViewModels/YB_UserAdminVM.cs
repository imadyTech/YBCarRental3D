using System;
using System.Collections.Generic;

namespace YBCarRental3D {
    //120 USER INFO UPDATE (admin feature) - InputView
    public class YB_UserAdminVM : YB_ViewModelBasis<YB_User>
	{
        public YB_UserAdminVM() : base() {}

		YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
		YB_RentManager rentManagerPtr = YB_ManagerFactory.RentMgr;
		YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;


        public async override void onSubmit()				
		{
			var userPtr = this.principalObject;
			try {
				//userPtr.Id				= std.stoi((valuesMapPtr)["Id"]);
				userPtr.UserName	= base.Get_PropertyValue("UserName");
				userPtr.FirstName	= base.Get_PropertyValue("FirstName");
				userPtr.FamilyName	= base.Get_PropertyValue("FamilyName");
				userPtr.UserRoles	= base.Get_PropertyValue("UserRoles");
				userPtr.Balance		= int.Parse(base.Get_PropertyValue("Balance"));
			}
			catch (Exception e)
			{
				ybWindow.PopPrompt(this.viewDef.Title, "Some issues in your input. Please check again.");
			}

			try {
				var result = await userManagerPtr.UpdateUser(userPtr);
				if (result == true) {
					ybWindow.PopPrompt(this.viewDef.Title, "The user info was successfully updated.", YBGlobal.ADMIN_MAIN_VIEW);
				}
				else
				{
					ybWindow.PopPrompt(this.viewDef.Title, "The user info update was NOT successful.", YBGlobal.ADMIN_MAIN_VIEW);
				}
			}
			catch (Exception e)
			{
				ybWindow.PopPrompt(this.viewDef.Title, "Something goes wrong. Please check again.");
			}
		}
	}
}