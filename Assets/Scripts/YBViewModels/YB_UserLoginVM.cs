
using System.Collections.Generic;

namespace YBCarRental3D {
    /// 102 - LoginView
    public class YB_UserLoginVM : YB_ViewModelBasis<YB_User>
	{
        public YB_UserLoginVM( ) :base (){
		}

        public override void onSubmit(Dictionary<string, string> valuesMapPtr)				
		{
			string uName = "";
			string uPsd = "";
			valuesMapPtr.TryGetValue("UserName", out uName);
			valuesMapPtr.TryGetValue("Password" ,out uPsd);

			bool loginResult = YB_ManagerFactory.UserMgr.UserLogin(uName, uPsd);
			this.principalObject = YB_ManagerFactory.UserMgr.CurrentUser();

			if (loginResult && !YB_ManagerFactory.UserMgr.IsAdmin())
				Window.Goto(YBGlobal.USER_MAIN_VIEW);			//Todo: this shall be view.GotoView
			else if (loginResult && YB_ManagerFactory.UserMgr.IsAdmin())
				Window.Goto(YBGlobal.ADMIN_MAIN_VIEW);
		}
	}
}

