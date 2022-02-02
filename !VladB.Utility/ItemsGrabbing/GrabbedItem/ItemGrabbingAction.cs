using UnityEngine;

namespace VladB.GameTemplate {
    public class ItemGrabbingAction : MonoBehaviour, IItemGrabbingAction {
        public virtual void DoAction() { }
    }

    public interface IItemGrabbingAction {
        void DoAction();
    }
}