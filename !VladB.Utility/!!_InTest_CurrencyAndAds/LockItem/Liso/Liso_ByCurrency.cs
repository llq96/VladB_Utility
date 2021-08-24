using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "NewLockItemSO", menuName = "VladB/Liso_ByCurrency", order = 1)]
    public class Liso_ByCurrency : Liso {
        [Header("Currency Settings")]
        public Currency currency;
        public int price = 100;
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Liso_ByCurrency)), CanEditMultipleObjects]
    public class Liso_ByCurrencyEditor : LisoEditor {
        Liso_ByCurrency script => (Liso_ByCurrency)target;


        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if(script.isLockOnStart) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currency"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("price"), true);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
#endif
}