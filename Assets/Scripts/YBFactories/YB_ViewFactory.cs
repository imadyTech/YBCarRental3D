using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace YBCarRental3D
{

    public class YB_ViewFactory : IEnumerable<YB_ViewBasis>
    {
        public YB_LogicFactory              logicFactory;
        public YB_ViewItemFactory           viewItemFactory;
        Dictionary<int, YB_ViewBasis>       viewPool;
        YB_Repository                       repository;

        public YB_ViewFactory()
        {
            viewPool = new Dictionary<int, YB_ViewBasis>();
        }
        /// <summary>
        /// Constructor and initialization (read view YBML into repository).
        /// </summary>
        /// <param name="viewRepoLocalPath">The file path of View Repository (view definitions similar function to HTML).</param>
        public YB_ViewFactory(string viewRepoLocalPath, Action afterLoad) : this()
        {
            this.repository = new YB_Repository(viewRepoLocalPath);
        }        
        public YB_ViewFactory(Uri viewRepoUrl, Action afterLoad) : this()
        {
            this.repository = new YB_Repository(viewRepoUrl, afterLoad);
        }

        public YB_ViewFactory   ConfigLogicFactory(YB_LogicFactory factory)
        {
            this.logicFactory = factory;
            return this;
        }        
        public YB_ViewFactory   ConfigViewitemFactory(YB_ViewItemFactory factory)
        {
            this.viewItemFactory = factory;
            return this;
        }

        public YB_ViewFactory   LoadAllViews()
        {
            //It is not allowed to access repository before ReadAllLines().
            if (!this.repository.isReady)
                throw new YB_RepositoryError();

            foreach (var pairValue in this.repository.allRecordLines)
            {
                YB_ViewBasis viewPtr = this.CreateProduct(pairValue.Value);
                Debug.Log($"[Initializing View] : {viewPtr.Title}");
                try
                {
                    CreateSubViewitems(ref viewPtr, pairValue.Value);
                    viewPool.Add(pairValue.Key, viewPtr);
                }
                catch (Exception e)
                {
                    throw new YB_FactoryError(e.Message);
                }
            }
            return this;
        }
        public YB_ViewBasis     GetView(int viewId)
        {
            YB_ViewBasis yBView = null;
            viewPool.TryGetValue(viewId, out yBView);
            return yBView;
        }
        public YB_ViewBasis     GetView(string viewTitle)
        {
            var result = viewPool.Where((a) => a.Value.Title == viewTitle).FirstOrDefault();
            return result.Value;

        }

        private YB_ViewBasis    CreateProduct(string serializeString)
        {
            int startIndex = serializeString.IndexOf("ViewType:") + "ViewType:".Length;
            int endIndex = serializeString.IndexOf(";", startIndex);
            var typeStr = serializeString.Substring(startIndex, endIndex - startIndex);  

            if (typeStr == "DetailsView")    return new YB_DetailsView(serializeString);
            if (typeStr == "DialogView")     return new YB_DialogView(serializeString);
            if (typeStr == "InputView")      return new YB_InputView(serializeString);
            if (typeStr == "ListView")       return new YB_ListView(serializeString);
            if (typeStr == "MenuView")       return new YB_MenuView(serializeString);
            if (typeStr == "WelcomeView")    return new YB_WelcomeView(serializeString);

            return new YB_ViewBasis(serializeString);
        }
        private void            CreateSubViewitems(ref YB_ViewBasis view, string viewString)
        {
            //YB_ViewItemBasis prompt = this.viewItemFactory.CreateViewItem(YBGlobal.PROMPT_MESSAGEBOX_VIEW_ITEM);
            //prompt.parent = view;
            //view.subItemsList.Add(prompt);

            var itemsDef = view.GetItemDefStrings();

            if (itemsDef != null && itemsDef.Count > 0)
            {
                foreach (var itemIdPiar in itemsDef)
                {
                    string itemSubString = itemIdPiar.Value;
                    YB_ViewItemBasis child = this.viewItemFactory.CreateViewItem(itemSubString);
                    Debug.Log($"[...generating viewItem] : {child.Id}");
                    child.parentDef = view;
                    view.subItemsList.Add(child);
                }
            }
        }



        public IEnumerator<YB_ViewBasis> GetEnumerator()
        {
            return viewPool.Select(v=>v.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
