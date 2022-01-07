using UnityEngine;

namespace VladB.Utility {
    public class LoadSceneOnStart : MonoBehaviour {
        [Header ("Loading Settings")]
        [SerializeField] string levelName;
        [SerializeField] bool isAdditive;

        void Start() {
            LoadScene();
        }

        public void LoadScene() {
            SceneLoader.LoadScene(levelName, isAdditive);
        }
    }
}
