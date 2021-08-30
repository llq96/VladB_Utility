using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
    [CreateAssetMenu(fileName = "NewLockItemSO", menuName = "VladB/Liso_ByAds", order = 1)]
    public class Liso_ByAds : Liso {
        [Header("ADS Settings")]
        public int startCountAds = 1;

        public int currentCountAds {
            get => PlayerPrefs.GetInt($"{ppName}_CountAds", startCountAds);
            set => PlayerPrefs.SetInt($"{ppName}_CountAds", value);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Liso_ByAds)), CanEditMultipleObjects]
    public class Liso_ByAdsEditor : LisoEditor {
        Liso_ByAds script => (Liso_ByAds)target;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if(script.isLockOnStart) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("startCountAds"), true);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
#endif
}