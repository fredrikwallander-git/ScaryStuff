using System.Collections.Generic;
using UnityEngine;

namespace AISystemExpanded.Configuration
{
	[CreateAssetMenu(fileName = "New EnemyStateConfig", menuName = "Enemy/State Config")]
	public class EnemyStateConfig : ScriptableObject
	{
		public List<StateType> States;
		public StateType StartingState;
	}
}