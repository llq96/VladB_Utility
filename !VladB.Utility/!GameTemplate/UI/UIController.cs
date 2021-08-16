using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class UIController : MonoBehaviour, IController {
        MainController mainController;

        protected List<IUIWindow> windowsList;

        #region Open/Close Window
        public virtual void OpenWindow<T>(bool _isCloseOther = true) where T : IUIWindow {
            foreach (IUIWindow _window in windowsList) {
                if (_isCloseOther) {
                    if (_window.isOpened == true) {//TODO «акрывает и нужное окно тоже, выебнутьс€ с linq
                        _window.Close();
                    }
                }
                if (_window is T) {
                    _window.Open();
                }
            }

        }

        public virtual void CloseWindow<T>() where T : IUIWindow {
            windowsList.Where(x => x is T).ToList().ForEach(x => x.Close());
        }
        #endregion

        #region IController Realization

        public virtual void Init(IMainController _mainController) {
            mainController = _mainController as MainController;
            windowsList = GetComponentsInChildren<IUIWindow>(true).ToList();
            windowsList.ForEach(m => m.Close());
        }

        public virtual void GameStateChanged(GameStateEnum _state) { }

        public virtual void LevelLoaded(Level _level) { }

        #endregion
    }


    public interface IUIWindow {
        bool isOpened { get; set; }
        void Open();
        void Close();
    }
}
