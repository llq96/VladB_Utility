using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {

    [System.Serializable]
    public class VariableInt : Variable<int> {
        protected override void SendEvents(int newValue, int oldValue) {
            base.SendEvents(newValue, oldValue);
            OnValueChanged_Ev3?.Invoke(newValue, oldValue, newValue - oldValue);
        }
    }

    [System.Serializable]
    public class VariableFloat : Variable<float> {
        protected override void SendEvents(float newValue, float oldValue) {
            base.SendEvents(newValue, oldValue);
            OnValueChanged_Ev3?.Invoke(newValue, oldValue, newValue - oldValue);
        }
    }

    [System.Serializable]
    public class VariableString : Variable<string> { }


    [System.Serializable]
    public class VariableVector2 : Variable<Vector2> { }


    [System.Serializable]
    public class VariableVector3 : Variable<Vector3> { }

}