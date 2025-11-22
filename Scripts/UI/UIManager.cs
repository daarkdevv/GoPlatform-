using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI starsText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Instance.UpdateScore(PlayerStats.score);
        Instance.UpdateStars(PlayerStats.starsCollected);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString("D6");
    }

    public void UpdateStars(int newStars)
    {
        starsText.text = newStars.ToString();
    }
}
