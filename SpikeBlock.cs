using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed = 2f;

    [Header("Spike Settings")]
    public float jumpForce = 10f;
    public float pushForce = 5f;
    public int damageAmount = 1;

    private Vector3 targetPosition;
    private bool movingToEnd = true;

    void Start()
    {
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (movingToEnd)
            {
                targetPosition = startPoint.position;
            }
            else
            {
                targetPosition = endPoint.position;
            }
            movingToEnd = !movingToEnd;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (player != null && rb != null)
            {
                player.TakeDamage();

                Vector2 pushDirection = (other.transform.position - transform.position).normalized;

                rb.velocity = new Vector2(pushDirection.x * pushForce, jumpForce);
            }
        }
    }
}
