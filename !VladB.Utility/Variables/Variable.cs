using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {

    [System.Serializable]
    public class Variable {
    }

    [System.Serializable]
    public class Variable<T> : Variable {
        [SerializeField] protected T defaulValue;

        protected bool isInited;

        public Action OnValueChanged;
        /// <summary>
        /// NewValue
        /// </summary>
        public Action<T> OnValueChanged_Ev1;
        /// <summary>
        /// NewValue, OldValue
        /// </summary>
        public Action<T, T> OnValueChanged_Ev2;
        /// <summary>
        /// NewValue, OldValue , Delta
        /// </summary>
        public Action<T, T, T> OnValueChanged_Ev3;

        protected T __value;
        protected T __oldValue;
        public virtual T value {
            get {
                CheckInit(true);
                return __value;
            }
            set {
                CheckInit(false);

                __oldValue = __value;
                __value = value;
                SendEvents(__value, __oldValue);
            }
        }

        protected virtual void CheckInit(bool isChangeValueToDefault) {
            if(!isInited) {
                isInited = true;
                if(isChangeValueToDefault) {
                    value = defaulValue;
                }
            }
        }

        protected virtual void SendEvents(T newValue, T oldValue) {
            OnValueChanged?.Invoke();
            OnValueChanged_Ev1?.Invoke(newValue);
            OnValueChanged_Ev2?.Invoke(newValue, oldValue);
        }
    }






}