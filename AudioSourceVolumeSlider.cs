using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AudioSourceVolumeSlider : MonoBehaviour
{
    public enum AudioType { Music, SFX }

    [Header("Audio type being controlled")]
    [SerializeField] private AudioType audioType;

    [Header("Slider controlling the audio")]
    [SerializeField] private Slider volumeSlider;

    private AudioSource targetAudioSource;
    private bool isInitialized = false;

    private void OnEnable()
    {
        if (!isInitialized)
        {
            SetupSlider();
            isInitialized = true;
        }
    }

    private void SetupSlider()
    {
        if (volumeSlider == null)
        {
            volumeSlider = GetComponent<Slider>();
            if (volumeSlider == null)
            {
                Debug.LogError("No Slider component found on this GameObject.");
                return;
            }
        }

        GameObject targetObject = null;

        switch (audioType)
        {
            case AudioType.Music:
                targetObject = FindInactiveObjectByTag("MusicPlayer");
                break;
            case AudioType.SFX:
                targetObject = FindInactiveObjectByTag("SFXPlayer");
                break;
        }

        if (targetObject == null)
        {
            Debug.LogError($"No GameObject found with the specified tag ({audioType}). Make sure it exists and has the correct tag.");
            return;
        }

        targetAudioSource = targetObject.GetComponent<AudioSource>();
        if (targetAudioSource == null)
        {
            Debug.LogError("The specified GameObject does not have an AudioSource component.");
            return;
        }

        volumeSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.value = targetAudioSource.volume;
        volumeSlider.onValueChanged.AddListener(UpdateAudioVolume);
    }

    private void UpdateAudioVolume(float value)
    {
        if (targetAudioSource != null)
        {
            targetAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    private GameObject FindInactiveObjectByTag(string tag)
    {
        return Resources
            .FindObjectsOfTypeAll<GameObject>()
            .FirstOrDefault(go => go.CompareTag(tag));
    }
}
