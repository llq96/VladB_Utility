using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public abstract class LockedItemUI : MonoBehaviour, ILockedItemUI {
        protected LockedItem lockedItem;

        [Header("Base Settings")]
        [SerializeField] protected bool isAutoDestory;

        [Header("Base UI")]
        [SerializeField] protected GameObject uiContainer;

        public virtual void Init(LockedItem lockedItem) {
            this.lockedItem = lockedItem;
            UpdateUI();
        }
        public virtual void UpdateUI() {
            UpdateUI(lockedItem ? lockedItem.liso : null);
        }

        public abstract void UpdateUI(Liso liso);

        protected virtual void SetActiveUI(bool isActive) {
            if(uiContainer) {
                uiContainer.SetActive(isActive);
            }
        }

        public abstract void ClickOnLockUI();


    }
}
