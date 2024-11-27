using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.M11D27.UIFramework
{
    public class UIManager
    {
        private static UIManager instance;
        public static UIManager Instance {  get { return instance ??= new UIManager(); } }
        private UIManager() {
            panelPathDic = new Dictionary<PanelType, string>();
            panelObjDic = new Dictionary<PanelType, BasePanel>();
            panelStack = new Stack<BasePanel>();
            Init();
        }
        // 获取panel的路径
        private Dictionary<PanelType, string> panelPathDic;
        // 获取panel游戏对象
        private Dictionary<PanelType, BasePanel> panelObjDic;
        // 存放panel的栈
        private Stack<BasePanel> panelStack;

        private GameObject canvasObj;


        void Init()
        {
            canvasObj = GameObject.Find("Canvas");
            if (canvasObj == null)
            {
                Debug.LogError($"找不到Canvas游戏对象，请创建.");
                return;
            }
            UIPanelList uiPanelList = Resources.Load<UIPanelList>("UIPanelList");
            foreach (var panel in uiPanelList.panelList)
            {
                panelPathDic.Add(panel.type, panel.path);
            }
        }


        /// <summary>
        /// 显示Panel
        /// </summary>
        /// <param name="panelType">panel类型</param>
        public BasePanel PushPanel(PanelType panelType) {
            if(panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            BasePanel currPanel = GetPanel(panelType);

            currPanel.transform.SetAsLastSibling(); // 在最上方
            currPanel.OnEnter();
            panelStack.Push(currPanel);

            return currPanel;
        }

        BasePanel GetPanel(PanelType panelType) {
            BasePanel panel;

            if (panelObjDic.TryGetValue(panelType, out panel))
            {
                return panel;
            }

            if(panelPathDic.TryGetValue(panelType, out string path))
            {
                GameObject prefab = Resources.Load<GameObject>(path);
                GameObject instObj = GameObject.Instantiate(prefab, canvasObj.transform);

                panel = instObj.GetComponent<BasePanel>();
                panelObjDic.Add(panelType, panel);
                return panel;
            }

            Debug.Log($"GetPanel: panel类型<{panelType}>不正确...");
            throw new ArgumentException($"Panel类型({panelType})不正确!!!");
        }

        /// <summary>
        /// 关闭panel
        /// </summary>
        public void PopPanel()
        {
            if (panelStack.Count == 0)
                return;

            BasePanel panel = panelStack.Pop();
            panel.OnExit();

            if (panelStack.Count > 0)
            {
                BasePanel nextPanel = panelStack.Peek();
                nextPanel.transform.SetAsLastSibling(); // 在最上方
                nextPanel.OnResume();
            }
        }
    }
}
