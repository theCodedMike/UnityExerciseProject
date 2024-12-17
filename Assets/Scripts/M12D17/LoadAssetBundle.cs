using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace M12D17
{
    public class LoadAssetBundle : MonoBehaviour
    {
        [Header("测试cube")]
        public Transform cube;

        private Dictionary<string, AssetBundle> dic = new Dictionary<string, AssetBundle>();
        private AssetBundleManifest manifest;


        // Start is called before the first frame update
        void Start()
        {
            //StartCoroutine(LoadAssetBundleFromNet());
            
            //StartCoroutine(LoadTextureAssetBundleFromNet());

            StartCoroutine(LoadManifest(OnLoadComplete));
        }

        private void OnLoadComplete()
        {
            string[] assetBundles = manifest.GetAllAssetBundles();
            foreach (string assetBundle in assetBundles)
            {
                print(assetBundle);
            }
        }


        // 场景打包
        IEnumerator LoadScene()
        {
            string uri = Application.streamingAssetsPath + "/PC/gamescene";
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
                print("Error: " + request.result);
            else
            {
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                if (ab.isStreamedSceneAssetBundle)
                    SceneManager.LoadScene("MyScene");

                ab.Unload(false);
            }
        }


        IEnumerator LoadManifest(Action action)
        {
            string uri = Application.streamingAssetsPath + "/PC/PC";
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
                print("Error: " + request.result);
            else
            {
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                action();
            }
        }


        IEnumerator LoadTexture(string bindleName, string varName)
        {
            string[] strs = manifest.GetAllAssetBundlesWithVariant();

            foreach (string str in strs)
            {
                string uri = Application.streamingAssetsPath + "/PC/" + str;
                UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
                yield return request.SendWebRequest();

                if(request.result != UnityWebRequest.Result.Success)
                    print("Error: " + request.result);
                else
                {
                    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                    Texture texture = ab.LoadAsset<Texture>("1");

                    cube.GetComponent<MeshRenderer>().material.mainTexture = texture;
                }
            }
        }

        // 从服务器读取AssetBundle
        IEnumerator LoadAssetBundleFromNet(string bundleName, string assetName)
        {
            string[] dependencies = manifest.GetAllDependencies(bundleName);
            foreach (string dependency in dependencies)
            {
                if (!dic.ContainsKey(dependency))
                {
                    string depUri = Application.streamingAssetsPath + "/PC/" + dependency;
                    UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(depUri);
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                        print("Error Occurs: " + www.result);
                    else
                    {
                        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www);
                        dic.Add(dependency, ab);
                    }
                }
            }

            string uri = Application.streamingAssetsPath + "/PC/" + bundleName;
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
                print("Error: " + request.result);
            else
            {
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                GameObject go = ab.LoadAsset<GameObject>(assetName);
                Instantiate(go);
            }
        }

        // 从本地读取AssetBundle
        IEnumerator LoadAssetBundleFromLocal()
        {
            string url = Application.streamingAssetsPath + "/PC/obj";
            print(url);

            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
            yield return request;

            AssetBundle ab = request.assetBundle;
            AssetBundleRequest abRequest = ab.LoadAssetAsync<GameObject>("Cube");
            yield return abRequest;

            Instantiate(abRequest.asset);
        }


        // 从服务器读取贴图
        IEnumerator LoadTextureAssetBundleFromNet()
        {
            string url = Application.streamingAssetsPath + "/PC/frame_char";
            print(url);
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                print("Error: " + request.result);
            }
            else
            {
                AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                Texture texture = ab.LoadAsset<Texture>("frame_char");
                GetComponent<MeshRenderer>().material.mainTexture = texture;
                ab.Unload(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(LoadAssetBundleFromNet("sphere", "Sphere"));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(LoadAssetBundleFromNet("obj", "Cube"));
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                StartCoroutine(LoadTexture("tex", "hd"));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(LoadTexture("tex", "sd"));
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(LoadScene());
            }
        }
    }
}
