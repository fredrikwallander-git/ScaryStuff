using System.Collections.Generic;
using AISystemExpanded.Configuration;
using UnityEngine;

namespace AISystemExpanded
{
	public class StateMachine
	{
		private IState current;
		private Dictionary<StateType, IState> states;

		public StateMachine(Dictionary<StateType, IState> states, StateType start)
		{
			this.states = states;
			ChangeState(start);
		}

		public void ChangeState(StateType type)
		{
			if (states.ContainsKey(type))
			{
				current?.Exit();
				current = states[type];
				current.Enter();

				return;
			}
			Debug.Log($"Warning, no state for {type} was found on this enemy.");
		}

		public void Update()
		{
			StateType? next = current.Tick();
			if (next.HasValue)
			{
				ChangeState(next.Value);
			}
		}
	}
}