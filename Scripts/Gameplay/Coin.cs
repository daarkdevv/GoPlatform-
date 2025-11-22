using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSound != null)
            {
                SFXManager.Instance.PlaySFX(collectSound);
            }

            PlayerStats.AddStar();
            gameObject.SetActive(false);
        }
    }
}
