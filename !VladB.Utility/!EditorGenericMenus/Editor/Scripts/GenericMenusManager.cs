#define IsEnableGenericMenusManager //ВКЛючает менеджер
//#undef IsEnableGenericMenusManager //ВЫКЛючает менеджер, можно откомментировать если беспокоят конфликты с другими сочетаниями клавиш,
//пока что не придумал более элегантное, удобное и правильное решение.

using UnityEditor;
using UnityEngine;

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "GenericMenusManager", menuName = "VladB/GenericMenu/!GenericMenusManager", order = 1)]
    public class GenericMenusManager : ScriptableObject {
        [Header("Base Settings")]
        public bool isActive = true;
        public GenericMenuInfo[] menus;

        static GenericMenusManager instance {
            get {
                string[] guids = AssetDatabase.FindAssets("t:GenericMenusManager");
                string[] paths = new string[guids.Length];
                for (int i = 0; i < guids.Length; i++) {
                    paths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                    //Debug.Log(paths[i]);
                }

                GenericMenusManager[] all = new GenericMenusManager[paths.Length];
                for (int i = 0; i < all.Length; i++) {
                    all[i] = AssetDatabase.LoadAssetAtPath<GenericMenusManager>(paths[i]);
                    if (all[i].isActive) {
                        //Debug.Log("Instance: " + all[i].name);
                        return all[i];
                    }
                }

                Debug.LogError("GenericMenusManagers Do not exist in project!");
                return null;
            }
        }




        #region Static PressKeys Functions

        #if IsEnableGenericMenusManager

        //Help https://docs.unity3d.com/ScriptReference/MenuItem.html

        //TODO Если комбинация клавиш не назначены - открывать в окне Project файл с этим скриптом

        // KEY Q
        [MenuItem("Window/GenericMenusManager/CTRL_Q %q")]
        public static void PressKeys_CTRL_Q() {
            instance.TryGenerateGenericMenu(GemericMenuKeys.CTRL_Q);
        }

        [MenuItem("Window/GenericMenusManager/SHIFT_Q #q")]
        public static void PressKeys_SHIFT_Q() {
            instance.TryGenerateGenericMenu(GemericMenuKeys.SHIFT_Q);
        }

        // KEY W
        [MenuItem("Window/GenericMenusManager/CTRL_W %w")]
        public static void PressKeys_CTRL_W() {
            instance.TryGenerateGenericMenu(GemericMenuKeys.CTRL_W);
        }

        [MenuItem("Window/GenericMenusManager/SHIFT_W #w")]
        public static void PressKeys_SHIFT_W() {
            instance.TryGenerateGenericMenu(GemericMenuKeys.SHIFT_W);
        }

        // KEY E
        [MenuItem("Window/GenericMenusManager/CTRL_E %e")]
        public static void PressKeys_CTRL_E() {
            instance.TryGenerateGenericMenu(GemericMenuKeys.CTRL_E);
        }

        [MenuItem("Window/GenericMenusManager/SHIFT_E #e")]
        public static void PressKeys_SHIFT_E() {
            instance.TryGenerateGenericMenu(GemericMenuKeys.SHIFT_E);
        }

        [MenuItem("Window/GenericMenusManager/SHqweIFT_E #e")]
        public static void PressKeys_SHIFT_EEE() {
            //instance.TryGenerateGenericMenu(GemericMenuKeys.SHIFT_E);
        }
        #endif

        #endregion




        void TryGenerateGenericMenu(GemericMenuKeys keys) {
            if (menus != null) {
                for (int i = 0; i < menus.Length; i++) {
                    if (menus[i].keys == keys) {

                        for (int k = 0; k < menus[i].events.GetPersistentEventCount(); k++) {
                            menus[i].events.SetPersistentListenerState(k, UnityEngine.Events.UnityEventCallState.EditorAndRuntime);
                            //Debug.Log(menus[i].events.GetPersistentTarget(k));
                            //Debug.Log(menus[i].events.GetPersistentMethodName(k));
                        }
                        menus[i].events.Invoke();
                    }
                }
            }
        }






        [System.Serializable]
        public class GenericMenuInfo {
            public GemericMenuKeys keys;
            public UnityEngine.Events.UnityEvent events;
        }

        public enum GemericMenuKeys {
            CTRL_Q,
            SHIFT_Q,
            CTRL_W,
            SHIFT_W,
            CTRL_E,
            SHIFT_E,
        }
    }
}
