using UnityEngine;
using System;


namespace VladB.Utility {

    [System.Serializable]
    public class Currency : VariablePPInt {

        public bool IsCanBuy(int price) => value >= price;
         

    }


}