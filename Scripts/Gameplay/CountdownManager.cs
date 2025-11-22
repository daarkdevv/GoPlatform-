using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public TMP_Text countdownText;
    public PlayerMovement playerMovement;
    public float countdownDuration = 1f;
    public AudioClip tickSFX;
    public AudioClip finalSFX;

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("countdownText is not assigned!");
            return;
        }

        if (playerMovement != null)
        {
            playerMovement.isMoving = false;
            Debug.Log("Player movement disabled at scene start");
        }
        else
        {
            Debug.LogWarning("playerMovement is not assigned!");
        }

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countdownText.text = "3";
        if (tickSFX != null)
        {
            SFXManager.Instance.PlaySFX(tickSFX);
        }
        else
        {
            Debug.LogWarning("tickSFX is not assigned!");
        }
        yield return new WaitForSeconds(countdownDuration);

        countdownText.text = "2";
        if (tickSFX != null)
        {
            SFXManager.Instance.PlaySFX(tickSFX);
        }
        else
        {
            Debug.LogWarning("tickSFX is not assigned!");
        }
        yield return new WaitForSeconds(countdownDuration);

        countdownText.text = "1";
        if (tickSFX != null)
        {
            SFXManager.Instance.PlaySFX(tickSFX);
        }
        else
        {
            Debug.LogWarning("tickSFX is not assigned!");
        }
        yield return new WaitForSeconds(countdownDuration);

        countdownText.text = "";
        if (finalSFX != null)
        {
            SFXManager.Instance.PlaySFX(finalSFX);
        }
        else
        {
            Debug.LogWarning("finalSFX is not assigned!");
        }

        if (playerMovement != null)
        {
            playerMovement.isMoving = true;
            Debug.Log("Player movement enabled after countdown");
        }
    }
}
