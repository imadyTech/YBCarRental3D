
using System.Collections.Generic;

namespace YBCarRental3D
{
    /// 102 - LoginView
    public class YB_UserLoginVM : YB_ViewModelBasis<YB_User>
    {
        public YB_UserLoginVM() : base()
        {
        }

        public async override void onSubmit()
        {
            var uName = base.Get_PropertyValue("UserName");
            var uPsd  = base.Get_PropertyValue("Password");
            if (string.IsNullOrEmpty(uPsd) || string.IsNullOrEmpty(uName))
            {
                base.ybWindow.PopPrompt(this.viewDef.Title, "Please input valid username and password.");
                return;
            }

            var loginResult =await YB_ManagerFactory.UserMgr.UserLogin(uName, uPsd);
            this.principalObject = YB_ManagerFactory.UserMgr.CurrentUser;

            if (YB_ManagerFactory.UserMgr.CurrentUser !=null && !YB_ManagerFactory.UserMgr.IsAdmin())
                base.ybWindow.Goto(YBGlobal.USER_MAIN_VIEW);
            else if (YB_ManagerFactory.UserMgr.CurrentUser != null && YB_ManagerFactory.UserMgr.IsAdmin())
                base.ybWindow.Goto(YBGlobal.ADMIN_MAIN_VIEW);
            else
                base.ybWindow.PopPrompt(this.viewDef.Title, "Login have encounted some error, check your connection or contact administrator for server status.");
        }

        public override void OnButtonClicked(YB_ViewItemBasis button)
        {
            if ((button as YB_ButtonItem).ButtonType == YBGlobal.Button_Type_Submit)
                this.onSubmit();
        }

    }
}

