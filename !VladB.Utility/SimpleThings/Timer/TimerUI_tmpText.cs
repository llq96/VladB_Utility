using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.Utility;
using TMPro;

public class TimerUI_tmpText : TimerUI {
    [Header("Label")]
    [SerializeField] TextMeshProUGUI timerLabel;


    protected override void SetText(string text) {
        if((timerLabel) && (timerLabel.text != text)) {
            timerLabel.text = text;
        }
    }
}
