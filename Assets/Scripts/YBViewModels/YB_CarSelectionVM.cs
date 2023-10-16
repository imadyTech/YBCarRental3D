

using imady.NebuUI;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace YBCarRental3D 
{
    //109 CarSelection - ListView
    public class YB_CarSelectionVM : YB_ViewModelListBasis<YB_Car>
	{
        private int _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }

		public YB_CarSelectionVM() :base() {
		}

        public async override void onInit(YB_Window window)
        {
            base.onInit(window);
            base.CreateListHead();

            var list = await YB_ManagerFactory.CarMgr.ListCars(CurrentPage, (base.viewDef as YB_ListView).ListRowCount);
            base.RenderListview(list);
        }
    };
}

