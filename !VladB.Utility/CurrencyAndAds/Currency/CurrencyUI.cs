using UnityEngine;


namespace VladB.Utility {
    public class CurrencyUI : MonoBehaviour {
        [Header("Currency")]
        public Currency currency;

        protected int currencyValue;
        [Header("Prefix/Postfix")]
        public string prefix;
        public string postfix;


        protected virtual void UpdateCurrencyValue(int currencyValue, int deltaValue = 0) {
            this.currencyValue = currencyValue;
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