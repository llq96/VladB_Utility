using System.Collections;
using UnityEngine;

namespace VladB.GameTemplate {
    public class GameStateSwitcher : MonoBehaviour, IController {
        protected MainController mainController;


        #region IController Realization
        public virtual void Init(MainController mainController) {
            this.mainController = mainController;
        }
        public virtual void GameStateChanged(GameStateEnum state, params object[] parameters) {}
        #endregion

        #region SetGameState With Delay
        public void SetGameState(GameStateEnum state, float delay, bool isRealTime = false, params object[] parameters) {
            StartCoroutine(SetGameState_Cor(state, delay, isRealTime, parameters));
        }

        public IEnumerator SetGameState_Cor(GameStateEnum state, float delay, bool isRealTime = false, params object[] parameters) {
            yield return isRealTime ? new WaitForSecondsRealtime(delay) : (object)new WaitForSeconds(delay);
            mainController.SetGameState(state, parameters);
        }
        #endregion
    }
}