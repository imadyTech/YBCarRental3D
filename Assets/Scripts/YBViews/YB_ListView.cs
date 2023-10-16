using imady.NebuUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ListView : YB_ViewBasis
    {
        public string   ListHead;
        public int      ListRowCount;
        public string   ListRowTemplate;

        public YB_ListView(string serializeString) : base() {
            base.serializeString = serializeString;
            this.Deserialize(base.serializeString);

        }

        public override void Deserialize(string line)
        {
            base.Deserialize(line);

            if (base.HasValue("ListHead"))          ListHead = base.FindValue("ListHead");
            if (base.HasValue("ListRowCount"))      ListRowCount = int.Parse(base.FindValue("ListRowCount"));
            if (base.HasValue("ListRowTemplate"))   ListRowTemplate = base.FindValue("ListRowTemplate");
        }

        /// <summary>
        /// return a list of viewitem definitions
        /// e.g., start with '<item>', plus specific types of itemdef
        /// </summary>
        /// <returns></returns>
        public override List<KeyValuePair<string, string>> GetItemDefStrings()
        {
            var itemDefList =  this.FindValues(YBGlobal.CONST_VIEW_ITEM_STARTER);
            itemDefList.Add(new KeyValuePair<string, string>("ListHead", base.FindValue("ListHead")));

            return itemDefList;
    }

}
}
