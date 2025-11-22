using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerWin : MonoBehaviour
{
    public GameObject winScreen;
    public AudioClip clapSFX;
    public PlayerMovement playerMovement;
    public TimerUI timerUI;
    public TMP_Text starsText;
    public TMP_Text enemiesKilledText;
    public TMP_Text scoreText;
    public TMP_Text jumpTimesText;
    public Button nextLevelButton;
    public Button mainMenuButton;
    public string mainMenuScene = "MainMenu";
    public float animationDuration = 1f;
    public LeanTweenType easeType = LeanTweenType.easeOutQuad;

    private bool hasWon = false;
    private Vector2 initialPosition;

    void Start()
    {
        if (winScreen != null)
        {
            RectTransform rectTransform = winScreen.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                initialPosition = rectTransform.anchoredPosition;
            }

            winScreen.SetActive(false);
            Debug.Log("Win screen hidden at the start of the game");
        }

        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelClick);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuClick);
        }
    }

    public void TriggerWin()
    {
        if (!hasWon)
        {
            Debug.Log("Win triggered from flag!");

            if (winScreen != null)
            {
                RectTransform rectTransform = winScreen.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = initialPosition;
                }

                winScreen.SetActive(true);

                if (rectTransform != null)
                {
                    LeanTween.move(rectTransform, Vector2.zero, animationDuration)
                        .setEase(easeType)
                        .setOnComplete(() => Debug.Log("Win screen movement completed"));
                }
            }

            UpdateWinScreenText();

            if (clapSFX != null)
            {
                SFXManager.Instance.PlaySFX(clapSFX);
            }

            if (playerMovement != null)
            {
                playerMovement.isMoving = false;
                Debug.Log("Player movement stopped");
            }

            if (timerUI != null)
            {
                timerUI.isRunning = false;
                Debug.Log("Timer stopped");
            }

            hasWon = true;

            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerLevelTracker.Instance?.UnlockLevel(currentIndex + 1);
            int highestUnlocked = PlayerPrefs.GetInt("HighestLevelUnlocked", 1);
            if (currentIndex + 1 > highestUnlocked)
            {
                PlayerPrefs.SetInt("HighestLevelUnlocked", currentIndex + 1);
                PlayerPrefs.Save();
                Debug.Log("Next level unlocked and saved: " + (currentIndex + 1));
            }
        }
    }

    void UpdateWinScreenText()
    {
        if (starsText != null)
        {
            starsText.text = "STARS: " + PlayerStats.starsCollected.ToString();
        }

        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "ENEMIES KILLED: " + PlayerStats.enemiesKilled.ToString();
        }

        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + PlayerStats.score.ToString();
        }

        if (jumpTimesText != null)
        {
            jumpTimesText.text = "JUMP TIMES: " + PlayerStats.jumpCount.ToString();
        }
    }

    public void OnNextLevelClick()
    {
        Debug.Log("Next Level button clicked");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next level! Returning to main menu instead.");
            SceneManager.LoadScene(mainMenuScene);
        }
    }

    public void OnMainMenuClick()
    {
        Debug.Log("Main Menu button clicked");
        SceneManager.LoadScene(mainMenuScene);
    }
}
