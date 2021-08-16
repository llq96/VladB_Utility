using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace VladB.Utility {
 public class ItemSpawner<TypeOfParent>: GenericMenuBase where TypeOfParent:Component{
        public SpawnSettings settings;

        TypeOfParent parent;

        public void OpenSpawner() {
            GenerateGenericMenu();
        }

        protected override bool IsCanGenerateMenu() {
            parent = FindObjectOfType<TypeOfParent>();
            if (parent == null) {
                settings.SpawnParent();
                parent = FindObjectOfType<TypeOfParent>();
                if (parent == null) {
                    Debug.LogError("Cant Find Parent On Scene");
                    return false;
                }
            }
            return true;
        }

        public override bool GenerateGenericMenu() {
            if (!base.GenerateGenericMenu()) {
                return false;
            }

            //Заполнение меню
            for (int i = 0; i < settings.arrays.Length; i++) {
                for (int k = 0; k < settings.arrays[i].prefabs.Length; k++) {
                    if (settings.arrays[i].prefabs[k] == null) {
                        Debug.LogError($"Have No Prefab in array - {settings.arrays[i].arrayName}");
                        continue;
                    }


                    if (settings.arrays[i].prefabs.Length > 1) {
                        AddMenuItem($"{((i < 9) ? (i + 1) + "" : "")}   {settings.arrays[i].arrayName} / {k + 1}   {settings.arrays[i].prefabs[k].name}", settings.arrays[i].prefabs[k]);
                    } else {
                        AddMenuItem($"{((i < 9) ? (i + 1) + "" : "")}   {settings.arrays[i].prefabs[k].name}", settings.arrays[i].prefabs[k]);
                    }
                }
            }

            //Показ меню в месте курсора мыши
            ShowGenericMenuAtCursorPosition();
            return true;
        }


        protected override void OnClicked(object _object) {
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab((Object)_object, parent.transform);
            //GameObject go =  (GameObject)PrefabUtility.InstantiatePrefab((Object)_object);
            Selection.activeGameObject = go;

            EditorSceneManager.MarkAllScenesDirty();
        }

    }



    [System.Serializable]
    public struct SpawnSettings{

        [Header("Array Of Prefabs")]
        public ItemSpawnerHelper_Array[] arrays;

        [Header("ParentPrefab")]
        public GameObject parentPrefab;
        public string baseName;

        public void SpawnParent() {
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(parentPrefab);
            if (!string.IsNullOrEmpty(baseName)) {
                go.name = baseName;
            }
           
        }

        [System.Serializable]
        public class ItemSpawnerHelper_Array {
            public string arrayName;
            public GameObject[] prefabs;
        }
    }
}