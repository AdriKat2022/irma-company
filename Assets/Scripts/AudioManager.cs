using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Make singleton

    public static AudioManager Instance;

    [Header("Musics")]
    public AudioClip menuMusic;
    public AudioClip gameThemeMusic;
    public AudioClip googleReviewMusic;

    [Header("Sound Effects")]
    public AudioClip cardHoverSound;
    public AudioClip soundClickSound;
    public AudioClip pauseClickSound;
    public AudioClip divinationBeginSound;

    private AudioSource musicSource;
    private AudioSource soundEffectSource;
    private AudioSource voiceSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("AudioManager: Il y a plus d'une instance de AudioManager dans la scène !");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlaySoundEffect(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            soundEffectSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("AudioManager: Aucun clip audio spécifié !");
        }
    }

    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioManager: Aucun clip audio spécifié !");
        }
    }

    public void PlayVoice(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            if (voiceSource.isPlaying) Debug.LogWarning("AudioManager: Un clip vocal est déjà en cours de lecture ! Lancement du fichier tout de même.");

            voiceSource.clip = clip;
            voiceSource.volume = volume;
            voiceSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioManager: Aucun clip audio spécifié !");
        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PauseVoice()
    {
        if (voiceSource.isPlaying)
        {
            voiceSource.Pause();
        }
    }
}
