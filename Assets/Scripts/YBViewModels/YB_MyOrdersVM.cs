
namespace YBCarRental3D {

    //108 My orders - ListView
    public class YB_MyOrdersVM : YB_ViewModelListBasis<YB_Rent>
	{
        private int _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }

        public YB_MyOrdersVM() :base() {
		}


        public async override void onInit(YB_Window window)
        {
            base.onInit(window);
            base.CreateListHead();

            var list = await YB_ManagerFactory.RentMgr.ListOrders(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }

    }
}

