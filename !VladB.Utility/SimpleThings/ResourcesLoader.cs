using UnityEngine;

namespace VladB.Utility {
	public class ResourcesLoader {
		public static T Load<T>(string path) where T : Object {
#if UNITY_EDITOR
			T loadedObject = Resources.Load<T>(path);
			if(loadedObject == null) {
				Debug.LogError($"Do not found resource: {path}");
			}
			return loadedObject;
#else
		return Resources.Load<T> (path);
#endif
		}

		public static Sprite LoadSprite(string path) => Load<Sprite>(path);
		public static Material LoadMaterial(string path) => Load<Material>(path);
		public static GameObject LoadGameObject(string path) => Load<GameObject>(path);
	}
}