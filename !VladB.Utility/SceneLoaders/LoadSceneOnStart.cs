using UnityEngine;

namespace VladB.Utility {
    public class LoadSceneOnStart : MonoBehaviour {
        [Header ("Settings")]
        [SerializeField] string levelName;
        [SerializeField] bool isAdditive;

        void Start() {
            SceneLoader.LoadScene(levelName, isAdditive);
        }
    }
}
