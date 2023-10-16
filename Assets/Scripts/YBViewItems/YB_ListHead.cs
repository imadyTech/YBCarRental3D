using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_ListHead : YB_ViewItemBasis
    {
        /// <summary>
        /// the list contains col:Name and col:Length
        /// </summary>
        public Dictionary<string, int> listHeadColumnDefMap = new Dictionary<string, int>();
        public YB_ListHead() { }
        public YB_ListHead(string def) : this()
        {
            base.Deserialize(def);
            //cacheing all def of col
            foreach (var iterator in this.stringPairsMap)
            {
                //Bypass items not contain '<col>'
                if (iterator.Key.Contains(YBGlobal.CONST_LIST_HEAD_STARTER))
                    listHeadColumnDefMap.Add(
                        iterator.Key.Remove(iterator.Key.IndexOf(YBGlobal.CONST_LIST_HEAD_STARTER), YBGlobal.CONST_LIST_HEAD_STARTER.Length), 
                        int.Parse(iterator.Value));
            }
        }
        public override YB_ViewItemBasis BindContent()
        {
            return this;
        }
        public override YB_ViewItemBasis BindAction()
        {
            return this;
        }
    }
}
