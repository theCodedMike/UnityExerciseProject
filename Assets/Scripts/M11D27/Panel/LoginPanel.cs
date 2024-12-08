using M11D27.UIFramework;
using TMPro;
using UnityEngine.UI;

namespace M11D27.Panel
{
    public class LoginPanel : BasePanel
    {
        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public Button loginButton;
        public TMP_Text tip;

        void Start()
        {
            loginButton.interactable = false;
        }

        public override void OnExit()
        {
            base.OnExit();
            ClearText();
        }


        private void OnEnable()
        {
            loginButton.onClick.AddListener(OnLoginBtnClick);
            usernameInput.onValueChanged.AddListener(OnUsernameInputValueChange);
            passwordInput.onValueChanged.AddListener(OnPasswordInputValueChange);
        }

        private void OnDisable()
        {
            loginButton.onClick.RemoveListener(OnLoginBtnClick);
            usernameInput.onValueChanged.RemoveListener(OnUsernameInputValueChange);
            passwordInput.onValueChanged.RemoveListener(OnPasswordInputValueChange);
        }

        void OnLoginBtnClick()
        {
            if (usernameInput.text == "1" && passwordInput.text == "2")
            {
                tip.text = "登录成功";
                // 跳转
                UIManager.Instance.PopPanel();
                MainPanel mainPanel = UIManager.Instance.PushPanel(PanelType.Main) as MainPanel;
                // 相邻Panel之间可以传递参数，比如用户名，密码等
                mainPanel.SetValue("hello MainPanel");
            }
            else
            {
                tip.text = "登录失败";
                Invoke("ClearText", 2f);
            }
        }

        void ClearText()
        {
            tip.text = "";
            usernameInput.text = "";
            passwordInput.text = "";
        }

        void OnUsernameInputValueChange(string value)
        {
            if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
            {
                tip.text = "";
                loginButton.interactable = false;
                return;
            }
            loginButton.interactable = true;
        }

        void OnPasswordInputValueChange(string value)
        {
            if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
            {
                tip.text = "";
                loginButton.interactable = false;
                return;
            }
            loginButton.interactable = true;
        }
    }
}
