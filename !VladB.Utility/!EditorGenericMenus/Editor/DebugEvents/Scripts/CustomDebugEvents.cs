using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "CustomDebugEvents", menuName = "VladB/GenericMenu/CustomDebugEvents", order = 2)]
    public class CustomDebugEvents : GenericMenuBase {
        public List<bool>isShow = new List<bool>();
        public bool isCustomEditorGUI = true;

        [Space(50)]
        public CustomEvent_WithoutParams[] events_withoutParams = new CustomEvent_WithoutParams[0];
        [Space (50)]
        public CustomEvent_int[] events_int = new CustomEvent_int[0];
        [Space(50)]
        public CustomEvent_Float[] events_float = new CustomEvent_Float[0];
        [Space(50)]
        public CustomEvent_String[] events_string = new CustomEvent_String[0];

        public override bool GenerateGenericMenu() {
            if (!base.GenerateGenericMenu()) {
                return false;
            }

            //Заполнение меню
            AddMenuItems();

            //Показ меню в месте курсора мыши
            ShowGenericMenuAtCursorPosition();
            return true;
        }

        void AddMenuItems() {
            for (int i = 0; i < events_withoutParams.Length; i++) {
                events_withoutParams[i].AddMenuItems(this, i);
            }

            menu.AddSeparator("");
            for (int i = 0; i < events_int.Length; i++) {
                events_int[i].AddMenuItems(this, i);
            }

            menu.AddSeparator("");
            for (int i = 0; i < events_float.Length; i++) {
                events_float[i].AddMenuItems(this, i);
            }

            menu.AddSeparator("");
            for (int i = 0; i < events_string.Length; i++) {
                events_string[i].AddMenuItems(this, i);
            }
        }


        protected override void OnClicked(object _object) {
            Invoke((SelectedItemInfo)_object);
        }


        void Invoke(SelectedItemInfo info) {
            if (info != null) {
                switch (info.type) {
                    case CustomEventType.Basic:
                        events_withoutParams[info.eventIndex].Invoke(info.extraInfo);
                        break;
                    case CustomEventType.Int:
                        events_int[info.eventIndex].Invoke(info.extraInfo);
                        break;
                    case CustomEventType.Float:
                        events_float[info.eventIndex].Invoke(info.extraInfo);
                        break;
                    case CustomEventType.String:
                        events_string[info.eventIndex].Invoke(info.extraInfo);
                        break;
                }

               
            }
        }



        [ContextMenu("EnableCustomEditor")]
        public void EnableCustomEditor() {
            isCustomEditorGUI = true;
        }

        [ContextMenu("DisableCustomEditor")]
        public void DisableCustomEditor() {
            isCustomEditorGUI = false;
        }

        public void Func_1() {

            Debug.Log("Func_1   ");
        }
        public void Func_2(int value) {
            Debug.Log("Func_2   " + value);
        }

        public void Func_LoadLevel(int value) {
            Debug.Log("Func_LoadLevel   " + value);
        }

        public void Func_3(float value) {
            Debug.Log("Func_3   " + value);
        }
        public void Func_4(string s) {
            Debug.Log("Func_4   " + s);
        }
    }

    [System.Serializable]
    public class SelectedItemInfo {
        public CustomEventType type;
        public int eventIndex;
        public object extraInfo;

        public SelectedItemInfo(CustomEventType _type , int _eventIndex, object _extraInfo) {
            type = _type;
            eventIndex = _eventIndex;
            extraInfo = _extraInfo;
        }
    }

    public enum CustomEventType {
        Basic,
        Int,
        Float,
        String
    }
}
