using UnityEngine;

namespace YBCarRental3D
{

    //106 AdminMenu
    public class YB_WelcomeVM : YB_ViewModelBasis<YB_User>
    {
        public YB_WelcomeVM() : base()
        {
        }

        public override void onInit(YB_Window window)
        {
            var item = new YB_TextItem() {ItemType="TextItem", Id = 1009, x = 4, y = 27, w = 100, h = 1, Content = $"Version {Application.version}", Background = " ", isCentral = false, isFocused = false, isSelected = false, isHidden = false };
            base.viewDef.AddItem(item);
            base.onInit(window);
        }

        private void Update()
        {
            if( Input.anyKeyDown && !Input.GetKey(KeyCode.Escape))
            {
                YB_Window.Instance.Goto(this.viewDef.GotoView);
            }
        }
    }}

