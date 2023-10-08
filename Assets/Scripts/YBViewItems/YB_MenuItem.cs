using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_MenuItem : YB_ViewItemBasis
    {
        public YB_MenuItem() { }
        public YB_MenuItem(string def) : this()
        {
            base.Deserialize(def);
        }
        public override YB_ViewItemBasis BindContent()
        {
            try
            {
                TMP_Text tmpText = itemObject.GetComponentInChildren<TMP_Text>();
                tmpText.enableWordWrapping = false;
                tmpText.text = this.Content;
                if (this.isCentral)
                    tmpText.alignment = TextAlignmentOptions.Center;
            }
            catch { }
            return this;
        }
        public override YB_ViewItemBasis BindAction()
        {
            try
            {
                var btn = itemObject.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(() => { YB_Window.Instance.Goto(this.Link); });
            }
            catch { }
            return this;
        }
    }
}
