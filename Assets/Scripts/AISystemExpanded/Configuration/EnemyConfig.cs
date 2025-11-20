using UnityEngine;

namespace AISystemExpanded.Configuration
{
	[CreateAssetMenu(fileName = "New EnemyConfig", menuName = "Enemy/EnemyConfig")]
	public class EnemyConfig : ScriptableObject
	{
		public float AttackCooldown = 1f;
		public float WalkSpeed = 4f;
		public float ChaseSpeed = 8f;
		public float FleeSpeed = 12f;
		public float MinFleeDistance = 12f;
		public float MaxFleeDistance = 35f;
		public float AwarenessRangeMultiplier = 1.25f;
		public float ViewDistance = 15f;
		[Range(0, 360)] public float ViewAngle = 125f;
		public float MinIdleTime = 1.2f;
		public float MaxIdleTime = 3.5f;
		public bool CanFlee = false;
		public float AttackRange = 2f;
	}
}