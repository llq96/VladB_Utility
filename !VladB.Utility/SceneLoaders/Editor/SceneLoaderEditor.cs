using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace VladB.Utility {
    [CustomEditor(typeof(SceneLoader))]
    public class SceneLoaderEditor : Editor {
        SceneLoader script => target as SceneLoader;
        public override void OnInspectorGUI() {
            EditorGUILayout.HelpBox(script.fullLog, MessageType.Info);
        }
    }
}