using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

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
            Debug.Log($"[YB_InputItem BindContent] : {this.Bind}");
            return this;
        }
        public override YB_ViewItemBasis BindAction()
        {
            Debug.Log($"[YB_InputItem BindAction] : {this.Bind}");
            //this.itemObject.GetComponent<TMP_Dropdown>().onValueChanged.AddListener((input) => { });
            return base.BindAction();
        }
        /// <summary>
        /// Value -> View
        /// </summary>
        /// <param name="valuesMapPtr"></param>
        /// <returns></returns>
        public override YB_ViewItemBasis ForwardBind(Dictionary<string, Action<string>> valuesMapPtr)
        {
            Debug.Log($"[YB_InputItem ForwardBinding] : {this.Bind}");
            try
            {
                valuesMapPtr.Add(
                    this.Bind, 
                    a => { 
                        this.itemGameObject.GetComponent<TMP_InputField>().text = a; });
            }
            catch (Exception e) { 
                Debug.Log(e);
            }
            return this;
        }
        /// <summary>
        /// View -> Value
        /// </summary>
        /// <param name="valuesMapPtr"></param>
        /// <returns></returns>
        public override YB_ViewItemBasis ReverseBind(Dictionary<string, Func<string>> valuesMapPtr)
        {
            Debug.Log($"[YB_InputItem ReverseBinding] : {this.Bind}");
            try
            {
                valuesMapPtr.Add(
                    this.Bind, 
                    () => { return this.itemGameObject.GetComponent<TMP_InputField>().text; });
            }
            catch (Exception e) { 
                Debug.Log(e);
            }
            return this;
        }
    }
}
