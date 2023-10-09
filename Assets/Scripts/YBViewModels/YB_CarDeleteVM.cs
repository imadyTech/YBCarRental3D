using System;
using System.Collections.Generic;

namespace YBCarRental3D
{
    //118 YB_CarDeleteVm- ListView
    public class YB_CarDeleteVm : YB_ViewModelBasis<YB_Car>
    {
        public YB_CarDeleteVm() : base()
        {
        }

        public override void onViewForwarded(YB_ViewBasis fromView)
        {
        }

        public override void onSubmit()
        {
            throw new NotImplementedException();

            bool deleteResult = YB_ManagerFactory.CarMgr.DeleteCar(this.principalObject);

            if (deleteResult)
                ybWindow.Goto(YBGlobal.USER_MAIN_VIEW);
            else if (deleteResult)
                ybWindow.Goto(YBGlobal.ADMIN_MAIN_VIEW);
        }

    };

}

