using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    [Header("The sound to play")]
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }
        else
        {
            Debug.LogWarning("Button not found on this object!");
        }
    }

    public void PlayClickSound()
    {
        SFXManager.Instance?.PlaySFX(clickSound);
    }
}
