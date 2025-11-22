using UnityEngine;

public static class PlayerStats
{
    public static int starsCollected = 0;
    public static int enemiesKilled = 0;
    public static int jumpCount = 0;
    public static int score = 0;

    public static void AddStar()
    {
        starsCollected++;
        score += 100;
        Debug.Log("Star collected! Total: " + starsCollected + " | Score: " + score);

        UIManager.Instance?.UpdateStars(starsCollected);
        UIManager.Instance?.UpdateScore(score);
    }

    public static void AddKill()
    {
        enemiesKilled++;
        score += 500;
        Debug.Log("Enemy killed! Total: " + enemiesKilled + " | Score: " + score);

        UIManager.Instance?.UpdateScore(score);
    }

    public static void AddJump()
    {
        jumpCount++;
    }

    public static void ResetStats()
    {
        starsCollected = 0;
        enemiesKilled = 0;
        jumpCount = 0;
        score = 0;

        UIManager.Instance?.UpdateStars(starsCollected);
        UIManager.Instance?.UpdateScore(score);
    }
}
