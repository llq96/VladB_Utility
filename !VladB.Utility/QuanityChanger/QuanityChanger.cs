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

        public virtual int GetCalculatedValue(int _value = 1, int _minValue = 1, int _maxValue = int.MaxValue) {
            int result = 0;
            switch (type) {
                case QuanityChangerType.Plus:
                    result = _value + number;
                    break;
                case QuanityChangerType.Multiply:
                    result = _value * number;
                    break;
                case QuanityChangerType.Minus:
                    result = _value - number;
                    break;
                case QuanityChangerType.Divide:
                    result = Mathf.RoundToInt(_value / (float)number);
                    break;
                default:
                    break;
            }

            result = Mathf.Clamp(result, _minValue, _maxValue);
            return result;
        }

        public virtual bool IsCanChangeQuanity(INumbered _numbered) {
            return _numbered.lastQuanityChanger != this;
        }

        public abstract void ChangeQuality(INumbered _numbered);

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
