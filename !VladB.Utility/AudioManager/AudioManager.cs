using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class AudioManager : MonoBehaviour {
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

        void Awake() {
            instance = this;
        }

        public static AudioSource GetAudioSource(string _soundPath) {
            if (dict.TryGetValue(_soundPath, out tempSource)) {
                return tempSource;
            } else {
                tempPrefab = ResourcesLoader.LoadGameObject(_soundPath);
                if (tempPrefab == null) {
                    return null;
                }

                tempObj = Instantiate(tempPrefab, instance.transform);
                tempSource = tempObj.GetComponent<AudioSource>();
                dict.Add(_soundPath, tempSource);
                return tempSource;
            }
        }

    }
}