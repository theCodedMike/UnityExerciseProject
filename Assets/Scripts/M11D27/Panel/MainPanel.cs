using M11D27.UIFramework;
using UnityEngine.UI;

namespace M11D27.Panel
{
    public class MainPanel : BasePanel
    {
        public Button settingBtn;
        public Button closeBtn;
        public Button bagBtn;

        private void OnEnable()
        {
            settingBtn.onClick.AddListener(OnSettingBtnClick);
            closeBtn.onClick.AddListener(OnCloseBtnClick);
            bagBtn.onClick.AddListener(OnBagBtnClick);
        }

        private void OnDisable()
        {
            settingBtn.onClick.RemoveListener(OnSettingBtnClick);
            closeBtn.onClick.RemoveListener(OnCloseBtnClick);
            bagBtn.onClick.RemoveListener(OnBagBtnClick);
        }

        private void OnSettingBtnClick()
        {
            UIManager.Instance.PushPanel(PanelType.Setting);
        }

        private void OnCloseBtnClick()
        {
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(PanelType.Login);

        }
        private void OnBagBtnClick()
        {
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(PanelType.Bag);
        }


        public void SetValue(string s)
        {
            print($"MainPanel: {s}");
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
