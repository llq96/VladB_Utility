using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VladB.Utility {
	public class Comment : MonoBehaviour {
		[Multiline] public string text;
		public Color color = Color.black;

#if !UNITY_EDITOR
	private void Awake() {
		Destroy(this);
	}
#endif

		[ContextMenu("SetColor_Black")]
		public void SetColor_Black() {
			color = Color.black;
		}

		[ContextMenu("SetColor_Red")]
		public void SetColor_Red() {
			color = Color.red;
		}


	}


#if UNITY_EDITOR
	[CustomEditor(typeof(Comment))]
	public class CommentEditor : Editor {

		private Comment script { get { return target as Comment; } }
		private GUIStyle style = new GUIStyle();

		public override void OnInspectorGUI() {
			serializedObject.Update();

			if (serializedObject == null) {
				return;
			}

			style.wordWrap = true;
			style.normal.textColor = script.color;
			style.fontSize = 12;

			//EditorGUILayout.Space();

			string text = EditorGUILayout.TextArea(string.IsNullOrEmpty(script.text) ? "Comment Here..." : script.text, style);
			if (text != script.text) {
				Undo.RecordObject(script, "Edit Comments");
				script.text = text;
			}

			//EditorGUILayout.Space();

			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}