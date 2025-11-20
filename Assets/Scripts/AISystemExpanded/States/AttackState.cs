using AISystemExpanded.Configuration;
using UnityEngine;

namespace AISystemExpanded.States
{
	public class AttackState : IState
	{
		private EnemyAIController ctx;
		private float cooldown;

		public AttackState(EnemyAIController ctx) => this.ctx = ctx;

		public void Enter()
		{
			ctx.movement.StopMoving();
			cooldown = 0f;
		}

		public StateType? Tick()
		{
			if (!ctx.eyes.IsPlayerInSight)
				return StateType.Idle;

			float dist = Vector3.Distance(ctx.transform.position, ctx.eyes.Player.position);

			if (dist > 3f)
				return StateType.Chase;

			cooldown -= Time.deltaTime;
			if (cooldown <= 0f)
			{
				ctx.combat.PerformAttack();
				cooldown = ctx.enemyConfig.AttackCooldown;
			}

			return null;
		}

		public void Exit() { }
	}
}