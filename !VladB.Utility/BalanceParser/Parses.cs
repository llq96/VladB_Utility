using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using VladB.Utility;
using System.Globalization;
using TMPro;
using System.Linq;


namespace VladB.Utility {
    public class Parses : MonoBehaviour {
        //[Header("Main Controller")]
        //public IG_MainController mc;
        //public IG_AllBalance balance;

        BalanceParser bp;

        public List<Color> colors = new List<Color>();

        private void Start() {
            bp = BalanceParser.Instance;

            if(bp == null) {
                return;
            }

            if(bp.isParsed) {
                ApplyBalance();
            } else {
                BalanceParser.OnEndParse += ApplyBalance;
                return;
            }

            //if ((bp == null) || (!bp.isParsed)) {
            //    return;
            //}

            //ApplyBalance();
        }

        //TODO Можно улучшить
        public bool IsHexColor(string s) => (s.Length == 7 || s.Length == 9) && s.StartsWith("#");

        public Color HexStringToColor(string str) {
            if(str.StartsWith("#")) {
                str = str.Remove(0, 1);
            }
            float r, g, b, a;
            r = g = b = a = 1f;

            if(str.Length >= 6) {
                r = GetFloatFromHex(str, 0);
                g = GetFloatFromHex(str, 2);
                b = GetFloatFromHex(str, 4);
                if(str.Length >= 8) {
                    a = GetFloatFromHex(str, 6);
                }
            }

            return new Color(r, g, b, a);
            //LocalFunction
            float GetFloatFromHex(string s, int from, int count = 2) => int.Parse(s.Substring(from, count), NumberStyles.HexNumber) / 255f;

            //bool isFirstSharp = str.StartsWith("#");
            //float[] floats = new float[]{ 1f, 1f, 1f, 1f };
            //Enumerable.Range(0, Mathf.FloorToInt(str.Length / 2f)).Act(i => {
            //    floats[i] = int.Parse(str.Substring((isFirstSharp ? 1 : 0) + i * 2, 2), NumberStyles.HexNumber) / 255f ;
            //});
            //return new Color(floats[0] , floats[1] , floats[2] , floats[3]);
        }

        public void ApplyBalance(bool isSuccess = true) {
            string[] mainParams_str = bp.GetArray("DataMainParams");

            mainParams_str.Act(s => {
                if(IsHexColor(s)) {
                    colors.Add(HexStringToColor(s));
                }
            });
        }

        //public void BtnConnectAndReload() {
        //    if (BalanceParser.Instance) {
        //        BalanceParser.OnEndParse += EndParse;
        //        BalanceParser.Instance.Connect();
        //    }
        //}

        //void EndParse(bool isSuccess) {
        //    BalanceParser.OnEndParse -= EndParse;
        //    if (gameObject == null) {//На всякий случай
        //        return;
        //    }

        //    PaintC_SceneLoader.Instance.LoadNewLevel("SceneGame", "SceneGame", true);
        //}
    }
}












//public Color HexStringToColor(string s) {

//}