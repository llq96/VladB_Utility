using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "RandomNickNamesSO", menuName = "VladB/RandomNickNames", order = 2)]
    public class RandomNickNamesSO : ScriptableObject {
        [Header ("All Lists")]
        public List<NickNamesList> nickNamesLists = new List<NickNamesList>();

        public string GetRandomNickName() {
            return nickNamesLists.GetRandom().nickNames.GetRandom();
        }

        [System.Serializable]
        public class NickNamesList {
            public string listName;
            public List<string> nickNames;
        }
    }
}