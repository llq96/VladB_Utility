using System;

namespace VladB.Utility {
    public static class RewardedVideoHelper {
        //public static RewardedVideoHelper Instance;

        public static LockedItem_UI_ByAds currentLockItem;

        //public delegate void RVLoadedEvent();
        //public static event RVLoadedEvent OnRewardedVideoControllerLoad;

        //public delegate void TryShowRewardedADSWhenIsNowLoaded();
        //public static event TryShowRewardedADSWhenIsNowLoaded OnTryShowRewardedADSWhenIsNowLoaded;

        //public delegate void RewardDialogYesEvent();
        //public static event RewardDialogYesEvent OnRewardDialogYes;

        //public static Action OnRewardedVideoControllerLoad;
        public static Action OnTryShowRewardedADSWhenIsNotAvailable;
        public static Action OnRewardDialogYes;


        public static void SetLockItem(LockedItem_UI_ByAds item) {
            ClearLockItem();

            currentLockItem = item;

            AdsManager.OnRewardedVideoShown += currentLockItem.RewardedVideoWasShown;
        }

        public static void ClearLockItem() {
            if(currentLockItem != null) {
                AdsManager.OnRewardedVideoShown -= currentLockItem.RewardedVideoWasShown;
                currentLockItem = null;
            }
        }


        public static void ShowVideoBanner() {
            if(!AdsManager.IsRewardedAvailable()) {
                OnTryShowRewardedADSWhenIsNotAvailable?.Invoke();
            }
            AdsManager.ShowRewarded();
        }


        public static void SimpleLockDialogYes() {
            OnRewardDialogYes?.Invoke();
            //ClearTimeBasedItem();
            ShowVideoBanner();
        }

    }

}






//public void SetTimeBasedItem(TimeBasedItem item) {
//    if (currentTimeBasedItem != null) {
//        AdsManager.OnRewardedVideoShown -= currentTimeBasedItem.OnVideoShown;
//    }

//    currentTimeBasedItem = item;
//    currentRewardType = TypeOfReward.Video; //TODO ?

//    AdsManager.OnRewardedVideoShown += currentTimeBasedItem.OnVideoShown;
//}

//void ClearTimeBasedItem() {
//    if (currentTimeBasedItem != null) {
//        AdsManager.OnRewardedVideoShown -= currentTimeBasedItem.OnVideoShown;
//        currentTimeBasedItem = null;
//    }
//}





//#region Play Sound
//public void PlaySound(AudioClip clip) {
//    aSource.PlayOneShot(clip);
//}

//public void PlayDeny() {
//    PlaySound(denySound);
//}

//public void PlaySuccess() {
//    PlaySound(successSound);
//    AdsManager.OnRewardedVideoShown -= PlaySuccess;
//}

//public void PlayAddRewardSound() {
//    PlaySound(addRewardSound);
//    AdsManager.OnRewardedVideoShown -= PlayAddRewardSound;
//}
//#endregion


//#region TimeToString

//static int secs;
//static int mins;
//static int hours;
//static string result = "";


//public static string SecondsToTime(float seconds) {
//    if (seconds > 3600) {
//        hours = (int)seconds / 3600;
//        mins = (int)seconds / 60 - hours * 60;

//        result = String.Format("{0:0}h:{1:00}m", hours, mins);
//    } else {
//        secs = (int)seconds % 60;
//        mins = (int)seconds / 60;

//        result = String.Format("{0:0}m:{1:00}s", mins, secs);
//    }

//    return result;
//}

//#endregion


//public void RewardDialogYes(bool useSoundOnVideoShown = true) {
//    LockItem cachedItem = currentLockItem;
//    ClearLockItem();

//    SetTimeBasedItem(currentTimeBasedItem);

//    if (currentRewardType == TypeOfReward.Video) {
//        if (useSoundOnVideoShown) {
//            AdsManager.OnRewardedVideoShown += PlayAddRewardSound;
//        }

//        OnRewardDialogYes?.Invoke();

//        ShowVideoBanner();
//    } else if (currentRewardType == TypeOfReward.Free) {
//        currentTimeBasedItem.OnVideoShown();

//        if (useSoundOnVideoShown) {
//            PlayAddRewardSound();
//        }

//        currentLockItem = cachedItem;
//    }
//}

//}


//public enum TypeOfReward {
//    Free,
//    Video,
//}

//public enum TimeFormat {
//    MinutesSeconds,
//    HoursMinutesSeconds
//}
