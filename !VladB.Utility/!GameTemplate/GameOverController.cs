using System.Collections;
using UnityEngine;

namespace VladB.GameTemplate {
    public class GameOverController : MonoBehaviour, IController {

        #region IController Realization
        public virtual void Init(MainController mainController) { }
        public virtual void GameStateChanged(GameStateEnum state, params object[] parameters) { }
        #endregion

        #region GameOVer
        public virtual void GameOver(GameOverType type, float delay = 2f) {
            if(IsWinType(type)) {
                StartCoroutine(GameOver_Win(delay));
            } else {
                StartCoroutine(GameOver_Lose(delay));
            }
        }
        #endregion

        #region Win/Lose Coroutines
        protected virtual IEnumerator GameOver_Win(float delay) {
            yield return new WaitForSeconds(delay);
        }

        protected virtual IEnumerator GameOver_Lose(float delay) {
            yield return new WaitForSeconds(delay);
        }
        #endregion


        #region IsWin/IsLose
        public virtual bool IsWinType(GameOverType type) => type == GameOverType.Win;
        public virtual bool IsLoseType(GameOverType type) => type == GameOverType.Lose;
        #endregion
    }

    public enum GameOverType {
        None,
        Win,
        Lose
    }
}