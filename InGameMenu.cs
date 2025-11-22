using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuScreen;
    public Button openMenuButton;
    public Button continueButton;
    public Button mainMenuButton;
    public Button exitButton;
    public Slider volumeSlider;
    public string mainMenuScene = "MainMenu";
    public float animationDuration = 1f;
    public LeanTweenType easeType = LeanTweenType.easeOutQuad;
    public AudioSource audioSource;
    public AudioClip buttonClickSFX;
    public float exitPositionY = 1000f;

    private Vector2 initialPosition;
    private bool isMenuOpen = false;
    private RectTransform rectTransform;
    private FloatingButton[] floatingButtons;

    void Start()
    {
        floatingButtons = FindObjectsOfType<FloatingButton>();
        Debug.Log("Found " + floatingButtons.Length + " FloatingButton objects");

        if (menuScreen != null)
        {
            rectTransform = menuScreen.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                initialPosition = rectTransform.anchoredPosition;
            }
            else
            {
                Debug.LogWarning("RectTransform not found on menuScreen!");
            }

            menuScreen.SetActive(false);
            Debug.Log("Menu screen hidden at the start of the game");
        }
        else
        {
            Debug.LogWarning("menuScreen not assigned!");
        }

        if (openMenuButton != null)
        {
            openMenuButton.onClick.AddListener(ToggleMenu);
        }
        else
        {
            Debug.LogWarning("openMenuButton not assigned!");
        }

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClick);
        }
        else
        {
            Debug.LogWarning("continueButton not assigned!");
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuClick);
        }
        else
        {
            Debug.LogWarning("mainMenuButton not assigned!");
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitClick);
        }
        else
        {
            Debug.LogWarning("exitButton not assigned!");
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(AdjustVolume);
        }
        else
        {
            Debug.LogWarning("volumeSlider not assigned!");
        }
    }

    void ToggleMenu()
    {
        if (isMenuOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    void OpenMenu()
    {
        if (menuScreen != null)
        {
            if (rectTransform != null)
            {
                LeanTween.cancel(rectTransform);
                rectTransform.anchoredPosition = initialPosition;
                Debug.Log("Menu screen position reset to: " + initialPosition);
            }

            menuScreen.SetActive(true);
            Debug.Log("Menu screen activated");

            if (rectTransform != null)
            {
                LeanTween.move(rectTransform, Vector2.zero, animationDuration)
                    .setEase(easeType)
                    .setOnComplete(() => Debug.Log("Menu screen moved to center"));
            }
            else
            {
                Debug.LogWarning("RectTransform not found on menuScreen!");
            }

            Time.timeScale = 0f;
            isMenuOpen = true;

            foreach (FloatingButton floatingButton in floatingButtons)
            {
                if (floatingButton != null && floatingButton.gameObject.layer != 5)
                {
                    floatingButton.SetPaused(true);
                }
            }

            Debug.Log("Menu opened, game paused, and FloatingButton movement paused");
        }
        else
        {
            Debug.LogWarning("menuScreen not assigned!");
        }
    }

    void CloseMenu()
    {
        if (menuScreen != null)
        {
            if (rectTransform != null)
            {
                LeanTween.cancel(rectTransform);

                LeanTween.move(rectTransform, new Vector2(0, exitPositionY), animationDuration)
                    .setEase(easeType)
                    .setOnComplete(() =>
                    {
                        menuScreen.SetActive(false);
                        Debug.Log("Menu screen hidden after moving up");
                    });
            }
            else
            {
                menuScreen.SetActive(false);
                Debug.Log("menuScreen hidden directly (no RectTransform found)");
            }

            Time.timeScale = 1f;
            isMenuOpen = false;

            foreach (FloatingButton floatingButton in floatingButtons)
            {
                if (floatingButton != null)
                {
                    floatingButton.SetPaused(false);
                }
            }

            Debug.Log("Menu closed, game resumed, and FloatingButton movement resumed");
        }
    }

    void OnContinueClick()
    {
        PlayButtonSound();
        CloseMenu();
    }

    void OnMainMenuClick()
    {
        PlayButtonSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnExitClick()
    {
        PlayButtonSound();
        Debug.Log("Exit button clicked - quitting the game");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("Volume adjusted to: " + volume);
    }

    void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);
        }
        else
        {
            Debug.LogWarning("AudioSource or buttonClickSFX not assigned!");
        }
    }
}
