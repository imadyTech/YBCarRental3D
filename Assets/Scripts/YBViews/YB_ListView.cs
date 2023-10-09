using imady.NebuUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ListView : YB_ViewBasis
    {


        public void CreateListItem(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {

        }
        public void CreateListHead(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {

        }
        public virtual List<INebuUIView> RenderListview(IEnumerable<object> sourceVMCollection, Type template, LayoutGroup container, Func<Type, GameObject, bool, INebuUIView> resourceLoader)
        {
            //ClearLayoutContainer(container);
            var resultViewList = new List<INebuUIView>();

            foreach (object item in sourceVMCollection)
            {
                var viewItem = resourceLoader.Invoke(template, container.gameObject, true);// true: create new viewitem instance
                viewItem.SetViewModel(item);
                resultViewList.Add(viewItem);
            }
            return resultViewList;
        }

        /// <summary>
        /// ¸ù¾ÝÌá¹©µÄViewModelÊý¾ÝÔ´£¬¶ÔÒ»¸ö¿ÕµÄListViewÈÝÆ÷½øÐÐäÖÈ¾¡£
        /// </summary>
        /// <typeparam name="T">type of a certain ViewModel</typeparam>
        /// <param name="sourceVMs">Êý¾ÝÔ´VM¼¯ºÏ£¬VMÏîÐè´øÓÐLeeViewModelTypeAttribute±êÇ©Ö¸Ã÷ViewÄ£°å</param>
        /// <param name="container">ListViewÈÝÆ÷</param>
        /// <param name="resultViewList">äÖÈ¾ºóµÄ½á¹û</param>
        /// <returns></returns>
        protected virtual List<INebuUIView> RenderListview<T>(List<T> sourceVMs, LayoutGroup container, Func<Type, GameObject, bool, INebuUIView> resourceLoader)
        {
            //ClearLayoutContainer(container);
            var resultViewList = new List<INebuUIView>();

            foreach (T item in sourceVMs)
            {
                var viewtype = item.GetType().GetCustomAttribute<NbuViewModelTypeAttribute>().NbuViewType;
                var viewItem = resourceLoader.Invoke(viewtype, container.gameObject, true);// true: create new viewitem instance
                viewItem.SetViewModel(item);
                resultViewList.Add(viewItem);
            }
            return resultViewList;
        }

    }
}
