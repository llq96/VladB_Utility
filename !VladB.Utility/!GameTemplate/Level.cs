using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.GameTemplate {

    public class Level : MonoBehaviour {
        MainController mainController;


        public virtual void Init(IMainController _iController) {
            mainController = _iController as MainController;
        }

        #region Awake/Start/Update Functions
        void Awake() {
            AwakeFunc();
        }
        protected virtual void AwakeFunc() { }

        void Start() {
            StartFunc();
        }
        protected virtual void StartFunc() { }

        void Update() {
            UpdateFunc();
        }
        protected virtual void UpdateFunc() { }

        void LateUpdate() {
            LateUpdateFunc();
        }
        protected virtual void LateUpdateFunc() { }
        #endregion
    }
}