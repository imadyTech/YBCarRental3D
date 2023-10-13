

namespace YBCarRental3D {
    //105 UserMenu
    public class YB_UserMenuVM : YB_ViewModelBasis<YB_User>
	{
        public YB_UserMenuVM():base() {
		}
        public string UserName => YB_ManagerFactory.UserMgr.CurrentUser.UserName;
        public string UserRoles => YB_ManagerFactory.UserMgr.CurrentUser.UserRoles;
        public override void onInit(YB_Window window)
        {
            base.onInit(window);
        }

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            base.onViewForwarded(fromView);
            base.RenderView();

        }

    }
}

