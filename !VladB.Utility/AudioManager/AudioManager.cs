using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class AudioManager : MonoBehaviour {
        //TODO instance используется только для того, чтобы иметь папку на сцене в которую кидаются объекты,
        //возможно стоит подумать как избавиться от инстанса, но сохранить функционал
        static AudioManager __instance;
        public static AudioManager instance {
            get {
                if(__instance == null) {
                    GameObject go = new GameObject("AudioManager");
                    //go.AddComponent<AudioListener>(); //TODO Делать проверку если ли в открытых сценах Listener ?
                    __instance = go.AddComponent<AudioManager>();
                    //DontDestroyOnLoad(go);
                }
                return __instance;

            }
            set => __instance = value;
        }

        static Dictionary<string, AudioSource> dict = new Dictionary<string, AudioSource>();

        static AudioSource tempSource;
        static GameObject tempObj;
        static GameObject tempPrefab;

        public static AudioSource GetAudioSource(string soundPath) {
            if (dict.TryGetValue(soundPath, out tempSource)) {
                return tempSource;
            } else {
                tempPrefab = ResourcesLoader.LoadGameObject(soundPath);
                if (tempPrefab == null) {
                    return null;
                }

                tempObj = Instantiate(tempPrefab, instance.transform);
                tempSource = tempObj.GetComponent<AudioSource>();
                dict.Add(soundPath, tempSource);
                return tempSource;
            }
        }

    }
}