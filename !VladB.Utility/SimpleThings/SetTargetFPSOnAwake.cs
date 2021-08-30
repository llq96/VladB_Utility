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

        void SetTargetFPS(int fps) {
            if(fps > 0) {
                Application.targetFrameRate = fps;
            }
        }

    }


#if UNITY_EDITOR
    [CustomEditor(typeof(SetTargetFPSOnAwake))]
    public class SetTargetFPSOnAwakeEditor : Editor {
        //private SetTargetFPSOnAwake script => target as SetTargetFPSOnAwake;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox("Works only in the editor", MessageType.None);
        }
    }
#endif
}
