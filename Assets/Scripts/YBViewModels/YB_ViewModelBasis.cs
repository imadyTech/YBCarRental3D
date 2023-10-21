﻿using imady.NebuUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ViewModelBasis<TData> : NebuEventUnityObjectBase, I_YB_ViewModel
        where TData : YB_DataBasis
    {
        public YB_ViewBasis previousView;
        public YB_ViewBasis viewDef;                    //View Definition object
        protected TData principalObject;                   //Logic DTO (Data Transfer Object)
        private Dictionary<string, Func<string>> valuesMapPtr;

        protected YB_Window ybWindow { get => YB_Window.Instance; }
        public YB_DataBasis PrincipalData
        {
            get => this.principalObject;
            set => this.principalObject = value as TData;
        }

        public YB_ViewModelBasis()
        {
        }
        protected override void Awake()
        {
            base.Awake();
        }

        public virtual bool Has_PropertyValue(string propertyName)
        {
            return valuesMapPtr.ContainsKey(propertyName);
        }
        public virtual string Get_PropertyValue(string bindName)
        {
            valuesMapPtr.TryGetValue(bindName, out var valueGetter);  //retrieve the input gameobject and return the value
            if (valueGetter != null)
            {
                return valueGetter.Invoke();
            }
            return string.Empty;
        }
        public virtual List<YB_DataBasis> Get_QueryList(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
        //public virtual YB_DataBasis Get_PrincipalData() { return this.principalObject; }


        //must use the YB_Window passed in here, as the static instance of YB_Window is not yet existing at this moment.
        public virtual void onInit(YB_Window window)
        {
            if (valuesMapPtr == null)
                valuesMapPtr = new Dictionary<string, Func<string>>();
            else
                valuesMapPtr.Clear();

            //generate and config item objects one-by-one (through viewModel's configuring method)
            foreach (var viewItemDef in viewDef)
            {
                Debug.Log($"[Generating ViewItem] : {viewItemDef.Id}, {viewItemDef.ItemType}");
                var item = window.GenerateViewItemTemplate(viewItemDef, this.transform);
                this.ConfigViewItemObj(viewItemDef, ref item);
            }
        }
        public virtual void onSubmit()
        {
        }
        public virtual void onViewForwarded(YB_ViewBasis fromView)
        {
            this.previousView = fromView;
        }
        public virtual void onContentUpdated(string bindName, string newValue)
        {
        }
        public virtual void OnButtonClicked(YB_ViewItemBasis button)
        {
            //the function in basis doing nothing.
            //override this method in derived VM
            Debug.Log(button.Content);

            //if (button.ButtonType == YBGlobal.Button_Type_Submit)
            //{
            //}
            //if (button.ButtonType == YBGlobal.Button_Type_Yes)
            //{
            //}
            //if (button.ButtonType == YBGlobal.Button_Type_No)
            //{
            //}
            //if (button.ButtonType == YBGlobal.Button_Type_Ok)
            //{
            //}
        }
        public virtual void onYesClicked() { }
        public virtual void onNoClicked() { }
        public virtual void onOkClicked() { }



        #region === Configure the unity gameobject ===
        public virtual void SetViewDef(YB_ViewBasis view)
        {
            this.viewDef = view;
        }
        protected virtual void ConfigViewItemObj(YB_ViewItemBasis itemDef, ref GameObject itemObj)
        {
            itemDef.itemGameObject = itemObj;

            Rect viewContainer = viewDef.viewObject.GetComponent<RectTransform>().rect;

            this.RescaleItem(itemDef, ref itemObj, viewContainer);
            this.RepositItem(itemDef, ref itemObj, viewContainer);
            itemDef
                .BindContent()
                .BindAction()
                .ReverseBind(this.valuesMapPtr);

            //!!!! this is extremely important for view/viewModel automatical binding !!!!
            if (!string.IsNullOrEmpty(itemDef.Bind)) itemObj.name = itemDef.Bind;
        }
        protected virtual void RescaleItem(YB_ViewItemBasis itemDef, ref GameObject itemObject, Rect viewContainer)
        {
#if DEVELOPMENT
            Debug.Log($"[RescaleItem] {itemDef.ItemType} {itemDef.Id}");
#endif
            //fit width
            var width = (viewContainer.width * itemDef.w / viewDef.w);
            var height = (viewContainer.height * itemDef.h / viewDef.h);
            itemObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)width, (float)height);
        }
        protected virtual void RepositItem(YB_ViewItemBasis itemDef, ref GameObject itemObject, Rect viewContainer)
        {
#if DEVELOPMENT
            Debug.Log($"[RepositItem] {itemDef.ItemType} {itemDef.Id}");
#endif
            var x = viewContainer.width * (itemDef.x + (float)itemDef.w / 2 - (float)viewDef.w / 2) / (float)viewDef.w;
            var y = viewContainer.height * (0.5f - (float)itemDef.y / (float)viewDef.h);
            itemObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
        #endregion


        #region === Bind/Render view with VM === 
        protected virtual GameObject RenderView()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            List<PropertyInfo> propertiesWithAttribute = properties
                .Where(property => Attribute.IsDefined(property, typeof(NbuViewPropertyAttribute)))
                .ToList();
            foreach (PropertyInfo property in propertiesWithAttribute)
            {
#if DEVELOPMENT
                Debug.Log($"[VM Rendering] : PropertyInfo {property.Name}");
#endif
                try
                {
                    BindingText(this.gameObject, property.Name, property.GetValue(this).ToString());
                    //Debug.Log($"[ViewBasis] {property.Name}={property.GetValue(this)}");
                }
                catch (Exception e)
                {
                    Debug.Log($"[Binding Error] : {this.gameObject.name}, {property.Name}");
                }

                void BindingText(GameObject gameObjectRoot, string filedName, string content)
                {
                    TMP_Text text;
                    gameObjectRoot.TryGetComponent<TMP_Text>(out text);
                    if (text != null && !String.IsNullOrEmpty(text.name) && text.name == filedName)
                        text.SetText(content);
                    foreach (Transform child in gameObjectRoot.transform)
                    {
                        BindingText(child.gameObject, filedName, content);
                    }
                }
            }
            return this.gameObject;
        }

        //protected   virtual GameObject RenderView(GameObject template, object viewmodel)
        //{
        //    var properties = viewmodel.GetType().GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        try
        //        {
        //            if (property.PropertyType.IsPrimitive || property.PropertyType.IsValueType || property.PropertyType == typeof(string))
        //            {
        //                var temp = property.GetValue(viewmodel).ToString();
        //                var temp0 = property.Name;
        //                BindingText(template, property.Name, property.GetValue(viewmodel).ToString());

        //            }
        //            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
        //                BindCombo(template, property.GetValue(viewmodel));
        //        }
        //        catch (Exception e)
        //        {
        //            //Debug.LogWarning($"View Rendering Error: {property.Name}: {e}");
        //        }
        //    }
        //    return template;
        //}

        //protected   void BindingText(GameObject targetGameObject, string fieldName, string content)
        //{
        //    var text = targetGameObject.GetComponent<Text>();
        //    if (text != null && !String.IsNullOrEmpty(text.name) && text.name == fieldName)
        //        text.text = content;
        //    foreach (Transform child in targetGameObject.transform)
        //    {
        //        BindingText(child.gameObject, fieldName, content);
        //    }
        //}

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        protected virtual void ClearLayoutContainer(LayoutGroup container)
        {
            for (int i = 0; i < container.transform.childCount; i++)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }
        }

        public virtual void Refresh(GameObject parent)
        {
            Transform transform;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                transform = parent.transform.GetChild(i);
                GameObject.Destroy(transform.gameObject);
            }
        }
        #endregion
    }
}