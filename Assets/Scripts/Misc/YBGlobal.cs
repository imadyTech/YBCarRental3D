using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    using LIST_HEAD_TEMPLATE            = List<KeyValuePair<string, int>>;
    using LIST_ITEM_VALUE               = KeyValuePair<string, string>;
    using LIST_ITEM_VALUES              = List<KeyValuePair<string, string>>;
    using FORMATED_LIST_ITEM_VALUE      = Tuple<string, string, int>;
    using FORMATED_LIST_VIEW_VALUES     = List<Tuple<string, string, int>>;

    public class YBGlobal
    {
        public static string CONST_LIST_HEAD_STARTER = "<col>";
        public static string CONST_VIEW_ITEM_STARTER = "<item>";

        public const string msgDef_ListItem = "ListItem";
        public const string msgDef_Menu = "MENU";

        public const string VIEWITEM_TYPE_BUTTON = "ButtonItem";
        public const string VIEWITEM_TYPE_TEXT = "TextItem";
        public const string VIEWITEM_TYPE_INPUT = "InputItem";
        public const string VIEWITEM_TYPE_HEAD = "ListHead";
        public const string VIEWITEM_TYPE_LISTITEM = "ListItem";
        public const string VIEWITEM_TYPE_MENUITEM = "MenuItem";
        public const string Button_Type_Submit = "Submit";
        public const string Button_Type_Delete = "BtnDelete";
        public const string Button_Type_Yes = "BtnYes";
        public const string Button_Type_No = "BtnNo";
        public const string Button_Type_Ok = "BtnOk";
        
        //when submit, the binded item's content will be passed to the datasource VM
        //EXAMPLE: a button with 'Bind:ButtonContent!' the the button's content will be passed.
        //if you need retrieve which button is clicked, just use "(*valuesMapPtr)["ButtonContent"]"
        public const string SUBMIT_BINDKEY_BUTTONCONTENT = "ButtonContent";
        //public const string INIT_VIEW = "CarSelectionView";
        public const string INIT_VIEW = "WelcomeView";
        public const string EXIT_VIEW = "ByeByeView";
        public const string MAIN_VIEW = "MainMenuView";
        public const string USER_MAIN_VIEW = "UserMenu";
        public const string ADMIN_MAIN_VIEW = "AdminMenu";
        public const string ERROR_VIEW = "ErrorView";
        public const string PROMPT_MESSAGEBOX_VIEW_ITEM = "Id:99990!x:2!y:01!w:116!h:27!ItemType:ButtonItem!Content:!ButtonType:BtnOk!Background:.!isCentral:1!isFocused:1!isSelected:0!isHidden:1!";
    }
}
