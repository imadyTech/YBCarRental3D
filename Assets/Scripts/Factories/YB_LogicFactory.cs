using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_LogicFactory
    {
        private Dictionary<string, IYB_DataSource>       serviceInstanceMap;

        public YB_LogicFactory() 
        {
            serviceInstanceMap = new Dictionary<string, IYB_DataSource>();

            this.RegisterDataSource("YB_UserLoginVM",       new YB_UserLoginVM());
            this.RegisterDataSource("YB_UserRegisterVM",    new YB_UserRegisterVM());
            this.RegisterDataSource("YB_UserMenuVM",        new YB_UserMenuVM());
            this.RegisterDataSource("YB_AdminMenuVM",       new YB_AdminMenuVM());
            this.RegisterDataSource("YB_CarSelectionVM",    new YB_CarSelectionVM());
            this.RegisterDataSource("YB_CarRentVM",         new YB_CarRentVM());
            this.RegisterDataSource("YB_MyOrdersVM",        new YB_MyOrdersVM());
            this.RegisterDataSource("YB_OrderDetailsVM",    new YB_OrderDetailsVM());
            this.RegisterDataSource("YB_MyProfileVM",       new YB_MyProfileVM());
            this.RegisterDataSource("YB_CarAddVM",          new YB_CarAddVM());//admin
            this.RegisterDataSource("YB_CarListVM",         new YB_CarListVM());//admin listing
            this.RegisterDataSource("YB_CarManageVM",       new YB_CarManageVM());//admin
            this.RegisterDataSource("YB_CarDeleteListVM",   new YB_CarDeleteListVM());//admin
            this.RegisterDataSource("YB_CarDeleteVm",       new YB_CarDeleteVm());//admin
            this.RegisterDataSource("YB_OrderManageVM",     new YB_OrderManageVM());//admin - list all orders
            this.RegisterDataSource("YB_OrderAdminVM",      new YB_OrderAdminVM());//admin - approve or reject
            this.RegisterDataSource("YB_UserAdminListVM",   new YB_UserAdminListVM());//admin
            this.RegisterDataSource("YB_UserAdminVM",       new YB_UserAdminVM());//admin
            this.RegisterDataSource("YB_LogOutVM",          new YB_LogOutVM());//admin
        }

        public bool RegisterDataSource(string sourceName, IYB_DataSource service) {
            try
            {
                this.serviceInstanceMap.Add(sourceName, service);
                return true;
            }
            catch { return false; }
        }
        public IYB_DataSource FindDataSource(string sourceName) { 
            if(this.serviceInstanceMap.ContainsKey(sourceName))
                return this.serviceInstanceMap[sourceName];
            else 
                return null;
        }		
    }
}
