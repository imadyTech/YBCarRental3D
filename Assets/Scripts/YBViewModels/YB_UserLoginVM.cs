
using System.Collections.Generic;

namespace YBCarRental3D
{
    /// 102 - LoginView
    public class YB_UserLoginVM : YB_ViewModelBasis<YB_User>
    {
        public YB_UserLoginVM() : base()
        {
        }

        public override void onSubmit()
        {
            var uName = base.Get_PropertyValue("UserName");
            var uPsd  = base.Get_PropertyValue("Password");
            if (string.IsNullOrEmpty(uPsd) || string.IsNullOrEmpty(uName))
            {
                base.ybWindow.PopPrompt(this.viewDef.Title, "Please input valid username and password.");
                return;
            }

            bool loginResult = YB_ManagerFactory.UserMgr.UserLogin(uName, uPsd);
            this.principalObject = YB_ManagerFactory.UserMgr.CurrentUser();

            if (loginResult && !YB_ManagerFactory.UserMgr.IsAdmin())
                base.ybWindow.Goto(YBGlobal.USER_MAIN_VIEW);
            else if (loginResult && YB_ManagerFactory.UserMgr.IsAdmin())
                base.ybWindow.Goto(YBGlobal.ADMIN_MAIN_VIEW);
        }

        public override void OnButtonClicked(YB_ButtonItem button)
        {
            if (button.ButtonType == YBGlobal.Button_Type_Submit)
                this.onSubmit();
        }

    }
}

