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
        static string                       CONST_LIST_HEAD_STARTER = "<col>";
        static string                       CONST_VIEW_ITEM_STARTER = "<item>";

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
        /// <param name="viewRepoUrl">The file path of View Repository (view definitions similar function to HTML).</param>
        public YB_ViewFactory(string viewRepoUrl) : this()
        {
            this.repository = new YB_Repository(viewRepoUrl);
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
            YB_ViewBasis newView = new YB_ViewBasis(serializeString);

            return newView;

        }
        private void            CreateSubViewitems(ref YB_ViewBasis view, string viewString)
        {
            //YB_ViewItemBasis prompt = this.viewItemFactory.CreateViewItem(YBGlobal.PROMPT_MESSAGEBOX_VIEW_ITEM);
            //prompt.parent = view;
            //view.subItemsList.Add(prompt);

            var itemsDef = view.FindValues(CONST_VIEW_ITEM_STARTER);
            if (itemsDef != null && itemsDef.Count > 0)
            {
                foreach (var itemIdPiar in itemsDef)
                {
                    string itemSubString = itemIdPiar.Value;
                    YB_ViewItemBasis child = this.viewItemFactory.CreateViewItem(itemSubString);
                    child.parent = view;
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
