
namespace YBCarRental3D
{
    //111 YB_CarListVM - List cars for updating
    public class YB_CarListVM : YB_ViewModelListBasis<YB_Car>
    {
        public YB_CarListVM() : base()
        {
        }

        private int _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }


        public async override void onInit(YB_Window window)
        {
            base.onInit(window);
            base.CreateListHead();

            var list = await YB_ManagerFactory.CarMgr.ListCars(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    }
}

