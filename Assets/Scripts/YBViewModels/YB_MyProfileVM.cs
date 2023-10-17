

namespace YBCarRental3D
{
    //113 order details - DetailsView
    public class YB_MyProfileVM : YB_ViewModelBasis<YB_User>
    {
        public YB_MyProfileVM() : base() { }

        YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;


        public string Id => userManagerPtr.CurrentUser.Id.ToString();
        public string UserName => userManagerPtr.CurrentUser.UserName;
        public string Password => "******";//forbidden
        public string FirstName => userManagerPtr.CurrentUser.FirstName;
        public string FamilyName => userManagerPtr.CurrentUser.FamilyName;
        public string UserRoles => userManagerPtr.CurrentUser.UserRoles;
        public string Balance=> userManagerPtr.CurrentUser.Balance.ToString("c2");

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
            this.principalObject = fromView.viewModel.PrincipalData as YB_User;
            base.onViewForwarded(fromView);
            base.RenderView();
        }

    };

}