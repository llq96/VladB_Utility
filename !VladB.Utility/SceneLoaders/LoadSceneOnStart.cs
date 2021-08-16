using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VladB.Utility {
    public class LoadSceneOnStart : MonoBehaviour {
        public string levelName;
        public bool isAdditive;

        void Start() {
            if (!string.IsNullOrEmpty(levelName)) {
                StartCoroutine(LoadNewLevel(isAdditive));
            }
        }

        IEnumerator LoadNewLevel(bool isAdditive) {
            AsyncOperation async = SceneManager.LoadSceneAsync(levelName, isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            yield return async;
        }
    }
}
