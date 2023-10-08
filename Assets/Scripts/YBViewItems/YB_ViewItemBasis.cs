using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace YBCarRental3D
{
    public class YB_ViewItemBasis : YB_DataBasis
    {
        public int x = 0, y = 0;                           //relative coordinate inside the view
        public int w = 100;                                //Width of the viewItem.
        public int h = 1;                              //Height of the viewItem.
        public string ItemType;
        public string Background = "";
        public string Bind = "";
        public string Link = "";
        public bool isCentral = true;
        public bool isFocused = false;
        public bool isSelected = false;
        public bool isHidden = false;                  //if an item is hidden, then the View will ignore it during rendering.
        public string Content;

        public YB_ViewBasis parent;
        public GameObject itemObject;


        public YB_ViewItemBasis()
        {
            this.persistentSeparator = '!';
        }
        public YB_ViewItemBasis(string definition) : this()
        {
            base.serializedString = definition;
            this.Deserialize(definition);
        }

        public void Deserialize(string line)
        {
            base.SplitLine(line);

            if (base.HasValue("Id")) base.Id = int.Parse(FindValue("Id"));
            if (base.HasValue("x")) x = int.Parse(FindValue("x"));
            if (base.HasValue("y")) y = int.Parse(FindValue("y"));
            if (base.HasValue("w")) w = int.Parse(FindValue("w"));
            if (base.HasValue("h")) h = int.Parse(FindValue("h"));
            if (base.HasValue("Bind")) Bind = FindValue("Bind");
            if (base.HasValue("Link")) Link = FindValue("Link");
            if (base.HasValue("ItemType")) ItemType = FindValue("ItemType");
            if (base.HasValue("Content")) Content = FindValue("Content");
            if (base.HasValue("Background")) Background = FindValue("Background");
            if (base.HasValue("isCentral")) isCentral = FindValue("isCentral") == "1";
            if (base.HasValue("isFocused")) isFocused = FindValue("isFocused") == "1";
            if (base.HasValue("isSelected")) isSelected = FindValue("isSelected") == "1";
            if (base.HasValue("isHidden")) isHidden = FindValue("isHidden") == "1";
        }

        public virtual YB_ViewItemBasis BindContent()
        {
            try
            {
                TMP_Text tmpText = itemObject.GetComponent<TMP_Text>();
                tmpText.enableWordWrapping = false;
                tmpText.text = this.Content;
                if (this.isCentral)
                    tmpText.alignment = TextAlignmentOptions.Center;
            }
            catch { }
            return this;
        }

        public virtual YB_ViewItemBasis BindAction()
        {
            return this;
        }
    }
}
