using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    // PlayerPrefs and Mixer keys
    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SoundEffectsVolume";
    private const string VoiceVolumeKey = "VoiceLinesVolume";
    private const string FullscreenKey = "Fullscreen";

    private void Start()
    {
        // Load settings
        masterSlider.value = PlayerPrefs.GetFloat(MasterVolumeKey, 0.75f); // Default value 0.75
        musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey, 0.75f);
        voiceSlider.value = PlayerPrefs.GetFloat(VoiceVolumeKey, 0.75f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt(FullscreenKey, 1) == 1; // Default fullscreen enabled

        // Apply loaded settings
        ApplySettings();

        // Add listeners
        masterSlider.onValueChanged.AddListener(value => SetVolume(MasterVolumeKey, value));
        musicSlider.onValueChanged.AddListener(value => SetVolume(MusicVolumeKey, value));
        sfxSlider.onValueChanged.AddListener(value => SetVolume(SFXVolumeKey, value));
        voiceSlider.onValueChanged.AddListener(value => SetVolume(VoiceVolumeKey, value));
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        gameObject.SetActive(false);
    }

    private void SetVolume(string parameterName, float value)
    {
        // Safeguard for very small or zero values
        if (value <= 0.001f)
        {
            audioMixer.SetFloat(parameterName, -80f);
        }
        else
        {
            audioMixer.SetFloat(parameterName, Mathf.Log10(value) * 20);
        }
        SaveSettings();
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, masterSlider.value);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxSlider.value);
        PlayerPrefs.SetFloat(VoiceVolumeKey, voiceSlider.value);
        PlayerPrefs.Save();
    }

    private void ApplySettings()
    {
        audioMixer.SetFloat(MasterVolumeKey, Mathf.Log10(masterSlider.value) * 20);
        audioMixer.SetFloat(MusicVolumeKey, Mathf.Log10(musicSlider.value) * 20);
        audioMixer.SetFloat(SFXVolumeKey, Mathf.Log10(sfxSlider.value) * 20);
        audioMixer.SetFloat(VoiceVolumeKey, Mathf.Log10(voiceSlider.value) * 20);
        Screen.fullScreen = fullscreenToggle.isOn;
    }
}
