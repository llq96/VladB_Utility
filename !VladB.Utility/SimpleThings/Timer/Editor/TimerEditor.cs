using UnityEngine;
using UnityEditor;


namespace VladB.Utility {
    [CustomEditor(typeof(Timer))]
    public class TimerEditor : Editor {
        Timer script => target as Timer;
        GUIStyle style;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (Application.isPlaying) {
                ShowTimer();
            }
        }

        void ShowTimer() {
            CheckStyle();

            EditorGUILayout.Space(20);

            float time = script.curValue;
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            int mili = Mathf.FloorToInt((time - (int)time) * 100f);

            style.normal.textColor = script.isTimerActive ? Color.green : Color.red;

            if (minutes == 0) {
                EditorGUILayout.LabelField($"{seconds}.{mili}", style);
            } else {
                EditorGUILayout.LabelField($"{minutes}:{seconds}.{mili}", style);
            }

            EditorGUILayout.Space(40);
        }

        void CheckStyle() {
            if(style == null){
                style = new GUIStyle {
                    alignment = TextAnchor.MiddleLeft,
                    fontSize = 40
                };
            }
        }
    }
}