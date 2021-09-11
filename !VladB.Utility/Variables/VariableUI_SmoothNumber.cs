using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public abstract class VariableUI_SmoothNumber<T> : VariableUI<T>{
        [Header("Smooth Settings")]
        public float minChangeSpeed = 100f;

        protected float smoothVariableValue = 0f;
        protected float variableValueChangeSpeed;

        public override void UpdateVariableUI() {
            if(smoothVariableValue != Convert.ToSingle(variableValue)) {
                smoothVariableValue = Mathf.MoveTowards(smoothVariableValue, Convert.ToSingle(variableValue), RealTime.deltaTime * variableValueChangeSpeed);
            }
        }

        protected override void UpdateVariableValue(T newValue, T oldValue, T deltaValue) {
            base.UpdateVariableValue(newValue, deltaValue , deltaValue);
            variableValueChangeSpeed = Mathf.Max(minChangeSpeed, Mathf.Abs(smoothVariableValue - Convert.ToSingle(newValue)));
        }

        protected override void OnEnableFunc() {
            base.OnEnableFunc();
            smoothVariableValue = Convert.ToSingle(variableValue);
            UpdateVariableUI();
        }

        protected override void UpdateFunc() {
            base.UpdateFunc();
            UpdateVariableUI();
        }


    }
}