using Assets.Scripts.M11D27.UIFramework;
using UnityEngine.UI;

namespace Assets.Scripts.M11D27.Panel
{
    public class SettingPanel : BasePanel
    {
        public Button closeBtn;


        void OnEnable()
        {
            closeBtn.onClick.AddListener(OnCloseBtnClick);
        }

        void OnDisable()
        {
            closeBtn.onClick.RemoveListener(OnCloseBtnClick);
        }


        void OnCloseBtnClick()
        {
            UIManager.Instance.PopPanel();
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
