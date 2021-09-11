using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class SimplePool : Pool { //Что ещё часто используемого добавить?
        [Header("Settings")]
        public int countSpawnOnStart = 0;

        void Start() {
            if (countSpawnOnStart > 0) {
                InstantiateObjects(countSpawnOnStart);
                SetActiveAllObjects(false, true);
            }
        }
    }
}