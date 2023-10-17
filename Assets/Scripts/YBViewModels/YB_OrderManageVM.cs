
namespace YBCarRental3D
{
    //115 YB_OrderManageVM - List all orders for admin
    public class YB_OrderManageVM : YB_ViewModelListBasis<YB_Rent>
    {
        public YB_OrderManageVM() : base()
        {
        }

        private int _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }



        public async override void onInit(YB_Window window)
        {
            base.onInit(window);
            base.CreateListHead();

            var list = await YB_ManagerFactory.RentMgr.ListOrders(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }

    };
}

