using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Get the sliders for each volume
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectsVolumeSlider;
    [SerializeField] private Slider voiceLinesVolumeSlider;

    [Space]

    [SerializeField] private AudioMixer audioMixer;

    // On awake, load all the settings (master, music, sfx, voiceLines, fullscreen) and disable the game object (the settings menu is never shown first)
    private void Awake()
    {
        LoadSettingsFromPlayerPrefs();
        gameObject.SetActive(false);
    }

    // The following functions set the volume of the audio mixer and save the corresponding value to player prefs
    public void SetMasterVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }
    }

    public void SetMusicVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetSoundEffectsVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat("SoundEffectsVolume", volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
        }
    }

    public void SetVoiceLinesVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat("VoiceLinesVolume", volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat("VoiceLinesVolume", volume);
        }
    }

    public void SetFullScreen(bool isFullScreen, bool saveToPlayerPrefs = true)
    {
        Screen.fullScreen = isFullScreen;
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        }
    }

    public void LoadSettingsFromPlayerPrefs()
    {
        // Load from player prefs
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);
        float soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 0);
        float voiceLinesVolume = PlayerPrefs.GetFloat("VoiceLinesVolume", 0);
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;

        // Set the sliders to the loaded values
        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        soundEffectsVolumeSlider.value = soundEffectsVolume;
        voiceLinesVolumeSlider.value = voiceLinesVolume;

        // Set the audio mixer to the loaded values
        SetMasterVolume(masterVolume, false);
        SetMusicVolume(musicVolume, false);
        SetSoundEffectsVolume(soundEffectsVolume, false);
        SetVoiceLinesVolume(voiceLinesVolume, false);
        SetFullScreen(isFullScreen, false);
    }
}
