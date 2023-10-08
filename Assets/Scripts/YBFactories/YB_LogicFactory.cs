using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Properties;
using UnityEngine;

namespace YBCarRental3D
{
    public class YB_LogicFactory: MonoBehaviour
    {
        private Dictionary<string, Type>       serviceInstanceMap;

        public YB_LogicFactory() 
        {
        }
        private void Awake()
        {
            serviceInstanceMap = new Dictionary<string, Type>();

            //This not work, get a null type
            //this.RegisterDataSource("YB_WelcomeVM",         Type.GetType("YB_WelcomeVM"));

            this.RegisterDataSource("YB_WelcomeVM",         typeof(YB_WelcomeVM));
            this.RegisterDataSource("YB_MainMenuViewVM",    typeof(YB_MainMenuViewVM));
            this.RegisterDataSource("YB_UserLoginVM",       typeof(YB_UserLoginVM));
            this.RegisterDataSource("YB_UserRegisterVM",    typeof(YB_UserRegisterVM));
            this.RegisterDataSource("YB_UserMenuVM",        typeof(YB_UserMenuVM));
            this.RegisterDataSource("YB_AdminMenuVM",       typeof(YB_AdminMenuVM));
            this.RegisterDataSource("YB_CarSelectionVM",    typeof(YB_CarSelectionVM));
            this.RegisterDataSource("YB_CarRentVM",         typeof(YB_CarRentVM));
            this.RegisterDataSource("YB_MyOrdersVM",        typeof(YB_MyOrdersVM));
            this.RegisterDataSource("YB_OrderDetailsVM",    typeof(YB_OrderDetailsVM));
            this.RegisterDataSource("YB_MyProfileVM",       typeof(YB_MyProfileVM));
            this.RegisterDataSource("YB_CarAddVM",          typeof(YB_CarAddVM));//admin
            this.RegisterDataSource("YB_CarListVM",         typeof(YB_CarListVM));//admin listing
            this.RegisterDataSource("YB_CarManageVM",       typeof(YB_CarManageVM));//admin
            this.RegisterDataSource("YB_CarDeleteListVM",   typeof(YB_CarDeleteListVM));//admin
            this.RegisterDataSource("YB_CarDeleteVm",       typeof(YB_CarDeleteVm));//admin
            this.RegisterDataSource("YB_OrderManageVM",     typeof(YB_OrderManageVM));//admin - list all orders
            this.RegisterDataSource("YB_OrderAdminVM",      typeof(YB_OrderAdminVM));//admin - approve or reject
            this.RegisterDataSource("YB_UserAdminListVM",   typeof(YB_UserAdminListVM));//admin
            this.RegisterDataSource("YB_UserAdminVM",       typeof(YB_UserAdminVM));//admin
            this.RegisterDataSource("YB_LogOutVM",          typeof(YB_LogOutVM));//
            this.RegisterDataSource("YB_HelpViewVM",        typeof(YB_HelpViewVM));//         
            this.RegisterDataSource("YB_CopyrightViewVM",   typeof(YB_CopyrightViewVM));//
            this.RegisterDataSource("YB_ErrorViewVM",       typeof(YB_ErrorViewVM));//
            this.RegisterDataSource("YB_ByeByeViewVM",      typeof(YB_ByeByeViewVM));//
        }
        public bool RegisterDataSource(string sourceName, Type service) {
            try
            {
                this.serviceInstanceMap.Add(sourceName, service);
                return true;
            }
            catch { 
                return false; 
            }
        }
        public Type FindDataSource(string sourceName) { 
            if(string.IsNullOrEmpty(sourceName)) return null;

            if(this.serviceInstanceMap.ContainsKey(sourceName))
                return this.serviceInstanceMap[sourceName];
            else 
                return null;
        }		
    }
}
