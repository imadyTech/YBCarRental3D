using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ViewBasis : YB_DataBasis, IEnumerable<YB_ViewItemBasis>
    {
        public string ViewType = "";
        public string Title = "";
        public int w = 120, h = 32;
        public char Background = '.';
        public string Source = "";                                  //datasource, viewmodel
        public string GotoView = "";
        public string ConfirmView = "";

        public GameObject viewObject;                               //The empty view frame 

        public IYB_DataSource dataSource;
        public List<YB_ViewItemBasis> bindableItems;                //Items affected by binding behaviour
        public List<YB_ViewItemBasis> subItemsList;
        private string serializeString;

        public YB_ViewBasis() : base()
        {
            subItemsList = new List<YB_ViewItemBasis>();
            bindableItems = new List<YB_ViewItemBasis>();
        }

        public YB_ViewBasis(string serializeString) : this()
        {
            this.serializeString = serializeString;
            this.Deserialize(this.serializeString);
        }

        public void Deserialize(string serializeString)
        {
            base.SplitLine(serializeString);

            if (!string.IsNullOrEmpty(FindValue("Id"))) base.Id = int.Parse(FindValue("Id"));
            if (!string.IsNullOrEmpty(FindValue("Title"))) Title = FindValue("Title");
            if (!string.IsNullOrEmpty(FindValue("ViewType"))) ViewType = FindValue("ViewType");
            if (!string.IsNullOrEmpty(FindValue("w"))) w = int.Parse(FindValue("w"));
            if (!string.IsNullOrEmpty(FindValue("h"))) h = int.Parse(FindValue("h"));
            if (!string.IsNullOrEmpty(FindValue("Source"))) Source = FindValue("Source");
            if (!string.IsNullOrEmpty(FindValue("ConfirmView"))) ConfirmView = FindValue("ConfirmView");
            if (!string.IsNullOrEmpty(FindValue("GotoView"))) GotoView = FindValue("GotoView");
        }

        #region === iterator of subitemList ===
        public IEnumerator<YB_ViewItemBasis> GetEnumerator()
        {
            return subItemsList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        public void Init()
        {
        }
        public void Bind()
        {

        }
        public void ReverseBind()
        {

        }
        public void Submit()
        {

        }
        public void Exit()
        {

        }

        public List<KeyValuePair<string, string>> FindValues(string key)
        {
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (var iterator in stringPairsMap)
            {
                if (iterator.Key.Contains(key))
                {
                    results.Add(iterator);
                }
            }
            return results;
        }


        #region === Configure the unity gameobject ===
        public virtual void ConfigViewItemObj(YB_ViewItemBasis itemDef, ref GameObject itemObj)
        {
            this.RescaleItem(itemDef, ref itemObj);
            this.RepositItem(itemDef, ref itemObj);
            this.SetItemContent(itemDef, ref itemObj);
            this.SetAction(itemDef, ref itemObj);
            
            //!!!! this is extremely important for view/viewModel automatical binding !!!!
            if (!string.IsNullOrEmpty(itemDef.Bind)) itemObj.name = itemDef.Bind;
        }

        public virtual void RescaleItem(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            //fit width
            var width = ((float)Screen.width * itemDef.w / this.w);
            var height = ((float)Screen.height * itemDef.h / this.h);
            itemObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)width, (float)height);
        }
        public virtual void RepositItem(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            //            systemLogContainer.GetComponent<RectTransform>().anchoredPosition3D = logViewCurrentPos += logItemHeight;//×Ô¶¯¹ö¶¯Ò»ÐÐ

            var x = ((float)Screen.width * (itemDef.x + itemDef.w/2 - this.w/2) / this.w);
            var y = Screen.height - ((float)Screen.height * itemDef.y / this.h);
            itemObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
        public virtual void SetItemContent(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            try
            {
                TMP_Text tmpText = itemObject.GetComponent<TMP_Text>();
                tmpText.enableWordWrapping = false;
                tmpText.text = itemDef.Content;
                if(itemDef.isCentral)
                    tmpText.alignment = TextAlignmentOptions.Center;
            }
            catch { }
        }
        public virtual void SetAction(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON)
                itemObject.GetComponent<Button>().onClick.AddListener(() => { YB_Window.Instance.Goto(itemDef.Link); });
        }
        public virtual void SetContent(YB_ViewItemBasis itemDef, ref GameObject itemObject)
        {
            if (itemDef.ItemType == YBGlobal.VIEWITEM_TYPE_BUTTON)
                itemObject.GetComponent<Text>().text = itemDef.Content;
        }
        #endregion


    }
}
