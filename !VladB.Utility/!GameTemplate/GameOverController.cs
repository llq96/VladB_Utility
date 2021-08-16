using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.GameTemplate {
    public class GameOverController : MonoBehaviour, IController {

        public virtual void GameOver(GameOverType _type, float _delay = 2f) {
            if (IsWinType(_type)) {
                StartCoroutine(GameOver_Win(_delay));
            } else {
                StartCoroutine(GameOver_Lose(_delay));
            }
        }

        #region Win/Lose Coroutines
        protected virtual IEnumerator GameOver_Win(float _delay) {
            yield return new WaitForSeconds(_delay);
        }

        protected virtual IEnumerator GameOver_Lose(float _delay) {
            yield return new WaitForSeconds(_delay);
        }
        #endregion

        #region IController Realization
        public virtual void GameStateChanged(GameStateEnum _state) {}

        public virtual void Init(IMainController _mainController) {}

        public virtual void LevelLoaded(Level _level) {}
        #endregion

        #region IsWin/IsLose
        public virtual bool IsWinType(GameOverType _type) {
            return _type == GameOverType.Win;
        }

        public virtual bool IsLoseType(GameOverType _type) {
            return _type == GameOverType.Lose;
        }
        #endregion
    }

    public enum GameOverType {
        None,
        Win,
        Lose
    }
}