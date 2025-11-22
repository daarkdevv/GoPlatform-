using UnityEngine;
using UnityEngine.UI;

public class AudioSliderManager : MonoBehaviour
{
    [Header("Sliders for volume control")]
    public Slider sfxSlider;
    public Slider musicSlider;

    public static AudioSliderManager Instance { get; private set; }

    private float sfxVolume = 1f;
    private float musicVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        sfxSlider = GameObject.Find("SfxSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener((value) =>
            {
                sfxVolume = value;
                AudioListener.volume = value;
            });
        }

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
            musicSlider.onValueChanged.AddListener((value) =>
            {
                musicVolume = value;
                AudioListener.volume = value;
            });
        }
    }

    void OnDestroy()
    {
        if (this != Instance) return;

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners();
        }
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
        }
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
