using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "NewÑurrencySO", menuName = "VladB/NewÑurrencySO", order = 1)]
    public class Currency : ScriptableObject {
        //[Header("Player Prefs")]
        [HideInInspector] public string ppName = "";

        public int currencyValue {
            get {
                if(ppName.IsNullOrEmpty()) {
                    ppName = Liso.GetRandomPP();
                }
                return PlayerPrefs.GetInt(ppName, 0);
            }
            set {
                if(ppName.IsNullOrEmpty()) {
                    ppName = Liso.GetRandomPP();
                }
                PlayerPrefs.SetInt(ppName, value);
            }
        }

        public delegate void CurrencyChangeEvent(int _currencyValue, int delta = 0);
        public event CurrencyChangeEvent OnCurrencyChange;


        public bool IsCanBuy(int price) => currencyValue >= price;

        public void ChangeValueWithDelta(int _deltaValue) {
            SetValue(currencyValue + _deltaValue);
        }

        public void SetValue(int _newCurrencyValue) {
            int _delta = _newCurrencyValue - currencyValue;
            currencyValue = _newCurrencyValue;
            SaveCurrency();
            OnCurrencyChange?.Invoke(currencyValue, _delta);
        }
        //public void AddCurrency_WithDelay(float delay, int value, bool isAddAchievement = true) {
        //    StartCoroutine(AddCurrency_Cor(delay, value, isAddAchievement));
        //}

        //IEnumerator AddCurrency_Cor(float delay, int value, bool isAddAchievement = true) {
        //    yield return new WaitForSecondsRealtime(delay);
        //    AddCurrency(value, isAddAchievement);
        //}

        public void SaveCurrency() {
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
                script.ppName = Liso.GetRandomPP();
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }

    }
#endif

}