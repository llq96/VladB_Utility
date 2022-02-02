using System.Collections.Generic;
using UnityEngine;

namespace VladB.GameTemplate {
    public class GrabbingController : MonoBehaviour, IController{
        protected MainController mainController;

        public static List<ItemReciever> recievers = new List<ItemReciever>();
        public static List<GrabbedItem> items = new List<GrabbedItem>();

        #region IController Realization
        public virtual void Init(MainController mainController) {
            this.mainController = mainController;

            //recievers = new List<ItemReciever>();
            //items = new List<GrabbedItem>();
        }
        public void GameStateChanged(GameStateEnum state, params object[] parameters) { }
        #endregion

        #region Adding Reciever Or Item
        public static void AddReciever(ItemReciever _reciever) {
            if(recievers.Contains(_reciever)) {
                return;
            }
            recievers.Add(_reciever);
        }

        public static void AddItem(GrabbedItem _item) {
            if(items.Contains(_item)) {
                return;
            }
            items.Add(_item);
        }
        #endregion

        Vector3 tempVec3;
        public void Update() {
            items.RemoveAll(x => ((x == null) || (x.gameObject == null)));
            recievers.RemoveAll(x => ((x == null) || (x.gameObject == null)));

            foreach(ItemReciever reciever in recievers) {
                foreach(GrabbedItem item in items) {
                    if(item.isGrabbed) {
                        continue;
                    }
                    if(!item.gameObject.activeSelf) {
                        continue;
                    }

                    tempVec3 = item.transform.position - reciever.transform.position;
                    tempVec3.y = 0;
                    //TODO Ignore Some Asis
                    if(tempVec3.magnitude < (reciever.distanceToGrab + item.distanceToGrab)) {
                        if(reciever.IsCanGrabItem(item)) {
                            reciever.GrabItem(item);
                        }
                        //TODO Проверка на взятие и Remove Item в конце метода ?
                    }
                }
            }

            foreach(GrabbedItem item in items) {
                if(item.isNeedRotate) {
                    item.UpdateRotate();
                }
            }
        }
    }
}