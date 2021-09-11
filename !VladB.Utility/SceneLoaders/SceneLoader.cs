#define AlwaysLog
#undef AlwaysLog //Закоментить если нужен дебаг на устройствах

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VladB.Utility {
    public class SceneLoader : MonoBehaviour {
        static SceneLoader __instance;
        static SceneLoader instance {
            get {
                if(__instance == null) {
                    __instance = (SceneLoader)FindObjectOfType(typeof(SceneLoader));

                    if(__instance == null) {
                        GameObject obj = new GameObject();
                        __instance = obj.AddComponent<SceneLoader>();
                        obj.name = "SceneLoader";
                        DontDestroyOnLoad(obj);
                    }
                }

                return __instance;
            }
        }

        #region Properties
        public static bool isUnLoading { get; private set; }
        public static bool isLoading { get; private set; }
        public static bool isLoading_Or_UnLoading => (isLoading || isUnLoading);
        public static bool isAnyAction => isLoading_Or_UnLoading;
        #endregion

        #region ForEditor
#if UNITY_EDITOR
        [HideInInspector] public string fullLog = "";
#endif
        #endregion


        #region UnLoad/Load Static Methods  
        public static void UnLoadScene(string unLoadScene)
            => instance.UnLoadSceneMethod(unLoadScene);
        public static void LoadScene(string loadScene, bool isAdditive)
            => instance.LoadSceneMethod(loadScene, isAdditive);
        public static void LoadScene(string unLoadScene, string loadScene, bool isAdditive)
            => instance.LoadSceneMethod(unLoadScene, loadScene, isAdditive);
        #endregion

        #region UnLoading/Loading Methods 
        void UnLoadSceneMethod(string unLoadScene) {
            if(!isAnyAction) {
                StartCoroutine(UnloadAndLoad_Cor(unLoadScene, null, false));
            }
        }

        void LoadSceneMethod(string loadScene, bool isAdditive) {
            if(!isAnyAction) {
                StartCoroutine(UnloadAndLoad_Cor(null, loadScene, isAdditive));
            }
        }

        void LoadSceneMethod(string unLoadScene, string loadScene, bool isAdditive) {
            if(!isAnyAction) {
                StartCoroutine(UnloadAndLoad_Cor(unLoadScene, loadScene, isAdditive));
            }
        }

        IEnumerator UnloadAndLoad_Cor(string unLoadScene, string loadScene, bool isAdditive) {
            if(unLoadScene.IsHaveSomething()) {
                DebugLog($"UnLoadScene: {unLoadScene}");

                isUnLoading = true;
                AsyncOperation asyncUnLoad = SceneManager.UnloadSceneAsync(unLoadScene);
                yield return asyncUnLoad;
                isUnLoading = false;
            }


            if(loadScene.IsHaveSomething()) {
                DebugLog($"LoadScene ({(isAdditive ? "Additive" : "Single")}): {loadScene}");

                isLoading = true;
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadScene, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
                yield return asyncLoad;
                isLoading = false;
            }
        }
        #endregion

        #region Debug
        void DebugLog(string log) {
#if (UNITY_EDITOR || AlwaysLog)
            Debug.Log(log);
#if UNITY_EDITOR
            fullLog += $"{log} \n";
#endif
#endif
        }
        #endregion
    }
}