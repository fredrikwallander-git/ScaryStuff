using UnityEditor;
using UnityEngine;

namespace ToolsPropertiesAndAttributes.LevelTools.Editor
{
	public class LevelToolWindow : EditorWindow
	{
		[MenuItem("Window/LevelTools")]
		public static void ShowWindow()
		{
			GetWindow<LevelToolWindow>("Level Tools");
		}

		void OnGUI()
		{
			GUILayout.Label("Quick Tools", EditorStyles.boldLabel);

			if (GUILayout.Button("Create Cube"))
			{
				GameObject.CreatePrimitive(PrimitiveType.Cube);
			}

			if (GUILayout.Button("Reset Scene Lighting"))
			{
				RenderSettings.ambientLight = Color.white;
			}
		}
	}
}