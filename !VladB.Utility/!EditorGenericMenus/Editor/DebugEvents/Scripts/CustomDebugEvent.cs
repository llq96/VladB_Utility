using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace VladB.Utility {
    [System.Serializable]
    public class CustomEvent<T>{
        [Header("Visual Name")]
        public string visualName;

        public UnityEvent<T> unityEvent;

        public virtual void Invoke(object extraInfo = null) {
            unityEvent.Invoke((T)extraInfo);
        }

        public virtual void AddMenuItems(CustomDebugEvents _customDebugEvents, int _eventIndex) {
            int count = unityEvent.GetPersistentEventCount();
            if (count == 0) {
                Debug.LogError("0 Events");
            } else if (count == 1) {
                AddMenuItems(_customDebugEvents, _eventIndex, unityEvent.GetPersistentTarget(0), GetVisualName());
            } else {
                Debug.LogError("Works Only One Function In UnityEvents");
                //Проблематично вызывать только определённые функции из UnityEvent
            }
        }

        string GetVisualName() {
            return string.IsNullOrEmpty(visualName) ? unityEvent.GetPersistentMethodName(0) : visualName;
        }

        protected virtual void AddMenuItems(CustomDebugEvents _customDebugEvents, int _eventIndex, Object _target, string _visualName) {
            _customDebugEvents.AddMenuItem(_visualName, new SelectedItemInfo(CustomEventType.Basic, _eventIndex, null));
        }
    }





        [System.Serializable]
    public class CustomEvent_WithoutParams : CustomEvent<object> {}

    [System.Serializable]
    public class CustomEvent_int : CustomEvent<int> {
        [Header ("Settings")]
        public int minValue;
        [Range(1, 10000)]
        public int step = 1;
        public int maxValue = 10;

        protected override void AddMenuItems(CustomDebugEvents _customDebugEvents, int _eventIndex, Object _target, string _visualName) {
            if (step <= 0f) {
                Debug.LogError("Wrong Step");
                return;
            }

            for (int i = minValue; i <= maxValue; i += step) {
                _customDebugEvents.AddMenuItem(_visualName + "/" + i, new SelectedItemInfo(CustomEventType.Int, _eventIndex, i));
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

        protected override void AddMenuItems(CustomDebugEvents _customDebugEvents, int _eventIndex, Object _target, string _visualName) {
            if (step <= 0f) {
                Debug.LogError("Wrong Step");
                return;
            }

            for (float i = minValue; i <= maxValue; i += step) {
                _customDebugEvents.AddMenuItem(_visualName + "/" + i, new SelectedItemInfo(CustomEventType.Float, _eventIndex, i));
            }
        }
    }

    [System.Serializable]
    public class CustomEvent_String : CustomEvent<string> {
        [Header("Settings")]
        public string[] strings;

        protected override void AddMenuItems(CustomDebugEvents _customDebugEvents, int _eventIndex, Object _target, string _visualName) {
            if (strings == null) {
                return;
            }


            for (int i = 0; i < strings.Length; i++) {
                _customDebugEvents.AddMenuItem(_visualName + "/" + strings[i], new SelectedItemInfo(CustomEventType.String, _eventIndex, strings[i]));
            }
        }
    }
}