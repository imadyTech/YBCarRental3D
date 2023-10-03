

namespace YBCarRental3D {
    //105 UserMenu
    public class YB_UserMenuVM : YB_ViewModelBasis<YB_User>
	{
        public YB_UserMenuVM():base() {
		}

        public override void onViewForwarded(YB_DataBasis fromData)		
		{
			this.principalObject = YB_ManagerFactory.UserMgr.CurrentUser();
		}
	}
}

