using UnityEngine;
using UnityEngine.EventSystems;

namespace VladB.Utility {
    public class EventSystemChecker : MonoBehaviour {
        [Header("Object With EventSystem")]
        [SerializeField] GameObject eventSystemObject;

        void Awake() {
            if(EventSystem.current == null) {
                if(eventSystemObject) {
                    eventSystemObject.SetActive(true);
                }
            }
        }

    }
}