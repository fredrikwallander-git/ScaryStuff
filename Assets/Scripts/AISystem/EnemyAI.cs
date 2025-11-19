using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float chaseDistance = 10f;

    [Header("Idle State")]
    public float idleDuration = 3f;
    
    [Header("Patrol")]
    public Vector3[] patrolWaypoints;
    public float patrolSpeed;

    [Header("Chase State")] 
    public float chaseSpeed;

    private EnemyState currentState;

    // TODO: Add a state machine and list of used states to be able to add more generic implementations of enemies
    public EnemyIdleState idleState;
    public EnemyChaseState chaseState;
    public EnemyPatrolState patrolState;

    void Start()
    {
        // Setup the states and give them their data they need to function
        idleState = new EnemyIdleState(this, idleDuration);
        chaseState = new EnemyChaseState(this, chaseSpeed);
        patrolState = new EnemyPatrolState(this, patrolWaypoints, patrolSpeed);

        // Set the starting state
        if (patrolWaypoints.Length > 0)
            ChangeState(patrolState);
        else
            ChangeState(idleState);
    }

    void Update()
    {
        currentState?.Tick();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    // Helper function to calculate the distance to the player
    public float DistanceToPlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector3.Distance(transform.position, player.position);
    }
}