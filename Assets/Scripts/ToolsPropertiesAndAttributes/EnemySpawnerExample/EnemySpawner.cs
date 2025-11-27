using UnityEngine;

namespace ToolsPropertiesAndAttributes.EnemySpawnerExample
{
	public class EnemySpawner : MonoBehaviour
	{
		public GameObject enemyPrefab;
		public bool showAdvanced;
		[HideInInspector] public int enemyHealth = 100;
		[HideInInspector] public float enemySpeed = 1;
		[HideInInspector] public float visionRange = 1;
		[HideInInspector] public float hearingRange = 1;
		
		public void SpawnEnemy()
		{
			var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
			// enemy.getcomponent<>()... set health
			// enemy.getcomponent<>()... set speed
			
		}
	}
}