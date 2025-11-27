using UnityEditor;
using UnityEngine;

namespace ToolsPropertiesAndAttributes.EnemySpawnerExample.Editor
{
	[CustomEditor(typeof(EnemySpawner))]
	public class EnemySpawnerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			EnemySpawner spawner = (EnemySpawner) target;

			if (GUILayout.Button("Spawn Enemy"))
			{
				spawner.SpawnEnemy();
			}
			
			EditorGUILayout.Space(10);
			
			EditorGUILayout.HelpBox("Enemy Settings", MessageType.Info);

			EditorGUILayout.BeginVertical("box");
			spawner.enemyHealth = EditorGUILayout.IntSlider("Health", spawner.enemyHealth, 1, 100);
			spawner.enemySpeed = EditorGUILayout.FloatField("Speed", spawner.enemySpeed);
			EditorGUILayout.EndVertical();
			
			spawner.showAdvanced = EditorGUILayout.Toggle("Show Advanced Options", spawner.showAdvanced);

			if (spawner.showAdvanced)
			{
				spawner.visionRange = EditorGUILayout.FloatField("Vision Range", spawner.visionRange);
				spawner.hearingRange = EditorGUILayout.FloatField("Hearing Range", spawner.hearingRange);
			}
		}
	}
}