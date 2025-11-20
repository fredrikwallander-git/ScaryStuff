using AISystemExpanded.Configuration;
using UnityEngine;

namespace AISystemExpanded.States
{
	public class ChaseState : IState
	{
		private EnemyAIController ctx;

		public ChaseState(EnemyAIController ctx) => this.ctx = ctx;

		public void Enter()
		{
			ctx.movement.SetMovementSpeed(ctx.enemyConfig.ChaseSpeed);
			if (ctx.eyes.Player != null)
				ctx.movement.MoveTo(ctx.eyes.Player.position);
		}

		public StateType? Tick()
		{
			if (!ctx.eyes.IsPlayerInSight)
				return StateType.Idle;

			float dist = Vector3.Distance(ctx.transform.position, ctx.eyes.Player.position);

			if (dist <= ctx.enemyConfig.AttackRange) // todo, move to enemy config
				return StateType.Attack;

			ctx.movement.MoveTo(ctx.eyes.Player.position);
			return null;
		}

		public void Exit() => ctx.movement.StopMoving();
	}
}