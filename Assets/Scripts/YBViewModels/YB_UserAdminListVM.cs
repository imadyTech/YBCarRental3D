
namespace YBCarRental3D
{


    //116 YB_UserAdminListVM - ListView
    public class YB_UserAdminListVM : YB_ViewModelListBasis<YB_User>
    {
        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            var list = await YB_ManagerFactory.UserMgr.ListUsers(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    };
}

