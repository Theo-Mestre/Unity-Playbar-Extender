using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using UnityEditor.TestTools.TestRunner.Api;

namespace PlayBarExtender
{
    public class PlayBarExtenderWindow : EditorWindow
    {
        static PlayBarExtenderSettings settings;
        static SerializedObject serializedObject;

        [MenuItem("Tools/Play Bar Extender Settings")]
        public static void ShowWindow()
        {
            PlayBarExtenderWindow window = GetWindow<PlayBarExtenderWindow>();
            window.titleContent = new GUIContent("Play Bar Extender Settings");
            window.Show();

            settings = Resources.Load<PlayBarExtenderSettings>("PlayBarExtender/PlayBarExtenderSettings");
            if (settings == null)
            {
                Debug.LogError("PlayBarExtenderSettings not found. Please Create One");
                return;
            }
            serializedObject = new SerializedObject(settings, window);
        }

        private void OnGUI()
        {
            if (settings == null) return;

            EditorGUILayout.LabelField("Play Bar Extender Settings", EditorStyles.boldLabel);

            EditorGUILayout.Space();
            settings.UseDefaultSpawnFunctions = EditorGUILayout.Toggle("Use Default Spawn Functions", settings.UseDefaultSpawnFunctions, GUILayout.ExpandWidth(true));

            EditorGUILayout.Space();
            serializedObject.Update();
            SerializedProperty serializedProperty = serializedObject.FindProperty("PlayerFromHereFunctions");
            EditorGUILayout.PropertyField(serializedProperty);
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(settings);
            }
        }
    }
}