using UnityEngine;
using UnityEngine.UI;

namespace VladB.Utility {
    public class TimerUI_uGUI : TimerUI {
        [Header("Label")]
        [SerializeField] Text timerLabel;


        protected override void SetText(string text) {
            if((timerLabel) && (timerLabel.text != text)) {
                timerLabel.text = text;
            }
        }
    }
}
