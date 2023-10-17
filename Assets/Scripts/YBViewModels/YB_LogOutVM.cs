

using imady.NebuUI;
using System.Collections.Generic;
using UnityEngine;

namespace YBCarRental3D
{

    //103 - RegisterView
    public class YB_LogOutVM : YB_ViewModelBasis<YB_User>
    {
        public YB_LogOutVM() : base() { }

        #region ===== view properties =====
        [NbuViewProperty] public string SeeYou => "See ya, " + YB_ManagerFactory.UserMgr.CurrentUser.FirstName;
        #endregion ===== view properties =====


        public override void onSubmit()
        {
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                YB_Window.Instance.Goto(this.viewDef.GotoView);
            }
        }

        public async override void onViewForwarded(YB_ViewBasis fromView)
        {
            base.onViewForwarded(fromView);
            base.RenderView();
            await YB_ManagerFactory.UserMgr.UserLogout();
        }
    };
}

