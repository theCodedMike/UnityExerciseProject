using System;
using M12D16;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    [CustomEditor(typeof(Capsule))]
    [CanEditMultipleObjects]
    public class CapsuleEditor : UnityEditor.Editor
    {
        private SerializedProperty damageProp;

        private SerializedProperty armorProp;

        private SerializedProperty gunProp;


        private void OnEnable()
        {
            damageProp = serializedObject.FindProperty("damage");
            armorProp = serializedObject.FindProperty("armor");
            gunProp = serializedObject.FindProperty("gun");
        }

        /*public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));
            if (!damageProp.hasMultipleDifferentValues) 
                ProgressBar(damageProp.intValue / 100f, "Damage");

            EditorGUILayout.IntSlider(armorProp, 0, 100, new GUIContent("Armor"));
            if(!armorProp.hasMultipleDifferentValues)
                ProgressBar(armorProp.intValue / 100f, "Armor");

            EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun Object"));

            serializedObject.ApplyModifiedProperties();
        }*/

        public override void OnInspectorGUI()
        {
            Capsule mp = (Capsule)target;

            mp.damage = EditorGUILayout.IntSlider("Damage", mp.damage, 0, 100);
            ProgressBar(mp.damage / 100.0f, "Damage");

            mp.armor = EditorGUILayout.IntSlider("Armor", mp.armor, 0, 100);
            ProgressBar(mp.armor / 100.0f, "Armor");

            bool allowSceneObjects = !EditorUtility.IsPersistent(target);
            mp.gun = (GameObject)EditorGUILayout.ObjectField("Gun Object", mp.gun, typeof(GameObject), allowSceneObjects);
        }

        // Custom GUILayout progress bar.
        void ProgressBar(float value, string label)
        {
            // Get a rect for the progress bar using the same margins as a textfield:
            Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
            EditorGUI.ProgressBar(rect, value, label);
            EditorGUILayout.Space();
        }
    }
}
