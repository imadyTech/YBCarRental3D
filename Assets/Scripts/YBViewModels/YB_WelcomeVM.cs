using UnityEngine;

namespace YBCarRental3D
{

    //106 AdminMenu
    public class YB_WelcomeVM : YB_ViewModelBasis<YB_User>
    {
        public YB_WelcomeVM() : base()
        {
        }

        private void Update()
        {
            if( Input.anyKeyDown )
            {
                YB_Window.Instance.Goto(this.viewDef.GotoView);
            }
        }
    };
}

