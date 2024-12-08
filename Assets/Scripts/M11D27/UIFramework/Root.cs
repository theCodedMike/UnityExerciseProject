using UnityEngine;

namespace M11D27.UIFramework
{
    public class Root : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UIManager.Instance.PushPanel(PanelType.Login);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
