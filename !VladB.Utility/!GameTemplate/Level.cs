using UnityEngine;

namespace VladB.GameTemplate {
    public class Level : MonoBehaviour {
        MainController mainController;


        public virtual void Init(MainController mainController) {
            this.mainController = mainController;
        }

        #region Awake/Start/Update/LateUpdate Functions
        void Awake() => AwakeFunc();
        protected virtual void AwakeFunc() { }

        void Start() => StartFunc();
        protected virtual void StartFunc() { }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() { }

        void LateUpdate() => LateUpdateFunc();
        protected virtual void LateUpdateFunc() { }
        #endregion



#if UNITY_EDITOR
        public virtual void LogIfExist<T>() where T : Component {
            if(GetComponentsInChildren<T>().Length >= 1) {
                Debug.Log(gameObject.name);
            }
        }
#endif

    }
}