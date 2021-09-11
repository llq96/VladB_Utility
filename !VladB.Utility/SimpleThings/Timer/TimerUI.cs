using UnityEngine;

namespace VladB.Utility {
    public class TimerUI : MonoBehaviour {
        [Header("Timer")]
        public Timer timer;

        [Header("UI")]
        [SerializeField] protected GameObject timerUI;

        [Header("View Settings")]
        [SerializeField] protected float valueToShowTimer = 999f;
        [SerializeField] protected Timer.ViewType viewType;


        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() {
            if(timerUI == null) {
                return;
            }
            if(timer == null) {
                SetActiveTimerUI(false);
                return;
            }

            if(timer.currentTime <= valueToShowTimer) {
                timerUI.SetActive(true);
                SetText(timer.GetTimeString(viewType));
            } else {
                timerUI.SetActive(false);
            }
        }

        protected virtual void SetActiveTimerUI(bool isActive) {
            if((timerUI) && (timerUI.activeSelf != isActive)) {
                timerUI.SetActive(isActive);
            }
        }

        protected virtual void SetText(string text) {
            //Debug.Log(_text);
        }
    }
}
