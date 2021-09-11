using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.Utility;

namespace VladB.Utility {

    public abstract class VariablePP<T> : Variable<T> {
        [Header ("Player Prefs Key")]
        public string ppKey = "";

        public VariablePP() {
            if(ppKey.IsNullOrEmpty()) {
                ppKey = Extensions.GetRandomPPKey();
            }
        }

        public override abstract T value {
            get; set;
        }

        protected override void CheckInit(bool isChangeValueToDefault) {
            if(!PlayerPrefs.HasKey(ppKey)) {
                base.CheckInit(isChangeValueToDefault);
            }
        }
    }

    [System.Serializable]
    public class VariablePPInt : VariablePP<int> {

        public override int value {
            get {
                CheckInit(true);
                return PlayerPrefs.GetInt(ppKey, defaulValue);
            }
            set {
                CheckInit(false);

                __oldValue = this.value;
                PlayerPrefs.SetInt(ppKey, value);
                SendEvents(value, __oldValue);
            }
        }

        protected override void SendEvents(int newValue, int oldValue) {
            base.SendEvents(newValue, oldValue);
            OnValueChanged_Ev3?.Invoke(newValue, oldValue, newValue - oldValue);
        }
    }

    [System.Serializable]
    public class VariablePPFloat : VariablePP<float> {
        public override float value {
            get {
                CheckInit(true);
                return PlayerPrefs.GetFloat(ppKey, defaulValue);
            }
            set {
                CheckInit(false);

                __oldValue = this.value;
                PlayerPrefs.SetFloat(ppKey, value);
                SendEvents(value, __oldValue);
            }
        }

        protected override void SendEvents(float newValue, float oldValue) {
            base.SendEvents(newValue, oldValue);
            OnValueChanged_Ev3?.Invoke(newValue, oldValue, newValue - oldValue);
        }
    }

    [System.Serializable]
    public class VariablePPString : VariablePP<string> {
        public override string value {
            get {
                CheckInit(true);
                return PlayerPrefs.GetString(ppKey, defaulValue);
            }
            set {
                CheckInit(false);

                __oldValue = this.value;
                PlayerPrefs.SetString(ppKey, value);
                SendEvents(value, __oldValue);
            }
        }

    }
}