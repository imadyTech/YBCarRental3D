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
        public GameObject viewTemplate;           //unique for all views
        public GameObject viewItemTemplate;       //empty temp template
        public GameObject buttonItemTemplate;
        public GameObject textItemTemplate;
        public GameObject listHeadTemplate;
        public GameObject listItemTemplate;
        public GameObject inputItemTemplate;
        public GameObject menuItemTemplate;

        public GameObject listHeadColTemplate;
        public GameObject listItemColTemplate;

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

            this.GenerateViewContainers();
            this.Goto(YBGlobal.INIT_VIEW);

            return this;
        }
        private void GenerateViewContainers()
        {
            foreach (var viewDef in viewFactory)
            {
                Debug.Log($"[Generate View] : {viewDef.Title}");
                //create view object
                viewDef.viewObject = Instantiate(viewTemplate, this.gameObject.transform);
                viewDef.viewObject.SetActive(false);
                viewDef.viewObject.name = viewDef.Title;

                //get and attach the viewmodel
                if (!string.IsNullOrEmpty(viewDef.Source))
                {
                    Type datasourceType = logicFactory.FindViewModel(viewDef.Source);
                    if (datasourceType != null)
                    {
                        viewDef.viewModel = viewDef.viewObject.AddComponent(datasourceType) as I_YB_ViewModel;
                        viewDef.viewModel.SetViewDef(viewDef);
                        viewDef.viewModel.onInit(this);
                    }
                }
            }
        }
        public GameObject GenerateViewItemTemplate(YB_ViewItemBasis itemDef, Transform parentViewObj)
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

        public void Goto(YB_ViewBasis viewPtr)
        {
            YB_ViewBasis previousView = currentView;

            if (currentView != null && currentView.viewObject != null)
            //currentView.viewObject.SetActive(false);
                currentView.viewModel.onExit();

            if (viewPtr == null)
                currentView = viewFactory.GetView(YBGlobal.ERROR_VIEW);
            else
                currentView = viewPtr;

            Debug.Log($"[Window.Goto ]: {currentView.Title}");
            currentView.viewObject.SetActive(true);
            if (previousView != null)
            {
                currentView.viewModel.onViewForwarded(previousView);
            }
        }
        public void Goto(string viewTitle)
        {
            if (!string.IsNullOrEmpty(viewTitle))
            {
                this.viewHistoryStack.Push(currentView);
                this.Goto(viewFactory.GetView(viewTitle));
            }
        }
        public void Back()
        {
            if (this.viewHistoryStack?.Count > 1)
            {
                var previous = this.viewHistoryStack.Pop();
                this.Goto(previous);
            }
        }
        public void PopPrompt(string title, string msg, string gotoView)
        {
            //throw new NotImplementedException();
            App.Instance.uiManager.ShowMessageBox(title, msg, () => { this.Goto(gotoView); });
        }
        public void PopPrompt(string title, string msg)
        {
            //throw new NotImplementedException();
            App.Instance.uiManager.ShowMessageBox(title, msg);
        }


        private float timePassed;
        public void Update()
        {
            timePassed += Time.deltaTime;

            if (Input.GetKey(KeyCode.Escape) &&  timePassed >= 0.2f) {
                // do stuff
                timePassed = 0f;
                this.Back();
            }
        }
    }
}

