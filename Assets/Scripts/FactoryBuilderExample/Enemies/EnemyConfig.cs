using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "New EnemyConfig", menuName = "Enemies/Config")]
	public class EnemyConfig : ScriptableObject
	{
		public EnemyType type;
		public float movementSpeed;
		public int health;
	}
}