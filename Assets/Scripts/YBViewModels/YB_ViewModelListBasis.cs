using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace YBCarRental3D
{
    /// <summary>
    /// This is the 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class YB_ViewModelListBasis<TData> : YB_ViewModelBasis<TData> where TData : YB_DataBasis
    {
        private YB_ListHead             _listHead;
        private List<GameObject>        _listRowObjects;
        private int                     _currentPage = 0;
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }

        protected void CreateListHead()
        {
            try
            {
                YB_ListHead head = base.viewDef.subItemsList.Find(i => i.ItemType == "ListHead") as YB_ListHead;
                this._listHead = head;

                GenerateRow(base.ybWindow.listHeadColTemplate, head.listHeadColumnDefMap, head.itemGameObject, null, head.w);
            }
            catch
            {
                Debug.LogWarning($"[No ListHead definined] in the view: {base.viewDef.Title}");
            }
        }
        protected virtual void RenderListview(IEnumerable<TData> dataList)
        {
            //ClearLayoutContainer(container);
            //var resultViewList = new List<INebuUIView>();
#if DEVELOPMENT
            Debug.Log($"[RenderListview] {this.viewDef.Title}");
#endif

            int numOfRows = int.Parse(this.viewDef.FindValue("ListRowCount"));
            int yPos = 2;
            foreach (TData yb_data in dataList)
            {
                var listItemDef = new YB_ListItem(this.viewDef.FindValue("ListRowTemplate"));
                listItemDef.y = _listHead.y + yPos;
                CreateListItem(listItemDef, yb_data);
                yPos++;
                //resultViewList.Add(viewItem);
            }
            //return resultViewList;
        }
        protected void CreateListItem(YB_ListItem listItemDef, TData data)
        {
#if DEVELOPMENT
            Debug.Log($"[CreateListItem] {data.GetType().Name} {data.Id}");
#endif
            //generate empty row object
            listItemDef.itemGameObject = base.ybWindow.GenerateViewItemTemplate(listItemDef, this.gameObject.transform);
            _listRowObjects.Add(listItemDef.itemGameObject);

            try
            {
                //generate row columns
                GenerateRow(base.ybWindow.listItemColTemplate, _listHead.listHeadColumnDefMap, listItemDef.itemGameObject, data, _listHead.w);
                listItemDef.linkedTData = data;
                listItemDef.parentDef = this.viewDef;
                ConfigViewItemObj(listItemDef, ref listItemDef.itemGameObject);
            }
            catch
            {
                Debug.LogWarning($"[ListItem generation error] in the view: {base.viewDef.Title}");
            }
        }

        public override void onInit(YB_Window window)
        {
            base.onInit(window);

            _listRowObjects = new List<GameObject>();
            this.CreateListHead();
        }

        public override void onExit()
        {
            foreach(var item in _listRowObjects)
            {
                Destroy(item);
            }
            _listRowObjects.Clear();
            base.onExit();
        }

        #region === Private Utility methods ===
        private void GenerateRow(GameObject unitTemplate, Dictionary<string, int> rowFormatDef, GameObject rowObject, TData data, int rowDefWidth)
        {

            float rowScreenWidth = rowObject.GetComponent<RectTransform>().rect.width;
            int startPos = 3;
            foreach (var iterator in rowFormatDef)
            {
#if DEVELOPMENT
            Debug.Log($"[Generate Row Col] {iterator.Key}");
#endif
                var colObjTemplate = Instantiate(unitTemplate, rowObject.transform);
                var headColText = colObjTemplate.GetComponent<TMP_Text>();

                if (data != null)
                {
                    headColText.text = data.FindFieldValue(iterator.Key);
                }
                else
                    headColText.text = iterator.Key;

                colObjTemplate.GetComponent<RectTransform>().anchoredPosition = new Vector2(calculatePos(startPos, rowDefWidth, rowScreenWidth), 0);
                colObjTemplate.name = iterator.Key;

                startPos += iterator.Value;
            }
            float calculatePos(int ybPos, int ybWidth, float listHeadWidth)
            {
                return (float)ybPos / ybWidth * listHeadWidth - listHeadWidth / 2;
            }
        }
        #endregion
    }
}
