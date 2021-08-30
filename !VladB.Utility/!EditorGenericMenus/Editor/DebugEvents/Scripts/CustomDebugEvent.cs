using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace VladB.Utility {
    [System.Serializable]
    public abstract class CustomEvent {
        [Header("Visual Name")]
        public string visualName;

        public abstract void Invoke(object extraInfo = null);
        public abstract void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex);

        protected abstract string GetVisualName();
        protected abstract void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex, Object target, string visualName);
    }

    [System.Serializable]
    public abstract class CustomEvent<T> : CustomEvent {
        public UnityEvent<T> unityEvent;

        public override void Invoke(object extraInfo = null) {
            unityEvent.Invoke((T)extraInfo);
        }

        public override void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex) {
            int count = unityEvent.GetPersistentEventCount();
            if (count == 0) {
                Debug.LogError("0 Events");
            } else if (count == 1) {
                AddMenuItems(customDebugEvents, eventIndex, unityEvent.GetPersistentTarget(0), GetVisualName());
            } else {
                Debug.LogError("Works Only One Function In UnityEvents");
                //Проблематично вызывать только определённые функции из UnityEvent
            }
        }

        protected override string GetVisualName() {
            return string.IsNullOrEmpty(visualName) ? unityEvent.GetPersistentMethodName(0) : visualName;
        }
    }




    [System.Serializable]
    public class CustomEvent_WithoutParams : CustomEvent<object> {
        protected override void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex, Object target, string visualName) {
           customDebugEvents.AddMenuItem(visualName, new SelectedItemInfo(CustomEventType.Basic, eventIndex, null));
        }
    }

    [System.Serializable]
    public class CustomEvent_int : CustomEvent<int> {
        [Header ("Settings")]
        public int minValue;
        [Range(1, 10000)]
        public int step = 1;
        public int maxValue = 10;

        protected override void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex, Object target, string visualName) {
            if (step <= 0f) {
                Debug.LogError("Wrong Step");
                return;
            }

            for (int i = minValue; i <= maxValue; i += step) {
                customDebugEvents.AddMenuItem(visualName + "/" + i, new SelectedItemInfo(CustomEventType.Int, eventIndex, i));
            }
        }
    }

    [System.Serializable]
    public class CustomEvent_Float : CustomEvent<float> {
        [Header("Settings")]
        public float minValue;
        [Range(0.001f, 10000f)]
        public float step = 1;
        public float maxValue = 10;

        protected override void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex, Object target, string visualName) {
            if (step <= 0f) {
                Debug.LogError("Wrong Step");
                return;
            }

            for (float i = minValue; i <= maxValue; i += step) {
                customDebugEvents.AddMenuItem(visualName + "/" + i, new SelectedItemInfo(CustomEventType.Float, eventIndex, i));
            }
        }
    }

    [System.Serializable]
    public class CustomEvent_String : CustomEvent<string> {
        [Header("Settings")]
        public string[] strings;

        protected override void AddMenuItems(CustomDebugEvents customDebugEvents, int eventIndex, Object target, string visualName) {
            if (strings == null) {
                return;
            }


            for (int i = 0; i < strings.Length; i++) {
                customDebugEvents.AddMenuItem(visualName + "/" + strings[i], new SelectedItemInfo(CustomEventType.String, eventIndex, strings[i]));
            }
        }
    }
}