using imady.NebuUI;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ViewModelBasis<TData>: NebuUIViewBase, IYB_DataSource 
        where TData : YB_DataBasis
    {
        public YB_ViewBasis         viewDef;
        public TData                principalObject;

        public YB_Window            Window { get => YB_Window.Instance; }


        public YB_ViewModelBasis()
        {
        }


        public virtual string Get_PropertyValue(string bindName)
        {
            return principalObject?.FindValue(bindName);
        }

        public virtual List<YB_DataBasis> Get_QueryList(int page, int pageSize) {
            throw new NotImplementedException();
        }
        public virtual YB_DataBasis Get_PrincipalData() { return this.principalObject; }

        public virtual void onInit() { 
        }
        public virtual void onSubmit(Dictionary<string, string> valuesMapPtr){
        }
        public virtual void onViewForwarded(YB_DataBasis fromData)
        {
            this.principalObject = (TData)fromData;
        }
        public virtual void onContentUpdated(string bindName, string newValue)
        {

        }
        public virtual void onButtonClicked(string buttonName)
        { }

        public virtual void onYesClicked() { }
        public virtual void onNoClicked(){}
        public virtual void onOkClicked(){}



        #region === Configure the unity gameobject ===
        public virtual void ConfigViewDef(YB_ViewBasis view){
            this.viewDef = view;
        }

        public virtual void ConfigViewItemObj(YB_ViewItemBasis itemDef, ref GameObject itemObj)
        {
            itemDef.itemObject = itemObj;

            Rect viewContainer = viewDef.viewObject.GetComponent<RectTransform>().rect;

            this.RescaleItem(itemDef, ref itemObj, viewContainer);
            this.RepositItem(itemDef, ref itemObj, viewContainer);
            this.SetItemContent(itemDef, ref itemObj, viewContainer);
            this.SetAction(itemDef, ref itemObj);

            //!!!! this is extremely important for view/viewModel automatical binding !!!!
            if (!string.IsNullOrEmpty(itemDef.Bind)) itemObj.name = itemDef.Bind;
        }

        public virtual void RescaleItem(YB_ViewItemBasis itemDef, ref GameObject itemObject, Rect viewContainer)
        {
            //fit width
            var width = (viewContainer.width * itemDef.w / viewDef.w);
            var height = (viewContainer.height * itemDef.h / viewDef.h);
            itemObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)width, (float)height);
        }
        public virtual void RepositItem(YB_ViewItemBasis itemDef, ref GameObject itemObject, Rect viewContainer)
        {
            var x = viewContainer.width * (itemDef.x + (float)itemDef.w / 2 - (float)viewDef.w / 2) / (float)viewDef.w;
            var y = viewContainer.height * (0.5f - (float)itemDef.y / (float)viewDef.h);
            itemObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
        public virtual void SetItemContent(YB_ViewItemBasis itemDef, ref GameObject itemObject, Rect viewContainer)
        {
            itemDef.OnBind(itemDef.Content);
        }
        public virtual void SetAction(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON)
                itemObject.GetComponent<Button>().onClick.AddListener(() => { YB_Window.Instance.Goto(itemDef.Link); });
        }
        #endregion


    }
}