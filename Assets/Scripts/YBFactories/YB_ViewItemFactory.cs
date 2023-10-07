using imady.NebuUI.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEditorInternal.VersionControl;
using UnityEngine;

namespace YBCarRental3D
{
    public class YB_ViewItemFactory
    {
        Dictionary<int, YB_ViewItemBasis> viewitemPool = new Dictionary<int, YB_ViewItemBasis>();

        public YB_ViewItemBasis CreateViewItem(string itemDefinition)
        {
            YB_ViewItemBasis item = this.CreateProduct(itemDefinition);
            if (item != null)
            {
                if (viewitemPool.ContainsKey(item.Id))                              //Already existed; Update.
                {
                    viewitemPool[item.Id] = item;                                   //replace the viewitem in the pool
                }
                else
                {
                    try
                    {
                        viewitemPool.Add(item.Id, item);
                    }
                    catch
                    {
                    }
                }
            }
            return item;
        }

        public YB_ViewItemBasis GetViewItem(int viewId)
        {
            YB_ViewItemBasis newItem;
            if (viewitemPool.TryGetValue(viewId, out newItem))
                return newItem;
            else
                return null;
        }

        private YB_ViewItemBasis CreateProduct(string serializeString)
        {
            YB_ViewItemBasis newItem = new YB_ViewItemBasis(serializeString);

            if (newItem.ItemType == "ButtonItem")   { return new YB_ButtonItem  (serializeString); }
            if (newItem.ItemType == "TextItem")     { return new YB_TextItem    (serializeString); }
            if (newItem.ItemType == "InputItem")    { return new YB_InputItem   (serializeString); }
            if (newItem.ItemType == "ListItem")     { return new YB_ListItem    (serializeString); }
            if (newItem.ItemType == "ListHead")     { return new YB_ListHead    (serializeString); }
            if (newItem.ItemType == "MenuItem")     { return new YB_MenuItem    (serializeString); }
            return null;
        }
    }
}
