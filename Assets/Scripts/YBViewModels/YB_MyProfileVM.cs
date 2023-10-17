

using imady.NebuUI;

namespace YBCarRental3D
{
    //113 order details - DetailsView
    public class YB_MyProfileVM : YB_ViewModelBasis<YB_User>
    {
        public YB_MyProfileVM() : base() { }

        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;

        #region ===== view properties =====
        [NbuViewProperty] public string Id => userManagerPtr.CurrentUser.Id.ToString();
        [NbuViewProperty] public string UserName => userManagerPtr.CurrentUser.UserName;
        [NbuViewProperty] public string Password => "******";//forbidden
        [NbuViewProperty] public string FirstName => userManagerPtr.CurrentUser.FirstName;
        [NbuViewProperty] public string FamilyName => userManagerPtr.CurrentUser.FamilyName;
        [NbuViewProperty] public string UserRoles => userManagerPtr.CurrentUser.UserRoles;
        [NbuViewProperty] public string Balance=> userManagerPtr.CurrentUser.Balance.ToString("c2");
        #endregion ===== view properties =====

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_User;
            base.onViewForwarded(fromView);
            base.RenderView();
        }

    };

}