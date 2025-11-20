using System.Collections.Generic;
using AISystemExpanded.Configuration;
using AISystemExpanded.States;
using UnityEngine;

namespace AISystemExpanded
{
	public class EnemyAIController : MonoBehaviour
	{
        public EnemyStateConfig stateConfig;
        public EnemyConfig enemyConfig;
        public List<Vector3> waypoints = new List<Vector3>();
        public EnemyEyes eyes { get; private set; }
        public EnemyMovement movement { get; private set; }
        public EnemyPatrol patrol { get; private set; }
        public EnemyCombat combat { get; private set; }

        private StateMachine fsm;

        private void Awake()
        {
            eyes = GetComponent<EnemyEyes>();
            
            movement = GetComponent<EnemyMovement>();
            
            patrol = GetComponent<EnemyPatrol>();
            patrol.waypoints = waypoints;
            
            combat = GetComponent<EnemyCombat>();
        }

        private void Start()
        {
            var states = new Dictionary<StateType, IState>();

            foreach (var type in stateConfig.States)
                states[type] = CreateState(type);

            fsm = new StateMachine(states, stateConfig.StartingState);
        }

        private void Update() => fsm.Update();

        IState CreateState(StateType type)
        {
            return type switch
            {
                StateType.Idle   => new IdleState(this),
                StateType.Patrol => new PatrolState(this),
                StateType.Chase  => new ChaseState(this),
                StateType.Attack => new AttackState(this),
                StateType.Flee   => new FleeState(this),
                _ => null
            };
        }
	}
}