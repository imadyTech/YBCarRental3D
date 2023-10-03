namespace YBCarRental3D
{

    //106 AdminMenu
    public class YB_AdminMenuVM : YB_ViewModelBasis<YB_User>
    {
        public YB_AdminMenuVM() : base()
        {
        }

        public override void onViewForwarded(YB_DataBasis fromData)
        {
            principalObject = YB_ManagerFactory.UserMgr.CurrentUser();
        }
    };
}

