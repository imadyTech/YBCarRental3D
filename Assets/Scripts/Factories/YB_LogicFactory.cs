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
        private Dictionary<string, IYB_DataSource>       serviceInstanceMap;

        public YB_LogicFactory() 
        {
            serviceInstanceMap = new Dictionary<string, IYB_DataSource>();
        }
        public void Awake()
        {
            this.RegisterDataSource("YB_UserLoginVM",       this.gameObject.AddComponent<YB_UserLoginVM>());
            this.RegisterDataSource("YB_UserRegisterVM",    this.gameObject.AddComponent<YB_UserRegisterVM>());
            this.RegisterDataSource("YB_UserMenuVM",        this.gameObject.AddComponent<YB_UserMenuVM>());
            this.RegisterDataSource("YB_AdminMenuVM",       this.gameObject.AddComponent<YB_AdminMenuVM>());
            this.RegisterDataSource("YB_CarSelectionVM",    this.gameObject.AddComponent<YB_CarSelectionVM>());
            this.RegisterDataSource("YB_CarRentVM",         this.gameObject.AddComponent<YB_CarRentVM>());
            this.RegisterDataSource("YB_MyOrdersVM",        this.gameObject.AddComponent<YB_MyOrdersVM>());
            this.RegisterDataSource("YB_OrderDetailsVM",    this.gameObject.AddComponent<YB_OrderDetailsVM>());
            this.RegisterDataSource("YB_MyProfileVM",       this.gameObject.AddComponent<YB_MyProfileVM>());
            this.RegisterDataSource("YB_CarAddVM",          this.gameObject.AddComponent<YB_CarAddVM>());//admin
            this.RegisterDataSource("YB_CarListVM",         this.gameObject.AddComponent<YB_CarListVM>());//admin listing
            this.RegisterDataSource("YB_CarManageVM",       this.gameObject.AddComponent<YB_CarManageVM>());//admin
            this.RegisterDataSource("YB_CarDeleteListVM",   this.gameObject.AddComponent<YB_CarDeleteListVM>());//admin
            this.RegisterDataSource("YB_CarDeleteVm",       this.gameObject.AddComponent<YB_CarDeleteVm>());//admin
            this.RegisterDataSource("YB_OrderManageVM",     this.gameObject.AddComponent<YB_OrderManageVM>());//admin - list all orders
            this.RegisterDataSource("YB_OrderAdminVM",      this.gameObject.AddComponent<YB_OrderAdminVM>());//admin - approve or reject
            this.RegisterDataSource("YB_UserAdminListVM",   this.gameObject.AddComponent<YB_UserAdminListVM>());//admin
            this.RegisterDataSource("YB_UserAdminVM",       this.gameObject.AddComponent<YB_UserAdminVM>());//admin
            this.RegisterDataSource("YB_LogOutVM",          this.gameObject.AddComponent <YB_LogOutVM>());//admin          
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
            if(string.IsNullOrEmpty(sourceName)) return null;

            if(this.serviceInstanceMap.ContainsKey(sourceName))
                return this.serviceInstanceMap[sourceName];
            else 
                return null;
        }		
    }
}
