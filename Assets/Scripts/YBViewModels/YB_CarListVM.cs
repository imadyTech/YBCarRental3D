
namespace YBCarRental3D
{
    //111 YB_CarListVM - ListView
    public class YB_CarListVM : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarListVM() : base()
        {
        }

        YB_User carryForwardedUser;

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
        }
    }
}

