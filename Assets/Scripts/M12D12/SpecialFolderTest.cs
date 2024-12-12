using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace M12D12
{
    // Assets
    //   |-- Editor（可以有多个）
    //   |-- Editor Default Resources（只能有1个，可通过 EditorGUIUtility.Load 加载资源）
    //   |-- Gizmos（只能有1个，可通过 Gizmos.DrawIcon 加载资源）
    //   |-- Plugins
    //          |-- Android（放置安卓系统用到的资源dll）
    //          |-- IOS（放置IOS系统用到的资源dll）
    //   |-- Resources（文件不需后缀）
    //   |-- StreamingAssets（文件需后缀）
    //   |-- Standard Assets
    //   |-- 
    //   |-- 
    //   |-- 
    public class SpecialFolderTest : MonoBehaviour
    {
        private MeshRenderer cubeRenderer;

        private Texture[] imgs;

        private string fileName = "frame_char.png";

        private string filePath = null;

        // Start is called before the first frame update
        void Start()
        {
            cubeRenderer = GetComponent<MeshRenderer>();
            imgs = Resources.LoadAll<Texture>("UI/Bag");
            filePath = Application.streamingAssetsPath + "/Bg/" + fileName;

            print(Application.persistentDataPath); // 沙盒路径 C:/Users/Students/AppData/LocalLow/DefaultCompany/UnityExerciseProject
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cubeRenderer.material.mainTexture = imgs[Random.Range(0, imgs.Length)];
                //Resources.UnloadAsset();
                //Resources.UnloadUnusedAssets();
            }


            // Application.streamingAssetsPath
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(Load(filePath, LoadCompleteCallback));
            }


            // Application.persistentDataPath
            if (Input.GetKeyDown(KeyCode.O))
            {
                string path = Application.persistentDataPath + "/config.txt";
                
                //File.WriteAllText(filePath, "hello world");
                
                print(File.ReadAllText(path));
            }
        }

        IEnumerator Load(string path, Action<Texture2D> onLoadComplete)
        {
            UnityWebRequest request = new UnityWebRequest(filePath);
            request.downloadHandler = new DownloadHandlerTexture();
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D t2d = DownloadHandlerTexture.GetContent(request);
                onLoadComplete(t2d);
            }
            else
            {
                print(request.result);
            }
        }

        void LoadCompleteCallback(Texture2D t2d)
        {
            cubeRenderer.material.mainTexture = t2d;
        }
    }
}
