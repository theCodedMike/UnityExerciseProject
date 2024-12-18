using M11D27.UIFramework;
using UnityEngine.UI;

namespace M11D27.Panel
{
    public class BagPanel : BasePanel
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
            UIManager.Instance.PushPanel(PanelType.Main);
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

