using System;
using M12D16;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Editor.Scripts
{
    public class EnemyConfigWindow : EditorWindow
    {
        private EnemyConfig config;

        private ReorderableList reorderableList;


        private void Awake()
        {
            config = Resources.Load<EnemyConfig>("EnemyConfig");
        }

        private void OnEnable()
        {
            reorderableList = new ReorderableList(config.enemies, typeof(Enemy), true, true, true, true);
            reorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 2.5f;
            reorderableList.drawElementCallback += DrawElementCallback;
            reorderableList.onRemoveCallback += OnRemoveCallback;
            reorderableList.onSelectCallback += OnSelectCallback;



        }

        private void OnSelectCallback(ReorderableList list)
        {
            Enemy enemy = list.list[list.index] as Enemy;
            Debug.Log(enemy.ToString());
        }

        private void OnRemoveCallback(ReorderableList list)
        {
            if (EditorUtility.DisplayDialog("警告", "是否确认删除？", "是", "否"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }
        }

        private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            rect.y += 2;
            Enemy enemy = config.enemies[index];
            
            EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Enemy" + index);
            rect.y += EditorGUIUtility.singleLineHeight * 1.1f;
            
            EditorGUI.LabelField(new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight), "name");
            rect.x += 50;
            enemy.name = EditorGUI.TextField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), enemy.name);
            rect.x += 50;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight), "hp");
            rect.x += 50;
            enemy.hp = EditorGUI.IntField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), enemy.hp);
            rect.x += 50;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, 40, EditorGUIUtility.singleLineHeight), "attack");
            rect.x += 50;
            enemy.attack = EditorGUI.IntField(new Rect(rect.x, rect.y, 50, EditorGUIUtility.singleLineHeight), enemy.attack);
            rect.x += 50;
        }

        [MenuItem("Window/Enemy Config Window")]
        static void OpenEnemyConfigWindow()
        {
            EnemyConfigWindow cw = GetWindow<EnemyConfigWindow>();
            cw.Show();
            cw.minSize = new Vector2(800, 600);
        }


        private void OnGUI()
        {
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

            GUILayout.Label("Enemy Config Window", style);


            reorderableList.DoLayoutList();
        }
    }
}
