using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class CurrencyUI_Smooth : CurrencyUI {
        //[Header ("Smooth Currency:")]
        protected float smoothCurrencyValue = 0f;

        //[Header("Smooth Currency Settings")]
        float currencyUpdateSpeed;

        protected virtual void UpdateCurrencyUI() {
            if(smoothCurrencyValue != currencyValue) {
                smoothCurrencyValue = Mathf.MoveTowards(smoothCurrencyValue, currencyValue, RealTime.deltaTime * currencyUpdateSpeed);
            }
        }

        protected override void UpdateCurrencyValue(int _currencyValue, int _delta = 0) {
            base.UpdateCurrencyValue(_currencyValue, _delta);
            currencyUpdateSpeed = Mathf.Max(100, Mathf.Abs(smoothCurrencyValue - _currencyValue));
        }

        protected override void OnEnableFunc() {
            base.OnEnableFunc();
            smoothCurrencyValue = currencyValue;
            UpdateCurrencyUI();
        }

        protected override void UpdateFunc() {
            base.UpdateFunc();
            UpdateCurrencyUI();
        }


    }
}