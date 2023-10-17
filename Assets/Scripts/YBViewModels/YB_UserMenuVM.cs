using imady.NebuUI;
using UnityEngine;


namespace YBCarRental3D {
    //105 UserMenu
    public class YB_UserMenuVM : YB_ViewModelBasis<YB_User>
	{
        public YB_UserMenuVM():base() {
		}

        #region ===== view properties =====
        [NbuViewProperty]
        public string UserName => YB_ManagerFactory.UserMgr.CurrentUser.UserName;
        [NbuViewProperty]
        public string UserRoles => YB_ManagerFactory.UserMgr.CurrentUser.UserRoles;
        #endregion ===== view properties =====


        public override void onInit(YB_Window window)
        {
#if DEVELOPMENT
            Debug.Log($"[View onInit] : {base.viewDef.Title}");
#endif
            base.onInit(window);
        }

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
#if DEVELOPMENT
            Debug.Log($"[View onViewForwarded] : {base.viewDef.Title}");
#endif
            base.onViewForwarded(fromView);
            base.RenderView();
        }

    }
}

