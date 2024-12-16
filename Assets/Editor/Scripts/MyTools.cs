using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class MyTools
    {
        [MenuItem("Tools/MyTool/Change Color #&g")] // 这里必须有一个空格
        static void ChangeColor()
        {
            Debug.Log("123");
            Material redMat = Resources.Load<Material>("Materials/Red");
            GameObject obj = Selection.activeGameObject;

            MeshRenderer[] mrs = obj.GetComponentsInChildren<MeshRenderer>();
            foreach (var mr in mrs)
            {
                mr.sharedMaterial = redMat;
            }
        }


        // 必须选中某个游戏对象才能执行上面的逻辑
        [MenuItem("Tools/MyTool/Change Color #&g", true)]
        static bool ValidateForChangeColor()
        {
            //if (Selection.activeGameObject != null)
            //    return true;
            //return false;

            return Selection.activeGameObject;
        }


        [MenuItem("Tools/MyTool/Restore Color #&r")] // 这里必须有一个空格
        static void RestoreColor()
        {
            Material defaultMat = Resources.Load<Material>("Materials/Default");
            GameObject obj = Selection.activeGameObject;

            MeshRenderer[] mrs = obj.GetComponentsInChildren<MeshRenderer>();
            foreach (var mr in mrs)
            {
                mr.sharedMaterial = defaultMat;
            }
        }

        // 必须选中某个游戏对象才能执行上面的逻辑
        [MenuItem("Tools/MyTool/Restore Color #&r", true)]
        static bool ValidateForRestoreColor()
        {
            //if (Selection.activeGameObject != null)
            //    return true;
            //return false;

            return Selection.activeGameObject;
        }

        [MenuItem("Assets/Create/CustomFolder/Resources")]
        static void CreateResourcesFolder()
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        [MenuItem("Assets/Create/CustomFolder/Prefabs")]
        static void CreatePrefabsFolder()
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Prefabs");
        }

        [MenuItem("Assets/Create/CustomFolder/Scripts")]
        static void CreateScriptsFolder()
        {
            AssetDatabase.CreateFolder("Assets", "Scripts");
        }

        [MenuItem("Assets/Create/CustomMat")]
        static void CreateAsset()
        {
            Material redMat = Resources.Load<Material>("Materials/Red");
            Material material = new Material(redMat);
            AssetDatabase.CreateAsset(material, "Assets/Resources/Materials/NewRed.mat");
        }
    }
}
