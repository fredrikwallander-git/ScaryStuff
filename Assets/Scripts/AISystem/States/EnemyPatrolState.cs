using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    private Vector3[] waypoints;

    public EnemyPatrolState(EnemyAI enemy, Vector3[] waypoints, float patrolSpeed) : base(enemy)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogWarning("EnemyPatrolState requires a NavMeshAgent on the enemy.");
        }

        this.waypoints = waypoints;
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned for patrol!");
        }
    }

    public override void Enter()
    {
        if (agent != null && waypoints.Length > 0)
        {
            agent.isStopped = false;
            agent.SetDestination(waypoints[currentWaypointIndex]);
            agent.speed = enemy.patrolSpeed;
        }
    }

    public override void Tick()
    {
        if (agent == null || waypoints.Length == 0) return;

        if (enemy.DistanceToPlayer() <= enemy.chaseDistance)
        {
            enemy.ChangeState(enemy.chaseState);
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = true;
    }
}