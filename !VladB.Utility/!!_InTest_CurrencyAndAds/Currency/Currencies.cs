using System.Collections;
using UnityEngine;

namespace VladB.Utility {
    public class Currencies : MonoBehaviour {
        //TODO
        public Currency money;
        public Currency crystals;

        public void Currency_ChangeValueWithDelta(Currency currency, float delay, int deltaValue) {
            StartCoroutine(Currency_ChangeValueWithDelta_Cor(currency , delay, deltaValue));
        }

        IEnumerator Currency_ChangeValueWithDelta_Cor(Currency currency, float delay, int deltaValue) {
            yield return new WaitForSecondsRealtime(delay);
            currency.ChangeValueWithDelta(deltaValue);
        }
    }
}