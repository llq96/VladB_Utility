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

        public virtual void Init(LockedItem _lockedItem) {
            lockedItem = _lockedItem;
            UpdateUI();
        }
        public virtual void UpdateUI() {
            UpdateUI(lockedItem ? lockedItem.liso : null);
        }

        public abstract void UpdateUI(Liso _liso);

        protected virtual void SetActiveUI(bool _isActive) {
            if(uiContainer) {
                uiContainer.SetActive(_isActive);
            }
        }

        public abstract void ClickOnLockUI();


    }
}
