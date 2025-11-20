using AISystemExpanded.Configuration;
using UnityEngine;

namespace AISystemExpanded.States
{
	public class IdleState : IState
	{
		private EnemyAIController ctx;
		private float timer;

		public IdleState(EnemyAIController ctx) => this.ctx = ctx;

		public void Enter()
		{
			timer = Random.Range(ctx.enemyConfig.MinIdleTime, ctx.enemyConfig.MaxIdleTime);
			ctx.movement.StopMoving();
		}

		public StateType? Tick()
		{
			if (ctx.eyes.IsPlayerInSight)
			{
				return ctx.enemyConfig.CanFlee
					? StateType.Flee
					: StateType.Chase;
			}

			timer -= Time.deltaTime;
			if (timer <= 0 && ctx.patrol.HasWaypoints)
				return StateType.Patrol;

			return null;
		}

		public void Exit() { }
	}
}