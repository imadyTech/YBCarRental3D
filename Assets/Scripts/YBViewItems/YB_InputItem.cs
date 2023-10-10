using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_InputItem : YB_ViewItemBasis
    {
        public YB_InputItem() { }
        public YB_InputItem(string def) : this()
        {
            base.Deserialize(def);
        }

        public override YB_ViewItemBasis BindContent()
        {
            return base.BindContent();
        }
        public override YB_ViewItemBasis BindAction()
        {
            //this.itemObject.GetComponent<TMP_Dropdown>().onValueChanged.AddListener((input) => { });
            return base.BindAction();
        }
        /// <summary>
        /// For value input type items, will provide a func returns the value of input gameobject
        /// </summary>
        /// <param name="valuesMapPtr"></param>
        /// <returns></returns>
        public override YB_ViewItemBasis ReverseBind(Dictionary<string, Func<string>> valuesMapPtr)
        {
            try
            {
                valuesMapPtr.Add(this.Bind, () => { 
                    return this.itemObject.GetComponent<TMP_InputField>().text; 
                });
            }
            catch (Exception e) { 
            }
            return this;
        }
    }
}
