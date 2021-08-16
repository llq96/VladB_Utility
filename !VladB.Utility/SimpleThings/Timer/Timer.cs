using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VladB.Utility {
    public class Timer : MonoBehaviour {
        [Header("Settings")]
        public Timer_TimeType timeType;
        public bool isActivateOnStart = false;
        public bool isReactivateTimer;
        public float maxTimeValue;


        [Header("Info:")]
        [HideInInspector] public bool isTimerActive;
        [HideInInspector] public float curValue;

        public delegate void EndTime();
        public event EndTime OnEndTime;

        //[Header("UI")]
        //public float valueToShowTimerUI = 999f;
        //public GameObject timerUI;
        //public UILabel timerLabelUI;


        public void TimerSetActive(bool _isActive, bool _isReset = false) {
            isTimerActive = _isActive;
            if (_isReset) {
                TimerReset();
            }
            //if (!_isActive) {
            //    SetActiveTimerUI(false);
            //}
        }

        //public void SetActiveTimerUI(bool _isActive) {
        //    if (timerUI) {
        //        timerUI.SetActive(_isActive);
        //    }
        //}

        public void TimerReset() {
            curValue = maxTimeValue;
        }

        void Start() {
            if (isActivateOnStart) {
                TimerSetActive(true, true);
            }
        }

        void Update() {
            if (!isTimerActive) {
                return;
            }

            if (timeType == Timer_TimeType.Scaled) {
                curValue -= Time.deltaTime;
            } else if (timeType == Timer_TimeType.UnScaled) {
                curValue -= Time.unscaledDeltaTime;
            }

            curValue = Mathf.Clamp(curValue, 0f, maxTimeValue);

            if (curValue <= 0f) {
                OnEndTime?.Invoke();
                if (isReactivateTimer) {
                    TimerSetActive(true, true);
                } else {
                    TimerSetActive(false, false);
                }
                //onEndTime.Invoke();

            }


            //if (timerUI) {
            //    if (curValue <= valueToShowTimerUI) {
            //        if (!timerUI.activeSelf) {
            //            timerUI.SetActive(true);
            //        }


            //        if (timerLabelUI) {
            //            timerLabelUI.text = (int)curValue + "";
            //        }
            //    } else {
            //        if (timerUI.activeSelf) {
            //            timerUI.SetActive(false);
            //        }
            //    }
            //}
        }

        public enum Timer_TimeType {
            Scaled, UnScaled
        }
    }
}
