using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace M12D06
{
    public class Loading : MonoBehaviour
    {
        public static string SceneName = "Scene1";

        public static Action<int> OnDisplayProgress;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(LoadScene(SceneName));
        }

        IEnumerator LoadScene(string sceneName)
        {
            int displayProgress = 0;
            int toProgress = 0;

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;

            while (op.progress < 0.9f)
            {
                toProgress = (int)(op.progress * 100);
                while (displayProgress < toProgress)
                {
                    displayProgress++;
                    if (OnDisplayProgress != null)
                        OnDisplayProgress(displayProgress);
                    yield return 0;
                }
            }

            toProgress = 100;
            while (displayProgress < toProgress)
            {
                displayProgress++;
                if (OnDisplayProgress != null)
                    OnDisplayProgress(displayProgress);
                yield return 0;
            }

            op.allowSceneActivation = true;
        }
    }
}
