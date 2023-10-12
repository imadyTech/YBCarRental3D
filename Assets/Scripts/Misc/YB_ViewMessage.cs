using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_ViewMessageBasis
    {
        public string Content;
        public YB_ViewMessageBasis()
        {
            Content = string.Empty;
        }
        public YB_ViewMessageBasis(string message) : this()
        {
            Content = message;
        }
    }

    public class YB_ButtonSubmitMessage : YB_ViewMessageBasis
    {

        public YB_ButtonSubmitMessage() : base(YBGlobal.Button_Type_Submit) { }

    }

    public class YB_ButtonYesMessage : YB_ViewMessageBasis
    {

        public YB_ButtonYesMessage() : base(YBGlobal.Button_Type_Yes) { }

    }

    public class YB_ButtonNoMessage : YB_ViewMessageBasis
    {

        public YB_ButtonNoMessage() : base(YBGlobal.Button_Type_No) { }
    }

    public class YB_ButtonOkMessage : YB_ViewMessageBasis
    {

		public YB_ButtonOkMessage() : base(YBGlobal.Button_Type_Ok) { }
    }

    public class YB_MenuItemMessage : YB_ViewMessageBasis
    {

		public YB_MenuItemMessage() : base(YBGlobal.msgDef_Menu) { }
        public YB_MenuItemMessage(string link)	: base(link) { }
    }

    public class YB_ListItemMessage : YB_ViewMessageBasis
    {

        public YB_ListItemMessage() : base(YBGlobal.msgDef_ListItem) { }
        public YB_ListItemMessage(int id) : base(YBGlobal.msgDef_ListItem)
        {
            this.itemId = id;
        }
        int itemId;
    }

    public class YB_InputItemMessage : YB_ViewMessageBasis
    {
        public YB_InputItemMessage() : base(YBGlobal.VIEWITEM_TYPE_INPUT) { }
	}
}