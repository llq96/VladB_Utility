using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class LockedItem_UI_ByCurrency : LockedItemUI {
        Liso_ByCurrency liso;

        [Header ("UI")]
        public string priceLabelValue;

        public override void UpdateUI(Liso liso) {
            this.liso = liso as Liso_ByCurrency;

            if(this.liso == null) {
                if(isAutoDestory) {
                    Destroy(uiContainer);
                    Destroy(this);
                } else {
                    SetActiveUI(false);
                }
                return;
            }

            SetActiveUI(true);
            priceLabelValue = this.liso.price.ToString();//TODO
        }

        public override void ClickOnLockUI() {
            if(liso) {
                if(liso.currency) {
                    if(liso.currency.IsCanBuy(liso.price)) {
                        liso.currency.ChangeValueWithDelta(-liso.price);
                        lockedItem.Unlock();
                    }
                }
            }
        }


    }
}
