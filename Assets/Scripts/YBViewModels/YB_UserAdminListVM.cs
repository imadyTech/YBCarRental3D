
namespace YBCarRental3D
{


    //116 YB_UserAdminListVM - ListView
    public class YB_UserAdminListVM : YB_ViewModelBasis<YB_User>
    {
        public YB_UserAdminListVM() : base(){
        }


        YB_User carryForwardedUser;

        //DO NOT carryForward current principalObject, this confuse with the current user (Admin) of the current view
        public override void onViewForwarded(YB_DataBasis fromData)
        {
            this.carryForwardedUser = (YB_User)fromData;
        }
    };
}

