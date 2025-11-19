using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Rotation")]
    public float mouseSensitivity = 2f;
    public float minPitch = -30f;
    public float maxPitch = 70f;
    public float rotationSmoothTime = 0.08f;

    [Header("Zoom")]
    public float defaultDistance = 12f;
    public float height = 1.7f;
    public float zoomSpeed = 5f;
    public float minDistance = 8f;
    public float maxDistance = 30f;
    public float zoomSmoothTime = 0.1f;

    [Header("Collision")]
    public float collisionRadius = 0.3f;
    public LayerMask collisionMask;

    private PlayerControls controls;
    private Vector2 lookInput;
    private float scrollInput;

    private float yaw;
    private float pitch;

    private float currentYaw;
    private float currentPitch;
    private float yawVelocity;
    private float pitchVelocity;

    private float targetDistance;
    private float currentDistance;
    private float zoomVelocity;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.Zoom.performed += ctx => scrollInput = ctx.ReadValue<float>();
        controls.Player.Zoom.canceled += ctx => scrollInput = 0f;

        targetDistance = defaultDistance;
        currentDistance = defaultDistance;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
        HandleCollision();
        UpdateCameraPosition();
    }

    private void HandleRotation()
    {
        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        currentYaw = Mathf.SmoothDampAngle(currentYaw, yaw, ref yawVelocity, rotationSmoothTime);
        currentPitch = Mathf.SmoothDampAngle(currentPitch, pitch, ref pitchVelocity, rotationSmoothTime);

        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    private void HandleZoom()
    {
        targetDistance -= scrollInput * zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        scrollInput = 0f;

        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime / zoomSmoothTime);
    }

    private void HandleCollision()
    {
        Vector3 desiredPosition = target.position + transform.rotation * new Vector3(0, height, -currentDistance);
        Vector3 direction = desiredPosition - target.position;
        float distance = direction.magnitude;

        if (Physics.SphereCast(target.position + Vector3.up * height * 0.5f, collisionRadius, direction.normalized, out RaycastHit hit, distance, collisionMask))
        {
            currentDistance = Mathf.Lerp(currentDistance, hit.distance - collisionRadius, Time.deltaTime / zoomSmoothTime);
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 offset = transform.rotation * new Vector3(0, height, -currentDistance);
        transform.position = target.position + offset;
    }
}
