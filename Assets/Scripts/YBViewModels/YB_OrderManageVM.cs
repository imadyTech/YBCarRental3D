
namespace YBCarRental3D
{
    //115 YB_OrderManageVM - List all orders for admin
    public class YB_OrderManageVM : YB_ViewModelListBasis<YB_Rent>
    {
        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            var list = await YB_ManagerFactory.RentMgr.ListOrders(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    };
}

