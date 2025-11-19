using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    private readonly float chaseSpeed;
    private NavMeshAgent agent;
    private EnemyVision vision;
    private Vector3 lastKnownPlayerPosition;
    private bool hasSeenPlayer;

    public EnemyChaseState(EnemyAI enemy, float chaseSpeed) : base(enemy)
    {
        this.chaseSpeed = chaseSpeed;
        agent = enemy.GetComponent<NavMeshAgent>();
        vision = enemy.GetComponent<EnemyVision>();

        if (agent == null)
            Debug.LogWarning("EnemyChaseState requires a NavMeshAgent.");
        if (vision == null)
            Debug.LogWarning("EnemyChaseState requires EnemyVision component.");
    }

    public override void Enter()
    {
        hasSeenPlayer = false;
        if (vision != null)
            lastKnownPlayerPosition = vision.lastKnownPlayerPosition;

        if (agent != null)
        {
            agent.isStopped = false;
            agent.speed = chaseSpeed;
        }
    }

    public override void Tick()
    {
        if (agent == null || vision == null) return;

        // Hunt the player if they are in sight
        if (vision.playerInSight)
        {
            hasSeenPlayer = true;
            lastKnownPlayerPosition = vision.lastKnownPlayerPosition;
            agent.SetDestination(vision.player.position);
        }
        // If we loose sight, then go to the last know location of the player
        else if (hasSeenPlayer)
        {
            agent.SetDestination(lastKnownPlayerPosition);

            // Switch to idle if there's no player around when reaching the last know position
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                enemy.ChangeState(enemy.idleState);
            }
        }
        // Otherwise, switch to patrolling if we have points to go to, else idle
        else
        {
            if (enemy.patrolWaypoints.Length > 0)
                enemy.ChangeState(enemy.patrolState);
            else
                enemy.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = true;
    }
}
