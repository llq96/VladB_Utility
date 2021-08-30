using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public abstract class QuanityChanger : MonoBehaviour {
        static string[] symbols = { "+", "x", "-", "/" };

        [Header("Number Settings")]
        [SerializeField] QuanityChangerType type;
        [SerializeField] int number = 2;


        public virtual void Init() {}

        public virtual int GetCalculatedValue(int value = 1, int minValue = 1, int maxValue = int.MaxValue) {
            int result = 0;
            switch (type) {
                case QuanityChangerType.Plus:
                    result = value + number;
                    break;
                case QuanityChangerType.Multiply:
                    result = value * number;
                    break;
                case QuanityChangerType.Minus:
                    result = value - number;
                    break;
                case QuanityChangerType.Divide:
                    result = Mathf.RoundToInt(value / (float)number);
                    break;
                default:
                    break;
            }

            result = Mathf.Clamp(result, minValue, maxValue);
            return result;
        }

        public virtual bool IsCanChangeQuanity(INumbered iNumbered) {
            return iNumbered.lastQuanityChanger != this;
        }

        public abstract void ChangeQuality(INumbered iNumbered);

        public virtual string GetText() => $"{symbols[(int)type]}{number}";
    }



    public enum QuanityChangerType {
        Plus,
        Multiply,
        Minus,
        Divide
    }


    public interface INumbered {
        public int count { get; set; }
        public QuanityChanger lastQuanityChanger { get; set; }
    }
}
