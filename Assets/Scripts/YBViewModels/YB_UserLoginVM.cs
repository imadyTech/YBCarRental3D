
using imady.NebuEvent;
using System.Collections.Generic;
using UnityEngine;

namespace YBCarRental3D
{
    /// 102 - LoginView
    public class YB_UserLoginVM : YB_ViewModelBasis<YB_User>, INebuProvider<NebuDataMessage<string>>
    {
        public async override void onSubmit()
        {
            var uName = base.Get_ViewPropertyValue("UserName");
            var uPsd  = base.Get_ViewPropertyValue("Password");
            if (string.IsNullOrEmpty(uPsd) || string.IsNullOrEmpty(uName))
            {
                base.ybWindow.PopPrompt(this.viewDef.Title, "Please input valid username and password.");
                return;
            }

            var loginResult =await YB_ManagerFactory.UserMgr.UserLogin(uName, uPsd);
            this.principalObject = YB_ManagerFactory.UserMgr.CurrentUser;

#if DEVELOPMENT
            Debug.Log($"[Login Returned] : {YB_ManagerFactory.UserMgr.CurrentUser?.UserName}");
#endif

            if (YB_ManagerFactory.UserMgr.CurrentUser != null && !YB_ManagerFactory.UserMgr.IsAdmin())
            {
                NotifyObservers(new NebuDataMessage<string>("userLogged"));//connect with 3D scene objects

                base.ybWindow.Goto(YBGlobal.USER_MAIN_VIEW);
            }
            else if (YB_ManagerFactory.UserMgr.CurrentUser != null && YB_ManagerFactory.UserMgr.IsAdmin())
            {
                NotifyObservers(new NebuDataMessage<string>("adminLogged"));//connect with 3D scene objects

                base.ybWindow.Goto(YBGlobal.ADMIN_MAIN_VIEW);
            }
            else
                base.ybWindow.PopPrompt(this.viewDef.Title, "Login have encounted some error, check your connection or contact administrator for server status.");
        }

        public override void onExit()
        {
            base.Set_ViewPropertyValue("UserName", string.Empty);
            base.Set_ViewPropertyValue("Password", string.Empty);

            base.onExit();
        }
    }
}

