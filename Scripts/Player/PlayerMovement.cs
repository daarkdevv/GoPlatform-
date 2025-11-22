using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Lives and Damage")]
    public int maxLives = 5;
    public float invincibleDuration = 2f;
    [HideInInspector] public int currentLives;
    private bool isInvincible = false;
    private Coroutine damageFlashRoutine;

    [Header("Sound Effects")]
    public AudioClip jumpSFX;
    public AudioClip hurtSFX;
    public AudioClip deathSFX;
    public AudioClip crowdAngerSFX;

    [Header("Game Over Screen")]
    public GameOverScreen gameOverScreen;
    public float fallDelayBeforeGameOver = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    private bool isGrounded;
    private float moveInput;
    private bool isDead = false;
    [HideInInspector] public bool isMoving = false;

    public HeartsUI heartsUI;

    public bool IsDead => isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        PlayerStats.ResetStats();
        if (rb == null || animator == null || spriteRenderer == null || playerCollider == null)
        {
            Debug.LogError("Some required components are missing on the player!");
            return;
        }

        currentLives = maxLives;

        if (heartsUI != null)
            heartsUI.InitializeHearts(currentLives);
    }

    void Update()
    {
        if (isDead || !isMoving) return;

        moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckRadius, Color.green);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        if (isDead || !isMoving) return;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        SFXManager.Instance.PlaySFX(jumpSFX);
        PlayerStats.AddJump();
    }

    public void TakeDamage()
    {
        if (isInvincible || isDead)
        {
            return;
        }

        currentLives--;

        if (heartsUI != null)
            heartsUI.RemoveHeart();
        else
            Debug.LogWarning("heartsUI is not linked!");

        PlayerStats.score -= 200;
        if (PlayerStats.score < 0) PlayerStats.score = 0;

        if (hurtSFX != null)
            SFXManager.Instance.PlaySFX(hurtSFX);
        else
            Debug.LogWarning("hurtSFX is not linked!");

        animator.SetTrigger("Hurt");

        isInvincible = true;

        if (damageFlashRoutine != null)
            StopCoroutine(damageFlashRoutine);
        damageFlashRoutine = StartCoroutine(DamageFlash());

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            Invoke(nameof(ResetInvincibility), invincibleDuration);
        }
    }

    public void ForceDie()
    {
        if (isDead)
        {
            return;
        }

        currentLives = 0;

        if (heartsUI != null)
            heartsUI.RemoveHeart();
        else
            Debug.LogWarning("heartsUI is not linked!");

        Die();
    }

    IEnumerator DamageFlash()
    {
        float flashDuration = 0.1f;
        int flashCount = 5;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }

        spriteRenderer.enabled = true;
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }

    void Die()
    {
        animator.SetTrigger("Die");

        isDead = true;

        if (deathSFX != null)
            SFXManager.Instance.PlaySFX(deathSFX);
        else
            Debug.LogWarning("deathSFX is not linked!");

        if (crowdAngerSFX != null)
            SFXManager.Instance.PlaySFX(crowdAngerSFX);
        else
            Debug.LogWarning("crowdAngerSFX is not linked!");

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
        else
        {
            Debug.LogError("playerCollider is missing!");
        }

        if (rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing!");
        }

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            rb.gravityScale = 1f;
        }

        if (gameOverScreen != null)
        {
            StartCoroutine(FallAndShowGameOver());
        }
        else
        {
            Debug.LogWarning("gameOverScreen is not linked!");
        }
    }

    IEnumerator FallAndShowGameOver()
    {
        yield return new WaitForSeconds(fallDelayBeforeGameOver);

        if (gameOverScreen != null)
        {
            gameOverScreen.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("GameOverScreen is not linked in the editor!");
        }
    }
}
