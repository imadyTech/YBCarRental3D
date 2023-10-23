

using Unity.VisualScripting;
using UnityEngine;

namespace YBCarRental3D
{
    //117 YB_CarDeleteListVM- List cars for deleting
    public class YB_CarDeleteListVM : YB_ViewModelListBasis<YB_Car>
    {
        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            var list = await YB_ManagerFactory.CarMgr.ListCars(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            this.RenderListview(list);
        }
    };
}

