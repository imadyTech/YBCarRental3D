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
        public GameObject viewTemplate;
        public GameObject viewItemTemplate;
        public GameObject buttonItemTemplate;
        public GameObject textItemTemplate;
        public GameObject listHeadTemplate;
        public GameObject listItemTemplate;
        public GameObject inputItemTemplate;
        public GameObject menuItemTemplate;

        YB_ViewFactory viewFactory;
        YB_LogicFactory logicFactory;
        YB_ViewBasis currentView;
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
            if (viewPtr == null) viewPtr = viewFactory.GetView(YBGlobal.ERROR_VIEW);
            currentView = viewPtr;

            this.viewHistoryStack.Push(currentView);
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
                GameObject view = Instantiate(viewTemplate);
                view.transform.SetParent(this.gameObject.transform);
                foreach (var viewItemDef in viewDef)
                {
                    var item = this.GenerateViewItemObject(viewItemDef, viewDef);
                    if (item != null)
                    {
                        item.transform.SetParent(view.transform);
                    }
                }
                view.name = viewDef.Title;
                view.SetActive(false);
            }
        }

        private GameObject GenerateViewItemObject(YB_ViewItemBasis itemDef, YB_ViewBasis parentViewDef)
        {
            GameObject viewItemObj = viewItemTemplate;
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON) viewItemObj = Instantiate(buttonItemTemplate);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_HEAD) viewItemObj = Instantiate(listHeadTemplate);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_INPUT) viewItemObj = Instantiate(inputItemTemplate);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_LISTITEM) viewItemObj = Instantiate(listItemTemplate);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_TEXT) viewItemObj = Instantiate(textItemTemplate);
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_MENUITEM) viewItemObj = Instantiate(menuItemTemplate);

            this.RescaleItem(parentViewDef, itemDef, ref viewItemObj);
            this.RepositItem(parentViewDef, itemDef, ref viewItemObj);
            this.SetItemContent(itemDef, ref viewItemObj);
            return viewItemObj;
        }

        private void RescaleItem(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            //fit width
            var width = ((float)Screen.width * itemDef.w / parentViewDef.w);
            var height = ((float)Screen.height * itemDef.h / parentViewDef.h);
            itemObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)width, (float)height);
        }
        private void RepositItem(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            //            systemLogContainer.GetComponent<RectTransform>().anchoredPosition3D = logViewCurrentPos += logItemHeight;//×Ô¶¯¹ö¶¯Ò»ÐÐ

            var x = Screen.width - ((float)Screen.width * itemDef.x / parentViewDef.w);
            var y = Screen.height - ((float)Screen.height * itemDef.y / parentViewDef.h);
            itemObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
        private void SetItemContent(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            try
            {
                TMP_Text tmpText = itemObject.GetComponent<TMP_Text>();
                tmpText.enableWordWrapping = false;
                tmpText.text = itemDef.Content;
            }
            catch { }
        }
        private void SetAction(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON)
                itemObject.GetComponent<Button>().onClick.AddListener(() => { this.Goto(itemDef.Link); });
        }
        private void CreateListItem(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {

        }
        private void CreateListHead(YB_ViewBasis parentViewDef, YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {

        }
        private void SetContent(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON)
                itemObject.GetComponent<Text>().text = itemDef.Content;
        }
    }

}

