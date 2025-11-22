using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text starsText;
    public TMP_Text enemiesKilledText;
    public TMP_Text scoreText;
    public TMP_Text jumpTimesText;
    public float animationDuration = 1f;

    private Vector2 initialPosition;
    private Vector2 targetPosition;

    void Start()
    {
        gameObject.SetActive(false);
        initialPosition = GetComponent<RectTransform>().anchoredPosition;
        targetPosition = Vector2.zero;
    }

    public void ShowGameOver()
    {
        gameObject.SetActive(true);

        starsText.text = "STARS: " + PlayerStats.starsCollected.ToString();
        enemiesKilledText.text = "ENEMIES KILLED: " + PlayerStats.enemiesKilled.ToString();
        scoreText.text = "SCORE: " + PlayerStats.score.ToString();
        jumpTimesText.text = "JUMP TIMES: " + PlayerStats.jumpCount.ToString();

        GetComponent<RectTransform>().anchoredPosition = initialPosition;

        LeanTween.move(GetComponent<RectTransform>(), targetPosition, animationDuration)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void RestartGame()
    {
        PlayerStats.ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        PlayerStats.ResetStats();
        SceneManager.LoadScene("MainMenu");
    }
}
