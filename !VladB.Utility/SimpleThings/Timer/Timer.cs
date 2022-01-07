using System;
using UnityEngine;

namespace VladB.Utility {
    public class Timer : MonoBehaviour {
        [Header("Settings")]
        public TimeType timeType;
        public bool isActivateOnStart;
        public bool isReactivateTimer;
        public float maxTimeValue;

        public virtual bool isTimerActive { get; private set; }
        public virtual float currentTime { get; private set; }

        public event Action OnEndTime;


        #region Public Functions
        public virtual void TimerSetActive(bool isActive, bool isReset = false) {
            isTimerActive = isActive;
            if(isReset) {
                currentTime = maxTimeValue;
            }
        }

        public virtual string GetTimeString(ViewType viewType) {
            return viewType switch {
                ViewType.JustInt => ((int)currentTime).ToString(),
                ViewType.JustFloat => currentTime.ToString(),
                ViewType.Time => GetReadableTime((int)currentTime),
                _ => "",
            };
        }
        #endregion

        //public static string GetReadableTime(int seconds) => $"{seconds / 3600:00}:{seconds / 60 % 60:00}:{seconds % 60:00}";
        public static string GetReadableTime(int seconds) {
            if(seconds >= 3600) {
                return $"{seconds / 3600:00}:{seconds / 60 % 60:00}:{seconds % 60:00}";
            } else {
                return $"{seconds / 60 % 60:00}:{seconds % 60:00}";
            }
            
        }

        #region Start/Update Functions
        void Start() => StartFunc();
        protected virtual void StartFunc() {
            if(isActivateOnStart) {
                TimerSetActive(true, true);
            }
        }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() {
            if(!isTimerActive) {
                return;
            }

            currentTime -= GetDeltaTime();
            currentTime = Mathf.Clamp(currentTime, 0f, maxTimeValue);

            if(currentTime <= 0f) {
                OnEndTime?.Invoke();
                TimerSetActive(isReactivateTimer, isReactivateTimer);
            }
        }
        #endregion

        protected virtual float GetDeltaTime() {
            switch(timeType) {
                case TimeType.Scaled:
                    return Time.deltaTime;
                case TimeType.UnScaled:
                    return Time.unscaledDeltaTime;
                default:
                    return 0;
            }
        }


        public enum TimeType {
            Scaled, UnScaled //TODO Add "InGame" Type ?
        }

        public enum ViewType {
            JustInt, JustFloat , Time //TODO Add Type 00:00
        }
    }
}
