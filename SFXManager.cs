using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = Mathf.Clamp01(volume);
    }

    public float GetVolume()
    {
        return sfxSource != null ? sfxSource.volume : 1f;
    }
}
