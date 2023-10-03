

namespace YBCarRental3D
{
    //117 YB_CarDeleteListVM- ListView
    public class YB_CarDeleteListVM : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarDeleteListVM() : base()
        {
        }

        YB_User carryForwardedUser;
        public override void onViewForwarded(YB_DataBasis fromData)
        {
            carryForwardedUser = (YB_User)fromData;
        }
    };
}

