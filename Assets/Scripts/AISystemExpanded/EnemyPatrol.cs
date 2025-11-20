using System.Collections.Generic;
using UnityEngine;

namespace AISystemExpanded
{
	public class EnemyPatrol : MonoBehaviour
	{
		public List<Vector3> waypoints;
		private int index;

		public Vector3 CurrentWaypoint => waypoints[index];

		public void AdvanceWaypoint() => index = (int)Mathf.Repeat(index + 1, waypoints.Count);

		public bool HasWaypoints => waypoints != null && waypoints.Count > 0;
	}
}