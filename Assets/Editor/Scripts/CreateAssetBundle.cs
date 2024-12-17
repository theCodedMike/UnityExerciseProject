using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class CreateAssetBundle
    {
        [MenuItem("AssetBundle/CreatePCAssetBundle")]
        static void CreatePCAssetBundle()
        {
            string path = Application.streamingAssetsPath + "/PC";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
        }
    }
}
