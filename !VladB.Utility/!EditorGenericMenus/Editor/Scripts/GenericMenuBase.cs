using UnityEditor;
using UnityEngine;

namespace VladB.Utility {

    [System.Serializable]
    public class GenericMenuBase : ScriptableObject{
        protected static GenericMenu menu;

        public void TryOpen() {
            GenerateGenericMenu();
        }

        public virtual bool GenerateGenericMenu() {
            if (!IsCanGenerateMenu()) {
                Debug.LogError("Can Not Generate Menu");
                return false;
            }

            //Созадние меню
            menu = new GenericMenu();
            menu.allowDuplicateNames = false;

            return true;
        }

        protected virtual bool IsCanGenerateMenu() { return true; }

        public virtual void AddMenuItem(string _menuName, object _object) {
            menu.AddItem(new GUIContent(_menuName, "123"), false, OnClicked, _object);
        }

        protected virtual void ShowGenericMenuAtCursorPosition() {
#if UNITY_2020_1_OR_NEWER
            Vector2 pos=Vector2.zero;
            if (Event.current != null) {
                pos = Event.current.mousePosition;
            }
            menu.DropDown(new Rect(pos.x, pos.y, 0f, 0f));
#else
        Vector2 pos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        EditorWindow focused = EditorWindow.focusedWindow;
        if (focused) {
            pos.x += focused.position.x;
            pos.y += focused.position.y;
        }

        menu.DropDown(new Rect(pos.x, pos.y, 0f, 0f));
#endif
        }

        protected virtual void OnClicked(object _object) { }
    }

}
