using UnityEngine;

namespace VladB.Utility {
	public class PlatformObjectsActivator : MonoBehaviour {
		[Header("Settings")]
		[SerializeField] protected bool isDestroyUnUsedObjects;
		[SerializeField] protected bool isDestroySelf = true;
		[Space(10)]
		[SerializeField] protected bool isDebugInverse;

		[Header("Only Android")]
		[SerializeField] protected GameObject[] onlyAndroid;

		[Header("Only IOS")]
		[SerializeField] protected GameObject[] onlyIOS;


		void Awake() => AwakeFunc();
		protected virtual void AwakeFunc() {
			bool isAndoid = false;
			bool isIOS = false;

#if UNITY_ANDROID
			isAndoid = true;
#endif

#if UNITY_IOS
			isIOS = true;
#endif

            if(isDebugInverse) {
				Debug.Log("Careful: Inverse Is Enable!");
				Debug.LogWarning("Inverse Is Enable!!");
                isAndoid = !isAndoid;
				isIOS = !isIOS;
			}

			onlyAndroid.Act((item, i) => SetActiveObject(item, isAndoid));
			onlyIOS.Act((item, i) => SetActiveObject(item, isIOS));

			if(isDestroySelf) {
				DestroySelf();
			}
		}

		protected virtual void SetActiveObject(GameObject obj, bool isActive) {
			if(obj == null) {
				return;
			}
			obj.SetActive(isActive);
			if((isActive == false) && (isDestroyUnUsedObjects)) {
				Destroy(obj);
			}
		}

		protected virtual void DestroySelf() {
			int childs = gameObject.transform.childCount;
			Destroy((childs == 0) ? gameObject : (Object)this);
		}
	}
}