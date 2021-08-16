using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace VladB.Utility {
    [CustomEditor(typeof(CustomDebugEvents))]
    public class CustomDebugEventsEditor : Editor {
        public static readonly string[] eventsFields = {
            "events_withoutParams",
            "events_int",
            "events_float",
            "events_string"
        };

        CustomDebugEvents script { get { return target as CustomDebugEvents; } }

        List<bool> isShow { get { return script.isShow; } set { isShow = value; } }

        public override void OnInspectorGUI() {
            if (serializedObject == null) {
                return;
            }
            serializedObject.Update();

            if (!script.isCustomEditorGUI) {
                if (GUILayout.Button("EnableCustomEditor")) {
                    script.isCustomEditorGUI = true;
                    //base.OnInspectorGUI();
                    //return;
                } else {
                    base.OnInspectorGUI();
                    return;
                }
            }

            int count = eventsFields.Length;
            if(eventsFields.Length != System.Enum.GetNames(typeof(CustomEventType)).Length) {
                Debug.LogError("Careful: eventsFields.Length != CustomEventType.Length");
            }


            if (isShow.Count != count) {
                isShow.Clear();
                for (int i = 0; i < count; i++) {
                    isShow.Add(false);
                }
            }

            bool[] toggles = new bool[count];

            for (int i = 0; i < count; i++) {
                toggles[i] = EditorGUILayout.Toggle(eventsFields[i], isShow[i], new GUILayoutOption[0]);
                if (isShow[i] != toggles[i]) {
                    isShow[i] = toggles[i];
                }
            }

            for (int i = 0; i < count; i++) {
                if (script.isShow[i]) {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(eventsFields[i]), new GUILayoutOption[0]);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}


