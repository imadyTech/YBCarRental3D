using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ListItem : YB_ViewItemBasis
    {
        public YB_DataBasis linkedTData;

        public YB_ListItem() { }
        public YB_ListItem(string def) : this()
        {
            base.Deserialize(def);
        }

        public override YB_ViewItemBasis BindAction()
        {
            try
            {
#if DEVELOPMENT
                Debug.Log($"[Item BindContent] {this.ItemType} {this.Id}");
#endif
                Button btn;
                itemGameObject.TryGetComponent<Button>(out btn);
                if (btn != null)
                    btn.onClick.AddListener(() => {
                        this.parentDef.viewModel.PrincipalData = linkedTData;
                        YB_Window.Instance.Goto(this.Link); 
                    });
            }
            catch { }
            return this;
        }
    }
}
