using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class GrabbedItem : MonoBehaviour {
        [Header("Settings")]
        public InitType initType = InitType.Awake;
        public float distanceToGrab = 0.5f;

        [Header("Rotation")]
        public bool isNeedRotate = true;
        public Vector3 rotationAxis = Vector3.up;
        public float rotateSpeed;

        public bool isGrabbed { get; protected set; }

        public delegate void ItemWasGrubbed_Event(GrabbedItem _item);
        public event ItemWasGrubbed_Event OnItemWasGrubbed;

        public List<IItemGrabbingAction> grabbingActions = new List<IItemGrabbingAction>();

        #region Init
        public virtual void Init() {
            GrabbingController.AddItem(this);
            grabbingActions = GetComponentsInChildren<IItemGrabbingAction>().ToList();
        }
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

        public virtual void ItemWasGrubbedByReciever(ItemReciever _reciever) {
            isGrabbed = true;
            //Debug.Log(grabbingActions.Count);
            grabbingActions.Act(ga => ga.TryDo(x => x.DoAction()));
            OnItemWasGrubbed?.Invoke(this);
        }

        public void UpdateRotate() {
            transform.Rotate(rotationAxis * rotateSpeed * Time.deltaTime);
        }

    }
}
