using UnityEngine;

namespace VladB.Utility {
    public class ScreenShooter : MonoBehaviour {
        [Header ("Keys")]
        public KeyCode key;
        public KeyCode keyPause;

        void Start() {
#if !UNITY_EDITOR
            Destroy(gameObject);
#endif
        }

        void Update() {
            if (Input.GetKeyDown(key)) {
                ScreenCapture.CaptureScreenshot($"!Screens/Screen {Time.unscaledTime}.jpg");
            }
            
            if (Input.GetKeyDown(keyPause)) {
                Debug.Break();
            }
        }

    }
}

