using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class AudioManager : MonoBehaviour {
        static Transform __folder;
        static Transform folder { //TODO Не проверялось!
            get {
                if(__folder == null) {
                    GameObject go = GameObject.Find("AudioManagerFolder");
                    if(go == null) {
                        go = new GameObject("AudioManagerFolder");
                    }
                    __folder = go.transform;
                }
                return __folder;
            }
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

                tempObj = Instantiate(tempPrefab, folder);
                tempSource = tempObj.GetComponent<AudioSource>();
                dict.Add(soundPath, tempSource);
                return tempSource;
            }
        }

    }
}