

using UnityEngine;

namespace YBCarRental3D
{
    //117 YB_CarDeleteListVM- List cars for deleting
    public class YB_CarDeleteListVM : YB_ViewModelListBasis<YB_Car>
    {
        public YB_CarDeleteListVM() : base()
        {
        }

        YB_User carryForwardedUser;//This is view about car, but came from adminMenuVM, so as to keep the user

        private int _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }


        public async override void onInit(YB_Window window)
        {
            base.onInit(window);
            base.CreateListHead();

            var list = await YB_ManagerFactory.CarMgr.ListCars(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    };
}

