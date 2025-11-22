using UnityEngine;

public class PlayerLevelTracker : MonoBehaviour
{
    public static PlayerLevelTracker Instance;

    public int highestLevelUnlocked = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        highestLevelUnlocked = PlayerPrefs.GetInt("HighestLevelUnlocked", 1);
    }

    public void UnlockLevel(int index)
    {
        if (index > highestLevelUnlocked)
        {
            highestLevelUnlocked = index;
            PlayerPrefs.SetInt("HighestLevelUnlocked", highestLevelUnlocked);
            PlayerPrefs.Save();
            Debug.Log("✅ New level unlocked: " + index);
        }
    }

    [ContextMenu("Reset Unlocks")]
    public void ResetUnlocks()
    {
        highestLevelUnlocked = 1;
        PlayerPrefs.SetInt("HighestLevelUnlocked", 1);
        PlayerPrefs.Save();
    }
}
using UnityEngine;

public class PlayerLevelTracker : MonoBehaviour
{
    public static PlayerLevelTracker Instance;

    public int highestLevelUnlocked = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        highestLevelUnlocked = PlayerPrefs.GetInt("HighestLevelUnlocked", 1);
    }

    public void UnlockLevel(int index)
    {
        if (index > highestLevelUnlocked)
        {
            highestLevelUnlocked = index;
            PlayerPrefs.SetInt("HighestLevelUnlocked", highestLevelUnlocked);
            PlayerPrefs.Save();
            Debug.Log("✅ New level unlocked: " + index);
        }
    }

    [ContextMenu("Reset Unlocks")]
    public void ResetUnlocks()
    {
        highestLevelUnlocked = 1;
        PlayerPrefs.SetInt("HighestLevelUnlocked", 1);
        PlayerPrefs.Save();
    }
}
using UnityEngine;

public class PlayerLevelTracker : MonoBehaviour
{
    public static PlayerLevelTracker Instance;

    public int highestLevelUnlocked = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        highestLevelUnlocked = PlayerPrefs.GetInt("HighestLevelUnlocked", 1);
    }

    public void UnlockLevel(int index)
    {
        if (index > highestLevelUnlocked)
        {
            highestLevelUnlocked = index;
            PlayerPrefs.SetInt("HighestLevelUnlocked", highestLevelUnlocked);
            PlayerPrefs.Save();
            Debug.Log(" New level unlocked: " + index);
        }
    }

    [ContextMenu("Reset Unlocks")]
    public void ResetUnlocks()
    {
        highestLevelUnlocked = 1;
        PlayerPrefs.SetInt("HighestLevelUnlocked", 1);
        PlayerPrefs.Save();
    }
}
