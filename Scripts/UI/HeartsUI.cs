using System.Collections.Generic;
using UnityEngine;

public class HeartsUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartContainer;

    private List<GameObject> hearts = new List<GameObject>();

    public void InitializeHearts(int totalLives)
    {
        ClearHearts();

        for (int i = 0; i < totalLives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(heart);
        }
    }

    public void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject lastHeart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(lastHeart);
        }
    }

    public void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, heartContainer);
        hearts.Add(heart);
    }

    public void ClearHearts()
    {
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();
    }
}
