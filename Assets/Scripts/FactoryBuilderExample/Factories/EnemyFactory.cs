using UnityEngine;

namespace DefaultNamespace.Factories
{
	// Enemy factory example
	public class EnemyFactory
	{
		public GameObject dogPrefab;
		public GameObject wolfPrefab;
		public GameObject hawkPrefab;
		
		// BASIC
		public Enemy CreateEnemy(EnemyConfig config)
		{
			return CreateEnemy(config, Vector3.zero, Quaternion.identity);
		}
		
		// WITH POSITION
		public Enemy CreateEnemy(EnemyConfig config, Vector3 position)
		{
			return CreateEnemy(config, position, Quaternion.identity);
		}
		
		// WITH POSITION & ROTATION
		public Enemy CreateEnemy(EnemyConfig config, Vector3 position, Quaternion rotation)
		{
			GameObject enemy = null;

			switch (config.type)
			{
				case EnemyType.Dog:
					enemy = GameObject.Instantiate(dogPrefab, position, rotation);
					// + further setup if necessary or use switch expression if no more setup is needed.
					break;
				case EnemyType.Wolf:
					enemy = GameObject.Instantiate(wolfPrefab, position, rotation);
					break;
				case EnemyType.Hawk:
					enemy = GameObject.Instantiate(hawkPrefab, position, rotation);
					break;
			}
			
			// Switch expression example:
			// GameObject enemy = config.type switch
			// {
			// 	EnemyType.Dog => GameObject.Instantiate(dogPrefab, position, rotation),
			// 	EnemyType.Wolf => GameObject.Instantiate(wolfPrefab, position, rotation),
			// 	EnemyType.Hawk => GameObject.Instantiate(hawkPrefab, position, rotation),
			// 	_ => null
			// };
			
			var enemyComponent = enemy.GetComponent<Enemy>();
			enemyComponent.health = config.health;
			enemyComponent.movementSpeed = config.movementSpeed;
			
			return enemyComponent;
		}

		// + any other like CreateRandomEnemy() etc.
		
		// ---------------------------------------------- OTHER EXAMPLES ----------------------------------------------- //
		
		// Stat overrides example:
		// public Enemy CreateEnemy(
		// 	EnemyConfig config,
		// 	Vector3 position,
		// 	int? overrideHealth = null,
		// 	float? overrideMoveSpeed = null,
		// 	float? overrideChaseSpeed = null,
		// 	bool? overrideChase = null)
		// {
		// 	var enemy = CreateEnemy(config, position, Quaternion.identity);
		//
		// 	// Apply overrides only if provided
		// 	enemy.Health = overrideHealth ?? config.health;
		// 	enemy.MoveSpeed = overrideMoveSpeed ?? config.moveSpeed;
		// 	enemy.ChaseSpeed = overrideChaseSpeed ?? config.chaseSpeed;
		// 	enemy.CanChasePlayer = overrideChase ?? config.doesChasePlayer;
		//
		// 	return enemy;
		// }
		
		// Or AI Modules to add:
		// public Enemy CreateEnemy(
		// 	EnemyConfig config,
		// 	Vector3 position,
		// 	params IEnemyModule[] modules)
		// {
		// 	var enemy = CreateEnemy(config, position, Quaternion.identity);
		//
		// 	foreach (var module in modules)
		// 		module.Install(enemy);
		//
		// 	return enemy;
		// }
	}

	// Usage example
	public class EnemySpawner : MonoBehaviour
	{
		EnemyFactory enemyFactory;
		EnemyConfig slowEnemy;
		EnemyConfig fastEnemy;

		public void Start()
		{
			enemyFactory = new EnemyFactory();

			// Test spawn different enemies, cache if necessary
			var fastOne = SpawnEnemy(fastEnemy);
			SpawnEnemy(slowEnemy);
		}

		public Enemy SpawnEnemy(EnemyConfig config)
		{
			return enemyFactory.CreateEnemy(config);
		}
	}
}