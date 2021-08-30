using UnityEngine;
using VladB.Utility;

namespace VladB.GameTemplate {
    public class UIWindow : MonoBehaviour, IUIWindow {
        public virtual bool isOpened { get; set; }


        public virtual void Open() {
            //gameObject.TryDo(g => g.SetActive(true));
            if(gameObject) {
                gameObject.SetActive(true);
            }
            isOpened = true;
        }

        public virtual void Close() {
            //gameObject.TryDo(g => g.SetActive(false));
            if(gameObject) {
                gameObject.SetActive(false);
            }
            isOpened = false;
        }
    }
}