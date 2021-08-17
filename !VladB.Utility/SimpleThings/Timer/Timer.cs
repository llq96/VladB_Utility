using System;
using UnityEngine;

namespace VladB.Utility {
    public class Timer : MonoBehaviour {
        [Header("Settings")]
        public TimeType timeType;
        public bool isActivateOnStart;
        public bool isReactivateTimer;
        public float maxTimeValue;
        public ViewType viewType;

        public virtual bool isTimerActive { get; private set; }
        public virtual float currentTime { get; private set; }

        public event Action OnEndTime; //public delegate void EndTime();


        #region Public Functions
        public virtual void TimerSetActive(bool _isActive, bool _isReset = false) {
            isTimerActive = _isActive;
            if(_isReset) {
                currentTime = maxTimeValue;
            }
        }

        public virtual string GetTimeString(ViewType _viewType) {
            switch(_viewType) {
                case ViewType.JustInt: return ((int)currentTime).ToString();
                case ViewType.JustFloat: return currentTime.ToString();
                default: return "";
            }
        }
        #endregion

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
            JustInt , JustFloat //TODO Add Type 00:00
        }
    }
}
