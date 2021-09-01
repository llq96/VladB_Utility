using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    public class LockedItem_UI_ByAds : LockedItemUI {
        Liso_ByAds liso;

        [Header ("UI")]
        public string currentCountAdsLabelUI;

        public override void UpdateUI(Liso liso) {
            this.liso = liso as Liso_ByAds;

            if(this.liso == null) {
                if(isAutoDestory) {
                    Destroy(uiContainer);
                    Destroy(this);
                } else {
                    base.SetActiveUI(false);
                }
                return;
            }

            SetActiveUI(true);
            UpdateCountAdsLabel();
        }

        public override void ClickOnLockUI() {
            if(liso) {
                RewardedVideoHelper.SetLockItem(this);
                RewardedVideoHelper.SimpleLockDialogYes();
            }
        }

        public void RewardedVideoWasShown() {
            liso.currentCountAds--;
            if(liso.currentCountAds >= 1) {
                UpdateCountAdsLabel();
                AdsManager.OnRewardedVideoShown -= RewardedVideoWasShown;
                return;
            }
        }

        private void UpdateCountAdsLabel() {
            currentCountAdsLabelUI = liso.currentCountAds.ToString();//TODO
        }
    }
}
