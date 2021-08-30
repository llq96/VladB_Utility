using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "NewÑurrencySO", menuName = "VladB/NewÑurrencySO", order = 1)]
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

        public delegate void CurrencyChangeEvent(int currencyValue, int deltaValue = 0);
        public event CurrencyChangeEvent OnCurrencyChange;


        public bool IsCanBuy(int price) => currencyValue >= price;

        public void ChangeValueWithDelta(int deltaValue) {
            SetValue(currencyValue + deltaValue);
        }

        public void SetValue(int newCurrencyValue) {
            int deltaValue = newCurrencyValue - currencyValue;
            currencyValue = newCurrencyValue;
            SaveCurrency();
            OnCurrencyChange?.Invoke(currencyValue, deltaValue);
        }


        void SaveCurrency() {
            PlayerPrefs.SetInt("shopcurrency", currencyValue);
        }
    }




#if UNITY_EDITOR
    [CustomEditor(typeof(Currency)), CanEditMultipleObjects]
    public class CurrencyEditor : Editor {
        Currency script => (Currency)target;


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