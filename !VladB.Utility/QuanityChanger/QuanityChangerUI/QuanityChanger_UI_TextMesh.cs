using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VladB.Utility {
    public class QuanityChanger_UI_TextMesh : QuanityChanger_UI {
        [Header("UI")]
        public TextMeshPro text;


        private void OnEnable() {
            UpdateUI();
        }

        public override void UpdateUI() {
            if (quanityChanger) {
                text.text = quanityChanger.GetText();
            }
        }
    }
}