using System;
using UnityEngine;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class ItemReciever : MonoBehaviour {
        [Header("Settings")]
        public InitType initType = InitType.Awake;
        public float distanceToGrab = 0.5f;

        public Action<GrabbedItem> OnRecieverGrabItem;


        #region Init
        public void Init() => GrabbingController.AddReciever(this);
        #endregion

        #region Awake/Start/Update Methods
        void Awake() => AwakeFunc();
        protected virtual void AwakeFunc() {
            if(initType == InitType.Awake) {
                Init();
            }
        }

        void Start() => StartFunc();
        protected virtual void StartFunc() {
            if(initType == InitType.Start) {
                Init();
            }
        }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() { }
        #endregion

        #region GrabItem
        public virtual void GrabItem(GrabbedItem item) {
            OnRecieverGrabItem?.Invoke(item);
            item.ItemWasGrubbedByReciever(this);
        }

        public virtual bool IsCanGrabItem(GrabbedItem item) {
            return true;
        }
        #endregion
    }
}