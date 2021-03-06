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
		public void SetColor_Black() => color = Color.black;

		[ContextMenu("SetColor_Red")]
		public void SetColor_Red() => color = Color.red;
	}


#if UNITY_EDITOR
	[CustomEditor(typeof(Comment))]
	public class CommentEditor : Editor {
		Comment script => target as Comment;
		GUIStyle style = new GUIStyle();

		public override void OnInspectorGUI() {
			serializedObject.Update();

			style.wordWrap = true;
			style.normal.textColor = script.color;
			style.fontSize = 12;

			string text = EditorGUILayout.TextArea(script.text.IsNullOrEmpty() ? "Comment Here..." : script.text, style);
			if(text != script.text) {
				Undo.RecordObject(script, "Edit Comments");
				script.text = text;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}