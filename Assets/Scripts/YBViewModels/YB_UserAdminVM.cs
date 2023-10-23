using imady.NebuUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace YBCarRental3D {
    //120 USER INFO UPDATE (admin feature) - InputView
    public class YB_UserAdminVM : YB_ViewModelBasis<YB_User>
	{
        public YB_UserAdminVM() : base() {}

		YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;
		YB_RentManager rentManagerPtr = YB_ManagerFactory.RentMgr;
		YB_CarManager carManagerPtr = YB_ManagerFactory.CarMgr;



        #region ===== view properties =====
        [NbuViewProperty]
        public string Id => this.principalObject.Id.ToString();
        [NbuViewProperty]
        public string UserName => this.principalObject.UserName;
        [NbuViewProperty]
        public string FirstName => this.principalObject.FirstName;
        [NbuViewProperty]
        public string FamilyName => this.principalObject.FamilyName;
        [NbuViewProperty]
        public string UserRoles => this.principalObject.UserRoles;
        [NbuViewProperty]
        public string Balance => this.principalObject.Balance.ToString();
        #endregion ===== view properties =====


        public override void onViewForwarded(YB_ViewBasis fromView)
        {
#if DEVELOPMENT
            Debug.Log($"[View onViewForwarded] : {base.viewDef.Title}");
#endif
			this.principalObject = fromView.viewModel.PrincipalData as YB_User;
            base.onViewForwarded(fromView);
        }

        public async override void onSubmit()				
		{
			var userPtr = this.principalObject;
			try {
				userPtr.Id			= int.Parse(base.Get_ViewPropertyValue("Id"));
				userPtr.UserName	= base.Get_ViewPropertyValue("UserName");
				userPtr.FirstName	= base.Get_ViewPropertyValue("FirstName");
				userPtr.FamilyName	= base.Get_ViewPropertyValue("FamilyName");
				userPtr.UserRoles	= base.Get_ViewPropertyValue("UserRoles");
				userPtr.Balance		= float.Parse(base.Get_ViewPropertyValue("Balance"));
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