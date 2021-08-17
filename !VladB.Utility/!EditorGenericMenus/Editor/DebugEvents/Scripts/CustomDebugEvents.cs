using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "CustomDebugEvents", menuName = "VladB/GenericMenu/CustomDebugEvents", order = 2)]
    public class CustomDebugEvents : GenericMenuBase {
        public List<bool> isShow = new List<bool>();
        public bool isCustomEditorGUI = true;

        [Space(50)] public CustomEvent_WithoutParams[] events_withoutParams = new CustomEvent_WithoutParams[0];
        [Space(50)] public CustomEvent_int[] events_int = new CustomEvent_int[0];
        [Space(50)] public CustomEvent_Float[] events_float = new CustomEvent_Float[0];
        [Space(50)] public CustomEvent_String[] events_string = new CustomEvent_String[0];
        // (!) При добавлении нового типа эвентов нужно в методе AddMenuItems
        //добавить массив в список allEvents по аналогии с остальными 
        public List<object> allEvents;

        protected override bool GenerateGenericMenu() {
            if(!base.GenerateGenericMenu()) {
                return false;
            }

            //Заполнение меню
            AddMenuItems();

            //Показ меню в месте курсора мыши
            ShowGenericMenuAtCursorPosition();
            return true;
        }

        void AddMenuItems() {
            //Добавление массивов эвентов разных типов в один список
            allEvents = new List<object>() { events_withoutParams, events_int, events_float, events_string };
            //
            allEvents.Act((item, i) => {
                (item as CustomEvent[]).Act((item, k) => item.AddMenuItems(this, k));
            });
        }

        protected override void OnClicked(object _object) {
            Invoke((SelectedItemInfo)_object);
        }

        void Invoke(SelectedItemInfo info) {
            if(info == null) {
                return;
            }
            (allEvents[(int)info.type] as CustomEvent[]) //Берём массив эвентов определённого тмпа
                    [info.eventIndex] // По индексу эвента достаём эвент
                    .Invoke(info.extraInfo);// и выполняем его
        }


        #region Enable/Disable CustomEditorUI
        [ContextMenu("EnableCustomEditor")]
        public void EnableCustomEditor() {
            isCustomEditorGUI = true;
        }

        [ContextMenu("DisableCustomEditor")]
        public void DisableCustomEditor() {
            isCustomEditorGUI = false;
        }
        #endregion

        #region TestFunctions
        public void TestFunc_1() => Debug.Log("TestFunc_1   ");
        public void TestFunc_2(int value) => Debug.Log("TestFunc_2   " + value);
        public void TestFunc_3(float value) => Debug.Log("TestFunc_3   " + value);
        public void TestFunc_4(string s) => Debug.Log("TestFunc_4   " + s);
        #endregion
    }

    [System.Serializable]
    public class SelectedItemInfo {
        public CustomEventType type;
        public int eventIndex;
        public object extraInfo;

        public SelectedItemInfo(CustomEventType _type, int _eventIndex, object _extraInfo) {
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

