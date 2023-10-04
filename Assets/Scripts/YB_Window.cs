using imady.NebuUI;
using imady.NebuUI.Samples;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_Window : NebuSingleton<YB_Window>
    {
        public GameObject   viewTemplate;
        public GameObject   viewItemTemplate;
        public GameObject   buttonItemTemplate;
        public GameObject   textItemTemplate;
        public GameObject   listHeadTemplate;
        public GameObject   listItemTemplate;
        public GameObject   inputItemTemplate;
        public GameObject   menuItemTemplate;

        YB_ViewFactory      viewFactory;
        YB_LogicFactory     logicFactory;
        YB_ViewBasis        currentView;
        Stack<YB_ViewBasis> viewHistoryStack;

        public YB_Window ConfigLogicFactory(YB_LogicFactory factory)
        {
            this.logicFactory = factory;
            return this;
        }
        public YB_Window ConfigViewFactory(YB_ViewFactory factory)
        {
            this.viewFactory = factory;
            return this;
        }
        public YB_Window Init()
        {
            viewHistoryStack = new Stack<YB_ViewBasis>();

            this.GenerateViews();
            this.Goto(YBGlobal.INIT_VIEW);

            return this;
        }

        public void Goto(YB_ViewBasis viewPtr)
        {
            if (currentView != null && currentView.viewObject != null)
                currentView.viewObject.SetActive(false);

            if (viewPtr == null)
                currentView = viewFactory.GetView(YBGlobal.ERROR_VIEW);
            else
                currentView = viewPtr;

            this.viewHistoryStack.Push(currentView);
            currentView.viewObject.SetActive(true);
        }
        public void Goto(int viewId)
        {
            this.Goto(viewFactory.GetView(viewId));
        }
        public void Goto(string viewTitle)
        {
            this.Goto(viewFactory.GetView(viewTitle));
        }
        public void Back()
        {
            if (this.viewHistoryStack?.Count > 1)
            {
                this.currentView.Exit();
                this.currentView = this.viewHistoryStack.Pop();
            }
        }
        public void PopPrompt(string v, object value)
        {
            throw new NotImplementedException();
        }

        private void GenerateViews()
        {
            foreach (var viewDef in viewFactory)
            {
                viewDef.viewObject = Instantiate(viewTemplate, this.gameObject.transform);
                foreach (var viewItemDef in viewDef)
                {
                    var item = this.GenerateViewItemObject(viewItemDef, viewDef.viewObject.transform);
                    viewDef.ConfigViewItemObj(viewItemDef, ref item);
                }
                viewDef.viewObject.name = viewDef.Title;
                viewDef.viewObject.SetActive(false);

                if (!string.IsNullOrEmpty(viewDef.Source))
                {
                    Type datasourceType = logicFactory.FindDataSource(viewDef.Source).GetType();
                    if (datasourceType != null) viewDef.viewObject.AddComponent(datasourceType);
                }
            }
        }

        private GameObject GenerateViewItemObject(YB_ViewItemBasis itemDef, Transform parentViewObj)
        {
            GameObject viewItemObj = viewItemTemplate;
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON) viewItemObj = Instantiate(buttonItemTemplate, parentViewObj);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_HEAD) viewItemObj = Instantiate(listHeadTemplate, parentViewObj);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_INPUT) viewItemObj = Instantiate(inputItemTemplate, parentViewObj);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_LISTITEM) viewItemObj = Instantiate(listItemTemplate, parentViewObj);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_TEXT) viewItemObj = Instantiate(textItemTemplate, parentViewObj);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_MENUITEM) viewItemObj = Instantiate(menuItemTemplate, parentViewObj);

            return viewItemObj;
        }
    }

}

