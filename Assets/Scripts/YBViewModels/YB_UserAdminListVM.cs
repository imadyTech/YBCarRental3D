
namespace YBCarRental3D
{


    //116 YB_UserAdminListVM - ListView
    public class YB_UserAdminListVM : YB_ViewModelListBasis<YB_User>
    {
        public YB_UserAdminListVM() : base(){
        }

        private int _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }

        public async override void onInit(YB_Window window)
        {
            base.onInit(window);
            base.CreateListHead();

            var list = await YB_ManagerFactory.UserMgr.ListUsers(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    };
}

