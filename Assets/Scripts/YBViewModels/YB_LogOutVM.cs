

using System.Collections.Generic;

namespace YBCarRental3D
{

    //103 - RegisterView
    public class YB_LogOutVM : YB_ViewModelBasis<YB_User>
    {
        public YB_LogOutVM() : base() { }

        public override string Get_PropertyValue(string bindName)
        {
            string value = string.Empty;
            if (bindName == "SeeYou")
            {
                value = "See ya, " + YB_ManagerFactory.UserMgr.CurrentUser().FirstName;
                return value;
            }
            return base.Get_PropertyValue(bindName);
        }


        public override void onSubmit()
        {
            YB_ManagerFactory.UserMgr.UserLogout();
        }
    };


}

