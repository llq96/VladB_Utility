using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public abstract class QuanityChanger_UI : MonoBehaviour {
        //TODO Динамическое изменение текста если изменился QuanityChanger
        public QuanityChanger quanityChanger;


        public abstract void UpdateUI();
    }
}
