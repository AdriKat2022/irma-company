using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private const string MasterVolumeString = "MasterVolume";
    private const string MusicVolumeString = "MusicVolume";
    private const string SoundEffectsVolumeString = "SoundEffectsVolume";
    private const string VoiceLinesVolumeString = "VoiceLinesVolume";
    private const string FullScreenString = "FullScreen";

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
        audioMixer.SetFloat(MasterVolumeString, volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat(MasterVolumeString, volume);
        }
    }

    public void SetMusicVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat(MusicVolumeString, volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat(MusicVolumeString, volume);
        }
    }

    public void SetSoundEffectsVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat(SoundEffectsVolumeString, volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat(SoundEffectsVolumeString, volume);
        }
    }

    public void SetVoiceLinesVolume(float volume, bool saveToPlayerPrefs = true)
    {
        audioMixer.SetFloat(VoiceLinesVolumeString, volume);
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetFloat(VoiceLinesVolumeString, volume);
        }
    }

    public void SetFullScreen(bool isFullScreen, bool saveToPlayerPrefs = true)
    {
        Screen.fullScreen = isFullScreen;
        if (saveToPlayerPrefs)
        {
            PlayerPrefs.SetInt(FullScreenString, isFullScreen ? 1 : 0);
        }
    }

    public void LoadSettingsFromPlayerPrefs()
    {
        // Load from player prefs
        float masterVolume = PlayerPrefs.GetFloat(MasterVolumeString, 0);
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeString, 0);
        float soundEffectsVolume = PlayerPrefs.GetFloat(SoundEffectsVolumeString, 0);
        float voiceLinesVolume = PlayerPrefs.GetFloat(VoiceLinesVolumeString, 0);
        bool isFullScreen = PlayerPrefs.GetInt(FullScreenString, 1) == 1;

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
