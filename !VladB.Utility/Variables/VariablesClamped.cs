using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {

    [System.Serializable]
    public class VariableIntClamped : VariableInt {
        [Header("Min/Max Values")]
        public int minValue = 0;
        public int maxValue = int.MaxValue;
        public override int value {
            get => base.value;
            set => base.value = Mathf.Clamp(value, minValue, maxValue);
        }
    }


    [System.Serializable]
    public class VariableFloatClamped : VariableFloat {
        [Header("Min/Max Values")]
        public float minValue = 0;
        public float maxValue = float.MaxValue;
        public override float value {
            get => base.value;
            set => base.value = Mathf.Clamp(value, minValue, maxValue);
        }
    }

}