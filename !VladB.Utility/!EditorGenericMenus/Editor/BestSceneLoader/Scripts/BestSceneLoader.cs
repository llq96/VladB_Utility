using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "BestSceneLoader", menuName = "VladB/GenericMenu/BestSceneLoader", order = 2)]
    public class BestSceneLoader : GenericMenuBase {
        bool isAdditionalLoad;


        public void OpenSceneLoader_Singe() { //Может вызываться через UnityEvents, не удалять.
            isAdditionalLoad = false;
            GenerateGenericMenu();
        }

        public void OpenSceneLoader_Additional() { //Может вызываться через UnityEvents, не удалять.
            isAdditionalLoad = true;
            GenerateGenericMenu();
        }

        protected override bool GenerateGenericMenu() {
            if (!base.GenerateGenericMenu()) {
                return false;
            }

            //Заполнение меню сценами идущими в билд
            AddScenes_FromBuildSettings();

            menu.AddSeparator("");

            //Заполнение меню остальными сценами
            AddScenes_Other();

            //Показ меню в месте курсора мыши
            ShowGenericMenuAtCursorPosition();
            return true;
        }

        #region Addiing Items
        void AddScenes_FromBuildSettings() {
            EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
            string[] paths = new string[buildScenes.Length];
            for (int i = 0; i < buildScenes.Length; i++) {
                paths[i] = buildScenes[i].path;
            }
            AddMenuItems(paths);
        }

        void AddScenes_Other() {
            string[] guids = AssetDatabase.FindAssets("t:Scene");
            string[] paths = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                paths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
            }
            AddMenuItems(paths);
        }

        void AddMenuItems(string[] paths) {
            string path;
            for (int i = 0; i < paths.Length; i++) {
                path = paths[i];
                AddMenuItem($"{path.Substring(path.LastIndexOf("/") + 1)}", path);
            }
        }
        #endregion

        protected override void OnClicked(object scenePath) {
            string path = (string)scenePath;
            if (!Application.isPlaying) {
                EditorSceneManager.SaveOpenScenes();
                //EditorSceneManager.sceneSaved += scene => {
                //    Debug.Log("Saved");
                //};
                EditorSceneManager.OpenScene(path, isAdditionalLoad ? OpenSceneMode.Additive : OpenSceneMode.Single);
            } else {
                string _name = path.Substring(path.LastIndexOf("/") + 1);
                //Debug.Log(_name);
                UnityEngine.SceneManagement.SceneManager.LoadScene(_name.Remove(_name.Length - 6),
                    isAdditionalLoad ? UnityEngine.SceneManagement.LoadSceneMode.Additive : UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }


    }
}