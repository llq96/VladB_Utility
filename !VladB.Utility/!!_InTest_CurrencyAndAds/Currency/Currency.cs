using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "New—urrencySO", menuName = "VladB/New—urrencySO", order = 1)]
    public class Currency : ScriptableObject {
        [HideInInspector] public string ppName = "";

        public int currencyValue {
            get {
                if(ppName.IsNullOrEmpty()) {
                    ppName = Extensions.GetRandomPP();
                }
                return PlayerPrefs.GetInt(ppName, 0);
            }
            private set {
                if(ppName.IsNullOrEmpty()) {
                    ppName = Extensions.GetRandomPP();
                }
                PlayerPrefs.SetInt(ppName, value);
            }
        }

        public Action<int, int> OnCurrencyChange;

        public bool IsCanBuy(int price) => currencyValue >= price;

        public void ChangeValueWithDelta(int deltaValue) {
            SetValue(currencyValue + deltaValue);
        }

        public void SetValue(int newCurrencyValue) {
            int deltaValue = newCurrencyValue - currencyValue;
            currencyValue = newCurrencyValue;
            OnCurrencyChange?.Invoke(currencyValue, deltaValue);
        }

    }




#if UNITY_EDITOR
    [CustomEditor(typeof(Currency)), CanEditMultipleObjects]
    public class CurrencyEditor : Editor {
        Currency script => target as Currency;


        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if(script.ppName.IsNullOrEmpty()) {
                script.ppName = Extensions.GetRandomPP();
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }

    }
#endif

}