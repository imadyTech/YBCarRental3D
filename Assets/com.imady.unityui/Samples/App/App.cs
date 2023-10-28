#region Copyrights imady
/*
 *Copyright(C) 2020 by imady Technology (Suzhou); All rights reserved.
 *Author:       Frank Shen
 *Date:         2020-07-31
 *Description:   
 */
#endregion

using System;
using UnityEngine;
using imady.NebuEvent;
using YBCarRental3D;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UIElements;

namespace imady.NebuUI.Samples
{
    public class App : NebuSingleton<App>
    {
        #region GameObjects & Managers对象定义
        //Nebu辅助开发工具，用于监控运行时进程
        public GameObject           NebuEventMgrGO;
        public GameObject           NebuSceneMgrGO;
        public GameObject           NebuUiMgrGO;
        public GameObject           MainCanvasObject;
        public GameObject           YBLogicFactoryGO;


        public NbuUIManager         uiManager;
        public NbuTheatreManager    theatreManager;
        public NebuEventManager     eventManager;
        public NebulogManager       nebulogManager;
        public YBCar_MessageManager carMessageManager;


        public YBCameraObject       yb_Camera;
        public YB_CarObject         bmw;
        public YB_CarObject         tesla1;
        public YB_CarObject         benz;
        public YB_CarObject         tesla2;
        public YB_CarObject         vw;

        #endregion

        #region MonoBehaviour Methods
        protected override void Awake()
        {
            Application.targetFrameRate = 30;
            base.Awake();

            //========================================
            //App对象及App.cs脚本必须自始至终都在场景中存在。
            //DontDestroyOnLoad(this);
            //========================================


            InitializeAppConfiguration();
            InitializeManagers(null, null);
        }

        //void Update()
        //{
        //    // exit
        //    if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        Debug.Log("[NebulogAPP应用程序]: 应用程序已经退出。");
        //        Application.Quit();
        //    }

        //}

        void OnApplicationQuit()
        {
            //停止 TODO: remove all listeners registered
        }
        #endregion

        public YB_Window            window;
        public YB_ManagerFactory    managerFactory;
        public YB_APIContext        apiContext;
        public YB_LogicFactory      logicVMFactory;
        public YB_ViewFactory       viewFactory;
        public YB_ViewItemFactory   viewItemFactory;

        private void InitializeAppConfiguration()
        {
            //bmw.GetComponent<YB_CarObject>().RightLight.intensity = 1;

            //初始化所有Manager。。。
            //eventManager必须在最先加载
            eventManager = new NebuEventManager();

            ///// 剧场管理器，负责管理被操作对象（NebuInteractable）的生成、销毁、入场、离场
            //if (NebuSceneMgrGO != null) theatreManager = ((NbuTheatreManager)NebuSceneMgrGO
            //    .AddComponent<NbuTheatreManager>()
            //    .AddEventManager(this.eventManager))//这是NebuTheatreManager自己注册到eventsystem
            //    .AddPool(this.NebuSceneMgrGO.transform);
            //Debug.Log("[Nebu剧场对象管理器]：NebuTheatreManager 初始化完成。");


            //添加用户界面管理器
            if (NebuUiMgrGO != null) uiManager = ((NbuUIManager)NebuUiMgrGO
                .GetComponent<NbuUIManager>()
                .AddEventManager(this.eventManager))
                .AddPool(MainCanvasObject.transform)
                .Initialize(this.eventManager);
            //uiManager.mainView.AddSystemLog("UI启动完成", this.name);
            //Debug.Log("[Nebu用户界面管理器]：NebuUIMananger UI管理器初始化完成。");

            //重要：EventSystem进行匹配subscribe。
            //theatreManager.Initialize(this.eventManager);//这是为自己管理的对象注册到eventsystem
            //uiManager.mainView.AddSystemLog("对象加载完成！", this.name);



            ////nebulogManager = NebuEventMgrGO
            ////    .AddComponent<NebulogManager>()
            ////    .AddEventManager(this.eventManager) as NebulogManager;
            ////eventManager.MappingEventObjectsByInterfaces();
            ////Debug.Log("[Nebu消息系统]：iNebuEventManager初始化完成。");

            //carMessageManager = NebuEventMgrGO
            //    .AddComponent<YBCar_MessageManager>()
            //    .AddEventManager(this.eventManager) as YBCar_MessageManager;
            //Debug.Log("[Nebu消息系统]：YBCar_MessageManager初始化完成。");




            //Debug.Log("[Nebu消息系统]：iNebuEventManager初始化完成。");


            //丢个加载完成的消息出去
            Debug.Log($"[Nebu应用管理器]: {Application.version}应用程序加载完成！");
            //uiManager.mainView.AddSystemLog("应用程序启动完成。", this.name);

            //uiManager.ShowMessageBox("App Initiated", "App has been initiated successfully.");
        }


        /// <summary>
        /// Add the manager objects and components after the logger is ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitializeManagers(object sender, EventArgs e)
        {
            managerFactory = new YB_ManagerFactory();
            viewItemFactory = new YB_ViewItemFactory();

            this.logicVMFactory = YBLogicFactoryGO
            .AddComponent<YB_LogicFactory>();

#if DEVELOPMENT
            //var viewRepoUrl = Path.Combine(Application.streamingAssetsPath, "ViewRepo.txt");//local file
#endif
#if PRODUCTION
#endif
            var viewRepoUrl = new Uri(Path.Combine(Application.streamingAssetsPath, "ViewRepo.txt"));//deployed to azure static web
            viewFactory = new YB_ViewFactory(viewRepoUrl, this.AfterNebuManagersInitialized);

#if DEVELOPMENT
            //AfterNebuManagersInitialized();
#endif


        }


        /// <summary>
        /// 完成管理器加载后要处理的事务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterNebuManagersInitialized()
        {
            viewFactory.ConfigLogicFactory(this.logicVMFactory)
                .ConfigViewitemFactory(this.viewItemFactory)
                .LoadAllViews();

            window = (YB_Window)
                MainCanvasObject
                .GetComponent<YB_Window>()
                .ConfigViewFactory(viewFactory)
                .ConfigLogicFactory(logicVMFactory)
                .Init();

            foreach (var view in viewFactory)
            {
                if (view.viewModel != null)
                    view.viewModel.AddEventManager(this.eventManager);
            }

            this.bmw.AddEventManager(this.eventManager);
            this.tesla1.AddEventManager(this.eventManager);
            this.tesla2.AddEventManager(this.eventManager);
            this.benz.AddEventManager(this.eventManager);
            this.vw.AddEventManager(this.eventManager);
            this.yb_Camera.AddEventManager(eventManager);


            //this must be done after all observers/providers added eventmanager.
            eventManager.MappingEventObjectsByInterfaces();

            Debug.Log("[Initialized.]");

        }
    }
}