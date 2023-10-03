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


        public override void onViewForwarded(YB_DataBasis fromData)		
		{
			this.principalObject = (YB_User)(fromData);
		}


        public override void onSubmit(Dictionary<string, string> valuesMapPtr)				
		{
			var userPtr = this.principalObject;

			try {
				//userPtr.Id				= std.stoi((valuesMapPtr)["Id"]);
				userPtr.UserName = ((valuesMapPtr)["UserName"]);
				userPtr.FirstName = ((valuesMapPtr)["FirstName"]);
				userPtr.FamilyName = ((valuesMapPtr)["FamilyName"]);
				userPtr.UserRoles = ((valuesMapPtr)["UserRoles"]);
				userPtr.Balance = int.Parse((valuesMapPtr)["Balance"]);
			}
			catch (Exception e)
			{
				Window.PopPrompt("Some issues in your input. Please check again.", null);
			}
			try {
				if (userManagerPtr.UpdateUser(userPtr)) {
					Window.PopPrompt("The user info was successfully updated.", YBGlobal.ADMIN_MAIN_VIEW);
				}
				else
				{
					Window.PopPrompt("The user info update was NOT successful.", YBGlobal.ADMIN_MAIN_VIEW);
				}
			}
			catch (Exception e)
			{
				Window.PopPrompt("Something goes wrong. Please check again.", null);
			}
		}
	}
}