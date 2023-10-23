
namespace YBCarRental3D
{
    //111 YB_CarListVM - List cars for updating
    public class YB_CarListVM : YB_ViewModelListBasis<YB_Car>
    {
        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            var list = await YB_ManagerFactory.CarMgr.ListCars(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    }
}

