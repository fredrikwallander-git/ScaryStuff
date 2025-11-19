using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float idleDuration;
    float currentIdleDuration;

    public EnemyIdleState(EnemyAI enemy, float idleDuration) : base(enemy)
    {
        this.idleDuration = idleDuration;
    }

    public override void Enter()
    {
        Debug.Log("Enemy entered Idle State");
        currentIdleDuration = 0;
    }

    public override void Tick()
    {
        if (enemy.DistanceToPlayer() <= enemy.chaseDistance)
        {
            enemy.ChangeState(enemy.chaseState);
        }
        
        currentIdleDuration += Time.deltaTime;
        if (currentIdleDuration >= idleDuration)
        {
            enemy.ChangeState(enemy.patrolState);
            currentIdleDuration = 0;
        }
    }

    public override void Exit()
    {
        Debug.Log("Enemy exiting Idle State"); 
        currentIdleDuration = 0;
    }
}