using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class UIController : MonoBehaviour, IController {
        MainController mainController;

        protected List<IUIWindow> windowsList;


        #region IController Realization
        public virtual void Init(MainController mainController) {
            this.mainController = mainController;
            windowsList = GetComponentsInChildren<IUIWindow>(true).ToList();
            windowsList.Act(w => w.Close());
        }

        public virtual void GameStateChanged(GameStateEnum _state, params object[] parameters) { }
        #endregion

        #region Open/Close Window
        public virtual void OpenWindow<T>(bool isCloseOther = true) where T : IUIWindow {
            windowsList = windowsList.Where(w => w != null).ToList();

            if(isCloseOther) {
                windowsList.Where(w => w.isOpened && !(w is T)).Act(w => w.Close());
            }

            windowsList.Where(w => (w is T)).Act(w => w.Open());
        }

        public virtual void CloseWindow<T>() where T : IUIWindow {
            windowsList.Where(w => w is T).Act(w => w.Close());
        }
        #endregion
    }


    public interface IUIWindow {
        bool isOpened { get; set; }
        void Open();
        void Close();
    }
}
