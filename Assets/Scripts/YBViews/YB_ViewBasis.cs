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

            if (HasValue("Id")) base.Id = int.Parse(FindValue("Id"));
            if (HasValue("Title")) Title = FindValue("Title");
            if (HasValue("ViewType")) ViewType = FindValue("ViewType");
            if (HasValue("w")) w = int.Parse(FindValue("w"));
            if (HasValue("h")) h = int.Parse(FindValue("h"));
            if (HasValue("Source")) Source = FindValue("Source");
            if (HasValue("ConfirmView")) ConfirmView = FindValue("ConfirmView");
            if (HasValue("GotoView")) GotoView = FindValue("GotoView");

#if DEVELOPMENT
            Debug.Log($"[Deserialized ViewDef] : {base.Id}");
#endif
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
