using UnityEngine;

namespace AISystemExpanded
{
	public class EnemyEyes : MonoBehaviour
	{
        [Header("Debug")]
		[SerializeField] private EnemyAIController ctx;
        [SerializeField] private bool showDebug;
        
        [Header("Vision Settings")]
        public LayerMask obstructionMask;
        public LayerMask detectableMask;

        public Transform Player { get; private set; }
        public Vector3 LastKnownPlayerPosition { get; private set; }
        public bool IsPlayerNearby { get; private set; }
        public bool IsPlayerInSight => Player != null;
        
        Collider[] targets = new Collider[1]; // will only ever be one player on this layer

        public void Awake()
        {
            ctx = GetComponent<EnemyAIController>();
        }

        private void Update()
        {
            if (ctx == null || ctx.enemyConfig == null) return;

            Player= null;

            float viewDistance = ctx.enemyConfig.ViewDistance;
            float awarenessRange = ctx.enemyConfig.ViewDistance * ctx.enemyConfig.AwarenessRangeMultiplier;
            
            int numHits = Physics.OverlapSphereNonAlloc(ctx.transform.position, awarenessRange, targets, detectableMask);

            for (var index = 0; index < numHits; index++)
            {
                var target = targets[index];
                PlayerController pc = target.GetComponent<PlayerController>();

                if (!pc) continue;

                Vector3 dir = target.transform.position - ctx.transform.position;
                float dist = dir.magnitude;

                IsPlayerNearby = dist <= awarenessRange;

                if (dist <= viewDistance)
                {
                    float angle = Vector3.Angle(ctx.transform.forward, dir);
                    float viewAngle = ctx.enemyConfig.ViewAngle;

                    if (angle <= viewAngle / 2f)
                    {
                        if (!Physics.Raycast(ctx.transform.position + Vector3.up * 1.5f, dir.normalized, dist, obstructionMask))
                        {
                            Player = target.transform;
                            LastKnownPlayerPosition = Player.position;

                            break;
                        }
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!showDebug) return;
            if (ctx == null || ctx.enemyConfig == null) return;

            float viewDistance = ctx.enemyConfig.ViewDistance;
            float awarenessRange = ctx.enemyConfig.ViewDistance * ctx.enemyConfig.AwarenessRangeMultiplier;

            Gizmos.color = new Color(1f, 0f, 1f, 1f);
            Gizmos.DrawWireSphere(ctx.transform.position, viewDistance);

            Gizmos.color = new Color(1f, 0f, 0f, 1f);
            Gizmos.DrawWireSphere(ctx.transform.position, awarenessRange);

            Vector3 forward = ctx.transform.forward * viewDistance;
            Vector3 leftBoundary = Quaternion.Euler(0, -45f, 0) * forward;
            Vector3 rightBoundary = Quaternion.Euler(0, 45f, 0) * forward;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(ctx.transform.position, ctx.transform.position + leftBoundary);
            Gizmos.DrawLine(ctx.transform.position, ctx.transform.position + rightBoundary);
        }
	}
}