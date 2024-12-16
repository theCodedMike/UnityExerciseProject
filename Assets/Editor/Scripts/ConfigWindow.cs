using M12D16;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public class ConfigWindow : EditorWindow
    {
        //public GameObject obj;
        private EnemyConfig config;

        private void Awake()
        {
            config = Resources.Load<EnemyConfig>("EnemyConfig");
        }


        [MenuItem("Window/Test Config Window")]
        static void OpenConfigWindow()
        {
            ConfigWindow cw = GetWindow<ConfigWindow>();
            cw.Show();
            cw.minSize = new Vector2(800, 600);
        }


        // 每帧刷新2次
        private void OnGUI()
        {
            /*GUI.Button(new Rect(100, 100, 80, 30), "使用");
            GUI.Label(new Rect(200, 100, 600, 30), "ConfigWindow");
            obj = EditorGUI.ObjectField(new Rect(100, 300, 80, 25), obj, typeof(GameObject), false) as GameObject;*/

            GUIStyle style = new()
            {
                normal =
                {
                    textColor = Color.green
                },
                alignment = TextAnchor.MiddleCenter,
                fontSize = 36,
                fixedHeight = 80,
            };

            GUILayout.Label("Test Config Window", style);

            foreach (Enemy enemy in config.enemies)
            {
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.LabelField("  Name: ");
                enemy.name = EditorGUILayout.TextField(enemy.name);
                EditorGUILayout.LabelField("    HP: ");
                enemy.hp = EditorGUILayout.IntField(enemy.hp);
                EditorGUILayout.LabelField("Attack: ");
                enemy.attack = EditorGUILayout.IntField(enemy.attack);

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add"))
            {
                Enemy enemy = new Enemy();
                config.enemies.Add(enemy);
            }

            if (GUILayout.Button("Remove"))
            {
                config.enemies.RemoveAt(config.enemies.Count - 1);
            }
        }
    }
}
