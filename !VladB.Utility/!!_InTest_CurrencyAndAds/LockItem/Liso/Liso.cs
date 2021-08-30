using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//[CreateAssetMenu(fileName = "NewLockItemSO", menuName = "VladB/LockItemSO", order = 1)]
namespace VladB.Utility {
    public abstract class Liso : ScriptableObject {
        [Header("Basic Settings")]
        public bool isLockOnStart = false;

        [Header("Player Prefs")]
        public string ppName = "";

        public virtual bool available {
            get {
                if(ppName.IsNullOrEmpty()) {
                    ppName = Extensions.GetRandomPP();
                }
                return (!isLockOnStart || (PlayerPrefs.GetInt(ppName, 0) == 1));
            }
            set {
                if(ppName.IsNullOrEmpty()) {
                    ppName = Extensions.GetRandomPP();
                }
                PlayerPrefs.SetInt(ppName, (value == true) ? 1 : 0);
            }
        }


    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Liso)), CanEditMultipleObjects]
    public class LisoEditor : Editor {
        Liso script => (Liso)target;


        public override void OnInspectorGUI() {
            serializedObject.Update();

            if(script.available) {
                EditorGUILayout.HelpBox("Now Item Is Available", MessageType.None);
            } else {
                EditorGUILayout.HelpBox("Now Item Is Not Available", MessageType.None);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("isLockOnStart"), true);

            //if(script.isLockOnStart) {
            //    EditorGUILayout.PropertyField(serializedObject.FindProperty("ppName"), true);
            //}

            serializedObject.ApplyModifiedProperties();
        }

        //public void DefaultInspectorGUI() {
        //    base.OnInspectorGUI();
        //}

    }
#endif
}
