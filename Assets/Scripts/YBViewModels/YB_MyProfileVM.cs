

namespace YBCarRental3D {
    //113 order details - DetailsView
    public class YB_MyProfileVM : YB_ViewModelBasis<YB_User>
	{
		public YB_MyProfileVM(): base(){}

		YB_UserManager userManagerPtr = YB_ManagerFactory.UserMgr;


		public override void					onViewForwarded(YB_DataBasis fromData)		
		{
			this.principalObject = (YB_User)fromData;
		}
		public override string Get_PropertyValue(string bindName) 
		{
			//show password in MyProfile is forbidden
			if (bindName == "Password")	return string.Empty;

			return base.Get_PropertyValue(bindName);
		}
	};

}