using UnityEngine;

namespace VladB.Utility {

    [System.Serializable]
    public class Currency : VariablePPInt {
        public override int value {
            get => base.value;
            set => base.value = Mathf.Clamp(value, 0, int.MaxValue);
        }

        public bool IsCanBuy(int price) => value >= price;

        public bool Buy(int price) {
            if(IsCanBuy(price)) {
                value -= price;
                return true;
            } else {
                return false;
            }
        }
    }
}