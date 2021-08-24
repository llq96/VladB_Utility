using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
namespace VladB.Utility {
    public class CurrencyUI_uGUI : CurrencyUI_Smooth {
        [Header("UI:")]
        public TextMeshProUGUI tmp;

        protected override void OnEnableFunc() {
            if(tmp == null) {
                tmp = GetComponent<TextMeshProUGUI>();
            }
            base.OnEnableFunc();
        }
        protected override void UpdateCurrencyUI() {
            base.UpdateCurrencyUI();
            if(tmp) {
                tmp.text = $"{(int)smoothCurrencyValue}";
            }
        }

    }
}