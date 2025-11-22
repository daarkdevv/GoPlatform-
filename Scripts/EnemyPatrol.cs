using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;
    public AudioClip idleSound;
    public AudioClip deathSound;

    private Vector3 targetPoint;
    private SpriteRenderer spriteRenderer;
    private Collider2D myCollider;
    private bool isDead = false;
    private float idleTimer = 0f;

    void Start()
    {
        targetPoint = pointB.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        UpdateFacingDirection();

        gameObject.layer = LayerMask.NameToLayer("Enemies");
    }

    void Update()
    {
        if (isDead) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.25f)
        {
            targetPoint = (targetPoint == pointA.position) ? pointB.position : pointA.position;
            UpdateFacingDirection();
        }

        idleTimer += Time.deltaTime;
        if (idleTimer >= 8f)
        {
            idleTimer = 0f;
            PlayIdleSound();
        }
    }

    void UpdateFacingDirection()
    {
        spriteRenderer.flipX = targetPoint.x > transform.position.x;
    }

    private void PlayIdleSound()
    {
        if (idleSound != null && !isDead)
            SFXManager.Instance.PlaySFX(idleSound);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.contacts[0];

            if (contact.normal.y < -0.5f && !isDead)
            {
                Die();

                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.Jump();
                }
            }
            else
            {
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage();
                }
            }
        }
    }

    void Die()
    {
        isDead = true;
        myCollider.enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<SpriteRenderer>().color = Color.gray;

        if (deathSound != null)
            SFXManager.Instance.PlaySFX(deathSound);

        PlayerStats.AddKill();
        Destroy(gameObject, 1f);
    }
}
