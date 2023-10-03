using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_ButtonItem : YB_ViewItemBasis
    {
        public YB_ButtonItem() { }
        public YB_ButtonItem(string def): this()
        {
            base.Deserialize(def);
        }


        public override void OnBind(string contents)
        {
            throw new NotImplementedException();
        }
    }
}
