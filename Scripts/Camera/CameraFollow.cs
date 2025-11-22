using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector2 offset;

    private PlayerMovement playerMovement;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    private bool shouldFollow = true;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not assigned!");
            return;
        }

        playerMovement = target.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogWarning("PlayerMovement script not found on target!");
        }
    }

    void LateUpdate()
    {
        if (target == null || playerMovement == null) return;

        shouldFollow = !playerMovement.IsDead;

        if (shouldFollow)
        {
            desiredPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                transform.position.z
            );

            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
        }
    }
}
