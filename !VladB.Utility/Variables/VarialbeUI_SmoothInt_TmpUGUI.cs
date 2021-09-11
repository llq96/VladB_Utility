using UnityEngine;
using TMPro;

namespace VladB.Utility {
    public class VarialbeUI_SmoothInt_TmpUGUI : VariableUI_SmoothNumber<int> {
        [Header("UI:")]
        public TextMeshProUGUI tmp;

        protected override void OnEnableFunc() {
            if(tmp == null) {
                tmp = GetComponent<TextMeshProUGUI>();
            }
            base.OnEnableFunc();
        }
        public override void UpdateVariableUI() {
            base.UpdateVariableUI();
            if(tmp) {
                tmp.text = $"{prefix}{(int)smoothVariableValue}{postfix}";
            }
        }

    }
}