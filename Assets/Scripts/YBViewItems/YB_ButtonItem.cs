﻿using System;
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
                TMP_Text tmpText = itemObject.GetComponentInChildren<TMP_Text>();//differ to base method
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
                        btn.onClick.AddListener(() => this.parent.viewModel.OnButtonClicked(this));
            }
            catch
            {
            }
            return this;
        }
        public override YB_ViewItemBasis ReverseBind(Dictionary<string, Func<string>> valuesMapPtr)
        {
            try
            {
                valuesMapPtr.Add(this.Bind, () => { return this.itemObject.GetComponentInChildren<TMP_Text>().text; });
            }
            catch
            {
            }
            return this;
        }
    }
}
