using UnityEngine;

[RequireComponent(typeof(Transform))]
public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewDistance = 15f;
    [Range(0, 360)] public float viewAngle = 90f;
    public LayerMask obstructionMask;   // Layers that block vision
    public LayerMask detectableMask;    // Layers the enemy can detect (e.g., player layer)

    [HideInInspector] public Transform player;                    // Current player in sight
    [HideInInspector] public Vector3 lastKnownPlayerPosition;     // Last known position
    [HideInInspector] public bool playerInSight;

    private void Update()
    {
        playerInSight = false;
        player = null;

        // Get all colliders within viewDistance on detectable layers
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance, detectableMask);

        foreach (Collider target in targetsInViewRadius)
        {
            // Check if the target has a PlayerController component
            PlayerController pc = target.GetComponent<PlayerController>();
            if (pc == null) continue;

            Vector3 directionToTarget = target.transform.position - transform.position;
            float distance = directionToTarget.magnitude;

            // Check view cone
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (angle > viewAngle / 2f) continue;

            if (!Physics.Raycast(transform.position + Vector3.up * 1.5f,
                                 directionToTarget.normalized,
                                 distance,
                                 obstructionMask))
            {
                playerInSight = true;
                player = target.transform;
                lastKnownPlayerPosition = target.transform.position;
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.15f);
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 forward = transform.forward * viewDistance;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
