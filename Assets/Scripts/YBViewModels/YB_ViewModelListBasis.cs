using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace YBCarRental3D
{
    /// <summary>
    /// This is the 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class YB_ViewModelListBasis<TData> : YB_ViewModelBasis<TData> where TData : YB_DataBasis
    {
        private YB_ListHead listHead;

        protected void CreateListHead()
        {
            try
            {
                YB_ListHead head = base.viewDef.subItemsList.Find(i => i.ItemType == "ListHead") as YB_ListHead;
                this.listHead = head;

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

            int numOfRows = int.Parse(this.viewDef.FindValue("ListRowCount"));
            int yPos = 2;
            foreach (TData yb_data in dataList)
            {
                var listItemDef = new YB_ListItem(this.viewDef.FindValue("ListRowTemplate"));
                listItemDef.y = listHead.y + yPos;
                CreateListItem(listItemDef, yb_data);
                yPos++;
                //resultViewList.Add(viewItem);
            }
            //return resultViewList;
        }
        protected void CreateListItem(YB_ListItem listItemDef, TData data)
        {
            listItemDef.itemGameObject = base.ybWindow.GenerateViewItemTemplate(listItemDef, this.gameObject.transform);

            try
            {
                GenerateRow(base.ybWindow.listItemColTemplate, listHead.listHeadColumnDefMap, listItemDef.itemGameObject, data, listHead.w);
                listItemDef.carriedData = data;
                listItemDef.parentDef = this.viewDef;
                ConfigViewItemObj(listItemDef, ref listItemDef.itemGameObject);
            }
            catch
            {
                Debug.LogWarning($"[ListItem generation error] in the view: {base.viewDef.Title}");
            }
        }




        #region === Utility methods ===
        private void GenerateRow(GameObject unitTemplate, Dictionary<string, int> rowFormatDef, GameObject rowObject, TData data, int rowDefWidth)
        {
            float rowScreenWidth = rowObject.GetComponent<RectTransform>().rect.width;
            int startPos = 3;
            foreach (var iterator in rowFormatDef)
            {
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
