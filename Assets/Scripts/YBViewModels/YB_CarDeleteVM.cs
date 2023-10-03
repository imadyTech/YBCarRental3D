using System.Collections.Generic;

namespace YBCarRental3D
{
    //118 YB_CarDeleteVm- ListView
    public class YB_CarDeleteVm : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarDeleteVm() : base()
        {
        }

        public override void onViewForwarded(YB_DataBasis fromData)
        {
            this.principalObject = (YB_Car)(fromData);
        }

        public override void onSubmit(Dictionary<string, string> valuesMapPtr)
        {
            bool deleteResult = YB_ManagerFactory.CarMgr.DeleteCar(this.principalObject);

            if (deleteResult)
                Window.Goto(YBGlobal.USER_MAIN_VIEW);
            else if (deleteResult)
                Window.Goto(YBGlobal.ADMIN_MAIN_VIEW);
        }

    };

}

