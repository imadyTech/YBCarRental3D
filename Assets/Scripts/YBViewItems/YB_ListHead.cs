using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_ListHead : YB_ViewItemBasis
    {
        public YB_ListHead() { }
        public YB_ListHead(string def) : this()
        {
            base.Deserialize(def);
        }
        public override YB_ViewItemBasis BindContent()
        {
            return base.BindContent();
        }
        public override YB_ViewItemBasis BindAction()
        {
            return base.BindAction();
        }
    }
}
