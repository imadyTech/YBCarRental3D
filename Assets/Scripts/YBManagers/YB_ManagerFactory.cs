﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    /// <summary>
    /// YB_ManagerFactory is somehow act as global DI (dependency injection) framework
    /// you may use YBCarRental::YB_ManagerFactory::userMgr to access the logic maanager
    /// </summary>
    public class YB_ManagerFactory
    {
        public static YB_UserManager       UserMgr;
        public static YB_CarManager        CarMgr;
        public static YB_RentManager       RentMgr;

        public YB_ManagerFactory()
        {
#if DEVELOPMENT
            UserMgr =   new YB_UserManager("https://localhost:7024/api");
#elif PRODUCTION
            UserMgr =   new YB_UserManager("https://angoomathapi.azurewebsites.net/api");
#endif
            CarMgr =    new YB_CarManager("\\CarRepo.txt");
            RentMgr =   new YB_RentManager("\\RentRepo.txt");
        }
    };
}
