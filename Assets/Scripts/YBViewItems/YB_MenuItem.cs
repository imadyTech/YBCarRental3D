using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace YBCarRental3D
{
    public class YB_MenuItem : YB_ViewItemBasis
    {
        public YB_MenuItem() { }
        public YB_MenuItem(string def) : this()
        {
            base.Deserialize(def);
        }
        public override void OnBind(string contents)
        {
            try
            {
                TMP_Text tmpText = itemObject.GetComponentInChildren<TMP_Text>();
                tmpText.enableWordWrapping = false;
                tmpText.text = contents;
                if (this.isCentral)
                    tmpText.alignment = TextAlignmentOptions.Center;
            }
            catch { }
        }
    }
}
