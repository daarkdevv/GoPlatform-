using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 180f;
    public PlayerMovement player;
    public bool isRunning = true;

    private float currentTime;
    private bool hasEnded = false;

    void Start()
    {
        currentTime = totalTime;

        if (timerText != null)
        {
            UpdateTimerDisplay();
        }
        else
        {
            Debug.LogWarning("timerText not assigned in the editor!");
        }

        if (player == null)
        {
            Debug.LogWarning("PlayerMovement not assigned in the editor!");
        }
    }

    void Update()
    {
        if (isRunning && currentTime > 0 && !hasEnded)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                hasEnded = true;
                UpdateTimerDisplay();
                OnTimeOut();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        float displayTime = Mathf.Max(0, currentTime);

        int minutes = Mathf.FloorToInt(displayTime / 60);
        int seconds = Mathf.FloorToInt(displayTime % 60);

        if (timerText != null)
        {
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogWarning("timerText not available during display update!");
        }
    }

    void OnTimeOut()
    {
        Debug.Log("Time's up - executing loss");

        if (player != null)
        {
            if (!player.IsDead)
            {
                player.ForceDie();
                Debug.Log("ForceDie called to force death");
            }
            else
            {
                Debug.Log("Player is already dead, no need to call ForceDie");
            }
        }
        else
        {
            Debug.LogWarning("Cannot apply loss as PlayerMovement is not assigned!");
        }
    }
}
