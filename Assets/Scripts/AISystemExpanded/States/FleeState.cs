using AISystemExpanded.Configuration;
using UnityEngine;

namespace AISystemExpanded.States
{
	public class FleeState : IState
	{

		private EnemyAIController ctx;

		public FleeState(EnemyAIController ctx) => this.ctx = ctx;

		public void Enter()
		{
			ctx.movement.SetMovementSpeed(ctx.enemyConfig.FleeSpeed);
			if (ctx.eyes.IsPlayerNearby)
			{
				MoveAwayFromPlayer();
			}
		}

		public StateType? Tick()
		{
			if (ctx.eyes.IsPlayerNearby && ctx.movement.ReachedDestination())
			{
				MoveAwayFromPlayer();
				return null;
			}

			if (!ctx.eyes.IsPlayerNearby && ctx.movement.ReachedDestination())
				return StateType.Idle;

			return null;
		}

		public void Exit() => ctx.movement.StopMoving();
		
		private void MoveAwayFromPlayer()
		{
			Vector3 direction = ctx.eyes.Player
				? (ctx.transform.position - ctx.eyes.Player.position).normalized
				: (ctx.transform.position - ctx.eyes.LastKnownPlayerPosition).normalized;
			
			Vector3 fleeTarget = ctx.transform.position + direction * Random.Range(ctx.enemyConfig.MinFleeDistance, ctx.enemyConfig.MaxFleeDistance);
			ctx.movement.MoveTo(fleeTarget);
		}
	}
}