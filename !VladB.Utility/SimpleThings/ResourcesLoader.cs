using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.Utility {
	public class ResourcesLoader : MonoBehaviour {

		public static Sprite LoadSprite(string path) {

#if UNITY_EDITOR

			Sprite spr = Resources.Load<Sprite>(path);
			if (spr == null) {
				Debug.LogError("Dont Found :   " + path);
			}
			return spr;

#else
		return Resources.Load<Sprite> (path);
#endif
		}


		public static Material LoadMaterial(string path) {
#if UNITY_EDITOR

			Material mat = Resources.Load<Material>(path);
			if (mat == null) {
				Debug.LogError("Dont Found :   " + path);
			}
			return mat;

#else
		return Resources.Load<Material> (path);
#endif
		}


		public static GameObject LoadGameObject(string path) {

#if UNITY_EDITOR

			GameObject go = Resources.Load<GameObject>(path);
			if (go == null) {
				Debug.LogError("Dont Found :   " + path);
			}
			return go;

#else
		return Resources.Load<GameObject> (path);
#endif
		}
	}
}