using imady.NebuEvent;
using imady.NebuLog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace imady.NebuUI.Samples
{
    [NbuResourcePath("MainCanvasCombo/")]
    public class MainSystemLogPanel : NebuUIViewBase, INebuUIView,
        INebuObserver<NebuLogMsg>
    {
        public MainSystemLogViewItem itemTemplate; //µ¥Ò»¼ÇÂ¼µÄÊÓÍ¼Ä£°å£¬ÔÚunity editorÖÐÔ¤¸³Öµ
        public VerticalLayoutGroup systemLogContainer;//ÈÝÆ÷£¬ÔÚunity editorÖÐÔ¤¸³Öµ
        public int maxLogNumber = 100; //×î´ó±£ÁôµÄÈÕÖ¾ÌõÊý£¨³¬³öºóÒÆ³ý×îÔçÈëÁÐµÄ¼ÇÂ¼£©
        private int logCountIndex = 0;

        Queue<MainSystemLogViewModel> m_logVMs;//ÈÕÖ¾VM¼¯ºÏ
        Queue<INebuUIView> m_logView;//Ã¿¸öLogÐÅÏ¢µÄItemView¶ÔÏó¼¯ºÏ
        private Vector3 logContainerHeight => new Vector3(0, systemLogContainer.preferredHeight, 0f);
        private Vector3 logItemHeight = new Vector3(0, 50, 0f);//Ò»Ìõ¼ÇÂ¼viewItemµÄµ¥Î»ÐÐ¸ß¶È
        private Vector3 logViewCurrentPos;//ÓÃÓÚ¹ö¶¯ListView

        protected override void Awake()
        {
            base.Awake();
        }
        public void Init()
        {
            
            m_logVMs = new Queue<MainSystemLogViewModel>();
            m_logView = new Queue<INebuUIView>();
            logViewCurrentPos = systemLogContainer.GetComponent<RectTransform>().anchoredPosition3D;
        }

        /// <summary>
        /// ´«ÈëÊý¾Ý£¬VMÇý¶¯ÊÓÍ¼·½Ê½äÖÈ¾
        /// </summary>
        /// <param name="logVMs"></param>
        /// <returns></returns>
        public MainSystemLogPanel SetViewModel(Queue<MainSystemLogViewModel> logVMs)
        {
            m_logVMs = logVMs;//»º´æÒýÓÃ
            this.RenderListview<MainSystemLogViewModel>(m_logVMs, systemLogContainer);
            return this;
        }

        /// <summary>
        /// Õâ¸ö·½·¨Òþ²ØbaseµÄ·½·¨ÒÔÃâÒýÆð´íÎó
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public new MainSystemLogPanel SetViewModel(object source)
        {
            try
            {
                this.SetViewModel(source as Queue<MainSystemLogViewModel>);
            }
            catch (Exception e)
            {
                Debug.LogError($"{this.name} is not able to cast object to List<MainSystemLogViewModel>.\n{e}");
            }
            return this;
        }

        /// <summary>
        /// ÊµÀý»¯µ¥Ìõ¼ÇÂ¼µÄviewItem
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected virtual INebuUIView RenderItem(MainSystemLogViewModel viewmodel, LayoutGroup container)
        {
            var itemView = GameObject.Instantiate(itemTemplate, container.transform, false)
                    .GetComponent<INebuUIView>();
            itemView.SetViewModel(viewmodel);
            return itemView;
        }

        /// <summary>
        /// ÅúÁ¿Ìí¼Ó
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceViewModel">ÅúÁ¿VM</param>
        /// <param name="container">°üº¬itemµÄÈÝÆ÷</param>
        /// <returns></returns>
        protected Queue<INebuUIView> RenderListview<T>(Queue<T> sourceViewModel, LayoutGroup container)
        {
            foreach (MainSystemLogViewModel viewmodel in sourceViewModel as Queue<MainSystemLogViewModel>)
            {
                m_logVMs.Enqueue(viewmodel);
                m_logView.Enqueue(RenderItem(viewmodel, container));
            }
            return m_logView;
        }

        /// <summary>
        /// µ¥ÌõÌí¼ÓviewItem
        /// </summary>
        /// <param name="content"></param>
        /// <param name="sender"></param>
        public void AddLog(DateTime time, string projectname, string sendername, string loglevel, string message)
        {
            if (m_logVMs == null)
                throw new InvalidOperationException("MainSystemLogPanel is not initialized yet!");
            var itemVM = new MainSystemLogViewModel()
            {
                LeeVM_Time = time,
                LeeVM_Loglevel = loglevel,
                LeeVM_Project = projectname,
                LeeVM_Sender = sendername,
                LeeVM_Message = message
            };
            m_logVMs.Enqueue(itemVM);
            m_logView.Enqueue(RenderItem(itemVM, this.systemLogContainer));
            logCountIndex++;
            if (m_logVMs.Count > maxLogNumber)//³¬³ö×î´óÊýÁ¿ºóÒÆ³ý×îÔçÈëÁÐµÄ¶ÔÏó
            {
                m_logVMs.Dequeue();
                var earliestItem = m_logView.Dequeue();
                Destroy(earliestItem.gameObject);
            }

            systemLogContainer.GetComponent<RectTransform>().anchoredPosition3D = logViewCurrentPos += logItemHeight;//×Ô¶¯¹ö¶¯Ò»ÐÐ
            //Debug.Log($"logViewCurrentPos {content} - Pos:{logViewCurrentPos}");
        }

        public void OnNext(NebuLogMsg message)
        {
            AddLog(message.TimeOfLog, message.ProjectName, message.SenderName, message.LogLevel, message.LoggingMessage);
        }
    }

}