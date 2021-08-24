using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VladB.Utility {
    public class CurrencyUI : MonoBehaviour {
        [Header("Currency")]
        public Currency currency;

        protected int currencyValue;

        protected virtual void UpdateCurrencyValue(int _currencyValue, int _delta = 0) {
            currencyValue = _currencyValue;
        }

        public virtual void UpdateCurrencyValue() {
            UpdateCurrencyValue(currency.currencyValue);
        }

        void OnEnable() => OnEnableFunc();
        protected virtual void OnEnableFunc() {
            currency.OnCurrencyChange += UpdateCurrencyValue;
            UpdateCurrencyValue();
        }


        void OnDisable() => OnDisableFunc();
        protected virtual void OnDisableFunc() {
            currency.OnCurrencyChange -= UpdateCurrencyValue;
        }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() { }
    }
}