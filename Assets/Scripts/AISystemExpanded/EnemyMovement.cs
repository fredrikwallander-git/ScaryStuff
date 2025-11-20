using UnityEngine;
using UnityEngine.AI;

namespace AISystemExpanded
{
	public class EnemyMovement : MonoBehaviour
	{
		private NavMeshAgent agent;

		private void Awake() => agent = GetComponent<NavMeshAgent>();

		public void MoveTo(Vector3 position)
		{
			agent.isStopped = false;
			agent.SetDestination(position);
		}

		public void StopMoving()
		{
			agent.isStopped = true;
			agent.velocity = Vector3.zero;
		}

		public bool ReachedDestination()
		{
			if (agent.pathPending) return false;

			return agent.remainingDistance <= agent.stoppingDistance &&
				(!agent.hasPath || agent.velocity.sqrMagnitude < 0.1f);
		}
		
		public void SetMovementSpeed(float speed)
		{
			agent.speed = speed;
		}
	}
}