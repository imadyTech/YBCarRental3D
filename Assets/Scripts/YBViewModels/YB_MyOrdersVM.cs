
namespace YBCarRental3D {

    //108 My orders - ListView
    public class YB_MyOrdersVM : YB_ViewModelListBasis<YB_Rent>
	{
        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            var list = await YB_ManagerFactory.RentMgr.ListOrders(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }

    }
}

