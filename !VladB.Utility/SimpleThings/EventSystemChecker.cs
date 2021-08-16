using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VladB.Utility {
    public class EventSystemChecker : MonoBehaviour {
        public GameObject eventSystemObject;

        private void Awake() {
            if (EventSystem.current == null) {
                if (eventSystemObject) {
                    eventSystemObject.SetActive(true);
                }
            }
        }

    }
}