using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ViewBasis : YB_DataBasis, IEnumerable<YB_ViewItemBasis>
    {
        public string   ViewType = "";
        public string   Title = "";
        public int      w = 120, h = 32;
        public char     Background = '.';
        public string   Source = "";                                    //=datasource, viewmodel
        public string   GotoView = "";
        public string   ConfirmView = "";

        public GameObject               viewObject;                     //The empty view frame 
        public I_YB_ViewModel           viewModel;
        public List<YB_ViewItemBasis>   bindableItems;                  //Items affected by binding behaviour
        public List<YB_ViewItemBasis>   subItemsList;

        protected string                serializeString;

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

        public virtual void Deserialize(string serializeString)
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
            this.viewObject.SetActive(false);
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

        public virtual List<KeyValuePair<string, string>> GetItemDefStrings()
        {
            //Base view only return def with '<col>'
            //Implement more in derived view
            return this.FindValues(YBGlobal.CONST_VIEW_ITEM_STARTER);
        }
    }
}
