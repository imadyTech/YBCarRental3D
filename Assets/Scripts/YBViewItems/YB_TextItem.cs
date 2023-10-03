using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_TextItem : YB_ViewItemBasis
    {
        public YB_TextItem() { }
        public YB_TextItem(string def) : this()
        {
            base.Deserialize(def);
        }
        public override void OnBind(string contents)
        {
            throw new NotImplementedException();
        }
    }
}
