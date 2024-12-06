using UnityEngine;
using UnityEngine.SceneManagement;

namespace M12D06
{
    public class ChangeScene : MonoBehaviour
    {
        public string nextScene;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Loading.SceneName = nextScene;
                SceneManager.LoadScene("Loading");
            }
        }
    }
}