using UnityEngine;

public class Flag : MonoBehaviour
{
    public PlayerWin playerWin;
    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player collided with the flag!");

            if (playerWin != null)
            {
                playerWin.TriggerWin();
            }
            else
            {
                Debug.LogWarning("playerWin is not assigned!");
            }
        }
    }
}
