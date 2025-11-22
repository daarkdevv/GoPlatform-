using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum LoadType
    {
        ByName,
        ByIndex
    }

    [Header("Load Options")]
    [SerializeField] private LoadType loadType = LoadType.ByIndex;
    [SerializeField] private string sceneName;
    [SerializeField] private int sceneIndex = 0;

    [Header("Associated Lock Icon")]
    [SerializeField] private GameObject lockIcon;

    private bool isUnlocked = false;

    [ContextMenu("Reset Level Unlocks")]
    public void ResetLevelUnlocks()
    {
        PlayerPrefs.SetInt("HighestLevelUnlocked", 1);
        PlayerPrefs.Save();
        Debug.Log("Level locks reset - highest unlocked level is now 1");

        CheckUnlockStatus();
    }

    private void OnEnable()
    {
        CheckUnlockStatus();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckUnlockStatus();
    }

    private int GetBuildIndexFromSceneName(string name)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameOnly = System.IO.Path.GetFileNameWithoutExtension(path);
            if (sceneNameOnly == name)
                return i;
        }
        return -1;
    }

    private void CheckUnlockStatus()
    {
        int targetIndex = (loadType == LoadType.ByIndex) ? sceneIndex : GetBuildIndexFromSceneName(sceneName);

        if (targetIndex == -1)
        {
            Debug.LogError("⚠️ Scene name not found in Build Settings!");
            isUnlocked = false;
            return;
        }

        int unlocked = PlayerLevelTracker.Instance != null
            ? PlayerLevelTracker.Instance.highestLevelUnlocked
            : PlayerPrefs.GetInt("HighestLevelUnlocked", 1);

        isUnlocked = (targetIndex <= 1 || targetIndex <= unlocked);

        if (lockIcon != null)
        {
            if (isUnlocked)
            {
                Destroy(lockIcon);
            }
            else
            {
                lockIcon.SetActive(true);
            }
        }
    }

    public void LoadScene()
    {
        int targetIndex = (loadType == LoadType.ByIndex) ? sceneIndex : GetBuildIndexFromSceneName(sceneName);

        if (targetIndex == -1)
        {
            Debug.LogError("⚠️ Scene name not found in Build Settings!");
            return;
        }

        if (!isUnlocked)
        {
            Debug.LogWarning($"🚫 Cannot load level '{sceneName}' as it is not unlocked.");
            return;
        }

        if (loadType == LoadType.ByName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("⚠️ Please provide a valid scene name!");
            }
        }
        else if (loadType == LoadType.ByIndex)
        {
            if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.LogError("⚠️ Scene index out of range!");
            }
        }
    }
}
