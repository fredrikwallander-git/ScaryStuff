using UnityEngine;

namespace AISystemExpanded
{
	public class EnemyCombat : MonoBehaviour
	{
		private EnemyAIController ctx;

		private void Awake() => ctx = GetComponent<EnemyAIController>();

		public void PerformAttack()
		{
			if (ctx.eyes.Player != null &&
				ctx.eyes.Player.TryGetComponent(out Health hp))
			{
				hp.TakeDamage(10);
			}

			Debug.Log($"{name} attacks the player!");
		}
	}
}