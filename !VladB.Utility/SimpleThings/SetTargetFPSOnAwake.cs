using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
    public class SetTargetFPSOnAwake : MonoBehaviour {
        [Header("Target FPS")]
        public int targetFPS = 30;

        private void Awake() {
#if UNITY_EDITOR
            SetTargetFPS(targetFPS);
#endif
        }

        void SetTargetFPS(int _fps) {
            if (_fps > 0) {
                Application.targetFrameRate = _fps;
            }
        }

    }


#if UNITY_EDITOR
    [CustomEditor(typeof(SetTargetFPSOnAwake))]
    public class SetTargetFPSOnAwakeEditor : Editor {

        //private SetTargetFPSOnAwake script { get { return target as SetTargetFPSOnAwake; } }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            serializedObject.Update();

            if (serializedObject == null) {
                return;
            }

            EditorGUILayout.HelpBox("Works only in the editor", MessageType.None);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

}
