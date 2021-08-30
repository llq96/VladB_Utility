using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.GameTemplate {
    public class GameOverController : MonoBehaviour, IController {

        public virtual void GameOver(GameOverType type, float delay = 2f) {
            if (IsWinType(type)) {
                StartCoroutine(GameOver_Win(delay));
            } else {
                StartCoroutine(GameOver_Lose(delay));
            }
        }

        #region Win/Lose Coroutines
        protected virtual IEnumerator GameOver_Win(float delay) {
            yield return new WaitForSeconds(delay);
        }

        protected virtual IEnumerator GameOver_Lose(float delay) {
            yield return new WaitForSeconds(delay);
        }
        #endregion

        #region IController Realization
        public virtual void GameStateChanged(GameStateEnum state) {}

        public virtual void Init(IMainController mainController) {}

        public virtual void LevelLoaded(Level level) {}
        #endregion

        #region IsWin/IsLose
        public virtual bool IsWinType(GameOverType type) {
            return type == GameOverType.Win;
        }

        public virtual bool IsLoseType(GameOverType type) {
            return type == GameOverType.Lose;
        }
        #endregion
    }

    public enum GameOverType {
        None,
        Win,
        Lose
    }
}