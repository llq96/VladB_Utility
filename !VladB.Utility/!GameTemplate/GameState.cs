using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.GameTemplate {
    public class GameState : MonoBehaviour {
        public bool isDebugLog;

        public virtual GameStateEnum state { get; private set; }
        public virtual GameStateEnum GetState() => state;
        public virtual bool isGame => state == GameStateEnum.Game;

        public delegate void GameStateChanged(GameStateEnum state);
        public event GameStateChanged OnGameStateChanged;


        public virtual void SetGameState(GameStateEnum state) {
            this.state = state;

            if(isDebugLog) {
                Debug.Log("OnGameStateChanged , state = " + state);
            }

            OnGameStateChanged?.Invoke(state);
        }
    }

    public enum GameStateEnum {
        None,
        BeginUnloadingLevel,
        EndUnloadingLevel,
        StartLoadLevel,
        EndLoadLevel,
        Start,
        Game,
        GameOver
        //TODO BeginGameOver
        //TODO Pausing
        //TODO UnPausing
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(GameState))]
    public class CommentEditor : Editor {
        GameState script => target as GameState;

        public override void OnInspectorGUI() {
            if(serializedObject == null) {
                return;
            }
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("isDebugLog"), new GUILayoutOption[0]);

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
