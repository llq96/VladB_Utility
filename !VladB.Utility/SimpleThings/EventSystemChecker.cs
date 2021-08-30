using UnityEngine;
using UnityEngine.EventSystems;

namespace VladB.Utility {
    public class EventSystemChecker : MonoBehaviour {
        [SerializeField] GameObject eventSystemObject;

        private void Awake() {
            if (EventSystem.current == null) {
                if (eventSystemObject) {
                    eventSystemObject.SetActive(true);
                }
            }
        }

    }
}