
namespace YBCarRental3D
{
    //115 YB_OrderManageVM - ListView
    public class YB_OrderManageVM : YB_ViewModelBasis<YB_Rent>
    {
        public YB_OrderManageVM() : base()
        {
        }

        YB_User carryForwardedUser;

        public override void onViewForwarded(YB_DataBasis fromData)
        {
            carryForwardedUser = (YB_User)fromData;
        }
    };
}

