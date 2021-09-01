using UnityEngine;
using System;
using VladB.Utility;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.GameTemplate {
    public class GameState : MonoBehaviour {
        [Header("Debug")]
        public bool isDebugLog;

        public virtual GameStateEnum state { get; private set; }
        public virtual GameStateEnum GetState() => state;
        public virtual bool isGame => state == GameStateEnum.Game;

        public Action<GameStateEnum, object[]> OnGameStateChanged;


        public virtual void SetGameState(GameStateEnum state, params object[] parameters) {
            this.state = state;

            if(isDebugLog) {
                Debug.Log($"OnGameStateChanged: state = {state}");
                if(parameters.Length != 0) {
                    parameters.DebugAll();
                }
            }

            OnGameStateChanged?.Invoke(state, parameters);
        }
    }

    public enum GameStateEnum {
        None,
        BeginUnloadingLevel,
        EndUnloadingLevel,

        BeginLoadLevel,
        LevelLoaded,

        Start,
        Game,

        GameOver,

        Pausing,
        Pause,
        UnPausing
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(GameState))]
    public class CommentEditor : Editor {
        GameState script => target as GameState;

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("isDebugLog"));

            string text;
            if(!Application.isPlaying) {
                text = $"The state will be shown in RunTime";
            } else {
                text = $"State : {script.state}";
            }
            EditorGUILayout.HelpBox(text, MessageType.None);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
