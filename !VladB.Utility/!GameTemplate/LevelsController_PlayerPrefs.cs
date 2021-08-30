using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.GameTemplate {
    public class LevelsController_PlayerPrefs : MonoBehaviour {
        [Header("PlayerPrefs Strings")]
        [SerializeField] LC_PP_Settings playerPrefs;

        public virtual int currentLevel {
            get => PlayerPrefs.GetInt(playerPrefs.pp_prefix_gameName + playerPrefs.pp_currentLevel, 1);
            set => PlayerPrefs.SetInt(playerPrefs.pp_prefix_gameName + playerPrefs.pp_currentLevel, value);
        }

        public virtual int loadedLevel {
            get => PlayerPrefs.GetInt(playerPrefs.pp_prefix_gameName + playerPrefs.pp_loadedLevel, 0);
            set => PlayerPrefs.SetInt(playerPrefs.pp_prefix_gameName + playerPrefs.pp_loadedLevel, value);
        }

        public virtual int lastPassedLevel {
            get => PlayerPrefs.GetInt(playerPrefs.pp_prefix_gameName + playerPrefs.pp_lastPassedLevel, 0);
            set {
                if(value > lastPassedLevel) {
                    PlayerPrefs.SetInt(playerPrefs.pp_prefix_gameName + playerPrefs.pp_lastPassedLevel, value);
                }
            }
        }
    }


    [System.Serializable]
    public class LC_PP_Settings {
        public string pp_prefix_gameName = "GameName!!!";
        public string pp_currentLevel = "_LevelsController_currentLevel";
        public string pp_loadedLevel = "_LevelsController_loadedLevel";
        public string pp_lastPassedLevel = "_LevelsController_lastPassedLevel";
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(LevelsController_PlayerPrefs))]
    public class LevelsController_PlayerPrefsEditor : Editor {
        LevelsController_PlayerPrefs script => target as LevelsController_PlayerPrefs;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.Space(20);

            string text = "";
            text += $"Current Level = {script.currentLevel}" + "\n";
            text += $"Loaded Level = {script.loadedLevel}" + "\n";
            text += $"Last Passed Level = {script.lastPassedLevel}";

            EditorGUILayout.HelpBox(text, MessageType.None);
        }
    }
#endif
}