using UnityEngine;

public class Spike : MonoBehaviour
{
    public float jumpForce = 10f;
    public int damageAmount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (player != null && rb != null)
            {
                player.TakeDamage();
                player.Jump();
            }
        }
    }
}
