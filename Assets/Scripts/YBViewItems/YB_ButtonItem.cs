using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ButtonItem : YB_ViewItemBasis
    {
        public string ButtonType;

        public YB_ButtonItem() { }
        public YB_ButtonItem(string def) : this()
        {
            base.Deserialize(def);
            if (base.HasValue("ButtonType")) this.ButtonType = base.FindValue("ButtonType");
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
                {
                    if (this.ButtonType == YBGlobal.Button_Type_Submit)
                    {
                        YB_ButtonSubmitMessage message = new YB_ButtonSubmitMessage();
                        btn.onClick.AddListener(() => this.parent.dataSource.OnChildReturn(message, this));
                    }
                    if (this.ButtonType == YBGlobal.Button_Type_Yes)
                    {
                        YB_ButtonYesMessage message = new YB_ButtonYesMessage();
                        btn.onClick.AddListener(() => this.parent.dataSource.OnChildReturn(message, this));
                    }
                    if (this.ButtonType == YBGlobal.Button_Type_No)
                    {
                        YB_ButtonNoMessage message = new YB_ButtonNoMessage();
                        btn.onClick.AddListener(() => this.parent.dataSource.OnChildReturn(message, this));
                    }
                    if (this.ButtonType == YBGlobal.Button_Type_Ok)
                    {
                        YB_ButtonOkMessage message = new YB_ButtonOkMessage();
                        btn.onClick.AddListener(() => this.parent.dataSource.OnChildReturn(message, this));
                    }
                }
            }
            catch
            {
            }
            return this;
        }
    }
}
