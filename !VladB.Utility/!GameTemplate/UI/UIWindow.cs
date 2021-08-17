using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.GameTemplate {
    public class UIWindow : MonoBehaviour, IUIWindow {
        public virtual bool isOpened { get; set; }


        public virtual void Open() {
            gameObject.SetActive(true);
            isOpened = true;
        }

        public virtual void Close() {
            gameObject.SetActive(false);
            isOpened = false;
        }


    }
}