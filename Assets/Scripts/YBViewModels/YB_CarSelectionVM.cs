

using imady.NebuUI;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

namespace YBCarRental3D 
{
    //109 CarSelection - ListView
    public class YB_CarSelectionVM : YB_ViewModelListBasis<YB_Car>
	{
        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            var list = await YB_ManagerFactory.CarMgr.ListCars(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
#if DEVELOPMENT
            Debug.Log($"[Listcars Returned] : {list.Count()} cars");
#endif
            base.RenderListview(list);
        }

    };
}

