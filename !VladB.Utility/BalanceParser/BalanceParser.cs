using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.GameTemplate;

namespace VladB.Utility {
    public class BalanceParser : MonoBehaviour {
        public static BalanceParser Instance;

        [Header("Enable Loading From Google Spreadsheets")]
        public bool isLoadingEnable;

        [Header("Settings")]
        public string url;
        public string spreadsheetId;
        public Range[] ranges;

        [Header("Info : Response")]
        public string response;
        public int responceLength;

        [Header("Info : IsParsed")]
        public bool isParsed;

        //[Header ("Info : Parameters")]
        //public List<BP_Parameter> parameters;

        public delegate void EndParse(bool isSuccess);
        public static event EndParse OnEndParse;


        [Header("All Strings")]
        public AllArrays all;

        [Header("Load Scene After Parse")]
        public LoadSceneOnStart sceneLoader;

        void Awake() {
            if(Instance) {
                Destroy(gameObject);
                return;
            } else {
                Instance = this;
                DontDestroyOnLoad(gameObject);


                if(isLoadingEnable) {
                    Connect();
                } else {
                    isParsed = true;
                    LoadScene();
                }
            }
        }

        public void Connect() {
            isParsed = false;
            StartCoroutine(Connect_Cor());
        }

        string GetString_AllRanges() {
            string result = "";
            for(int i = 0; i < ranges.Length; i++) {
                result += $"{ranges[i].list}!{ranges[i].range}";
                if(i != ranges.Length - 1) {
                    result += "|";
                }
            }
            return result;
        }

        string GetString_AllNames() {
            string result = "";
            for(int i = 0; i < ranges.Length; i++) {
                result += ranges[i].name;
                if(i != ranges.Length - 1) {
                    result += "|";
                }
            }
            return result;
        }


        IEnumerator Connect_Cor() {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246");

            WWWForm form = new WWWForm();
            form.AddField("_spreadsheetId", spreadsheetId);
            form.AddField("_ranges", GetString_AllRanges());
            form.AddField("_names", GetString_AllNames());
            WWW www = new WWW(url, form.data, headers);
            yield return www;

            response = www.text;
            responceLength = response.Length;

            if(responceLength != 0) {
                ParseResponse();
            } else {
                Debug.LogError("Wrong Length of Response");
                OnEndParse?.Invoke(false);
                LoadScene();
            }

            yield break;
        }


        void ParseResponse() {
            try {
                all = JsonUtility.FromJson<AllArrays>(response);
            } catch {
                Debug.LogError("Parse Failed");

                OnEndParse?.Invoke(false);
                LoadScene();
                return;
            }

            Debug.Log("Parse Complete");
            isParsed = true;
            OnEndParse?.Invoke(true);
            LoadScene();
        }

        void LoadScene() {
            sceneLoader.LoadScene();
        }

        public string[] GetArray(string _name) {
            for(int i = 0; i < all.arrays.Length; i++) {
                if(all.arrays[i].name == _name) {
                    return all.arrays[i].strings;
                }
            }
            return null;
        }


    }




    [System.Serializable]
    public class StringsArray {
        public string name;
        public string[] strings;
    }

    [System.Serializable]
    public class AllArrays {
        public StringsArray[] arrays;
    }

    [System.Serializable]
    public class Range {
        public string name;
        public string list;
        public string range;
    }
}

























//void ParseResponse() {
//    try {
//        Debug.Log("Parse : " + response);

//        parameters = new List<BP_Parameter>();

//        string[] paramsStrings = response.Split('|');

//        string[] key_and_value;

//        for (int i = 0; i < paramsStrings.Length - 1; i++) {
//            //Debug.Log(parameters[i]);
//            key_and_value = paramsStrings[i].Split(' ');
//            Add(key_and_value[0], key_and_value[1]);
//        }

//    } catch {
//        Debug.LogError("Parse Failed");

//        if (OnEndParse != null) {
//            OnEndParse(false);
//        }
//        return;
//    }

//    Debug.Log("Parse Complete");
//    isParsed = true;
//    if (OnEndParse != null) {
//        OnEndParse(true);
//    }

//}

//void Add(string _key , string _value) {
//    BP_Parameter newParam = new BP_Parameter();
//    newParam.key = _key;
//    newParam.value = _value;
//    parameters.Add(newParam);
//}

//public int GetInt(string _key, int _defaultValue = -1) {
//    foreach (BP_Parameter par in parameters) {
//        if(par.key == _key) {
//            return par.GetInt(_defaultValue);
//        }
//    }
//    return _defaultValue;
//}

//public float GetFloat(string _key, float _defaultValue = -1) {
//    foreach (BP_Parameter par in parameters) {
//        if (par.key == _key) {
//            return par.GetFloat(_defaultValue);
//        }
//    }
//    Debug.LogError($"Cant Find Key : {_key}");
//    return _defaultValue;
//}

//public string GetString(string _key, string _defaultValue = null) {
//    foreach (BP_Parameter par in parameters) {
//        if (par.key == _key) {
//            return par.value;
//        }
//    }
//    return _defaultValue;
//}



//[System.Serializable]
//public class BP_Parameter{
//    public string key;
//    public string value;

//    public int GetInt(int _defaultValue = -1) {
//        int result = _defaultValue;
//        int.TryParse(value, out result);
//        return result;
//    }

//    public float GetFloat(float _defaultValue = -1f) {
//        float result = _defaultValue;
//        float.TryParse(value, System.Globalization.NumberStyles.Float  , System.Globalization.CultureInfo.InvariantCulture,  out result);
//        //if(result == _defaultValue) {
//        //    Debug.LogError("Cant Parse To Float : " + value);
//        //}
//        //Debug.Log(result);
//        return result;
//    }

//}

