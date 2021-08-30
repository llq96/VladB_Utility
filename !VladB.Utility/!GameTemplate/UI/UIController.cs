using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class UIController : MonoBehaviour, IController {
        MainController mainController;

        protected List<IUIWindow> windowsList;

        #region IController Realization
        public virtual void Init(IMainController iMainController) {
            this.mainController = iMainController as MainController;
            windowsList = GetComponentsInChildren<IUIWindow>(true).ToList();
            windowsList.Act(m => m.Close());
        }

        public virtual void GameStateChanged(GameStateEnum _state) { }

        public virtual void LevelLoaded(Level _level) { }
        #endregion


        #region Open/Close Window
        public virtual void OpenWindow<T>(bool isCloseOther = true) where T : IUIWindow {
            windowsList = windowsList.Where(x => x != null).ToList();

            if(isCloseOther) {
                windowsList.Where(x => (x.isOpened && !(x is T))).Act(item => item.Close());
            }

            windowsList.Where(x => (x is T)).Act(item => item.Open());
        }

        public virtual void CloseWindow<T>() where T : IUIWindow {
            windowsList.Where(x => x is T).Act(x => x.Close());
        }
        #endregion


    }


    public interface IUIWindow {
        bool isOpened { get; set; }
        void Open();
        void Close();
    }
}
