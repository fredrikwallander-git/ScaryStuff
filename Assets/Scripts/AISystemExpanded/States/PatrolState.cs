using AISystemExpanded.Configuration;
using UnityEngine;

namespace AISystemExpanded.States
{
	public class PatrolState : IState
	{
		private EnemyAIController ctx;

		public PatrolState(EnemyAIController ctx) => this.ctx = ctx;

		public void Enter()
		{
			ctx.movement.SetMovementSpeed(ctx.enemyConfig.WalkSpeed);
			if (ctx.patrol.HasWaypoints)
				ctx.movement.MoveTo(ctx.patrol.CurrentWaypoint);
		}

		public StateType? Tick()
		{
			if (ctx.eyes.IsPlayerInSight)
				return ctx.enemyConfig.CanFlee ? StateType.Flee : StateType.Chase;

			if (ctx.movement.ReachedDestination())
			{
				ctx.patrol.AdvanceWaypoint();
				ctx.movement.MoveTo(ctx.patrol.CurrentWaypoint);
			}

			return null;
		}

		public void Exit() => ctx.movement.StopMoving();
	}
}