using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Make singleton

    public static AudioManager Instance;

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundEffectSource;
    [SerializeField]
    private AudioSource voiceSource;

    [Header("Musics")]
    public AudioClip menuMusic;
    public AudioClip gameThemeMusic;
    public AudioClip googleReviewMusic;

    [Header("Sound Effects")]
    public AudioClip cardHoverSound;
    public AudioClip clickSound;
    public AudioClip pauseClickSound;
    public AudioClip divinationBeginSound;

    private AudioClip lastVoice;
    private bool isVoiceFinished = true;

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
            print("Playing " + clip.name);
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

            print("Playing " + clip.name);
            voiceSource.clip = clip;
            voiceSource.volume = volume;
            voiceSource.Play();
            lastVoice = clip;
            isVoiceFinished = false;
        }
        else
        {
            Debug.LogWarning("AudioManager: Aucun clip audio spécifié !");
        }
    }

    public void TooglePauseMusic(bool play)
    {
        if (!play) musicSource.Pause();
        else musicSource.Play();
    }

    public void TooglePauseVoice(bool play)
    {
        if (!play)
        {
            voiceSource.Pause();
        }
        else if (!isVoiceFinished)
        {
            voiceSource.Play();
        }
    }

    private IEnumerator StopVoice(AudioClip lastVoiceClip)
    {
        yield return new WaitForSeconds(lastVoiceClip.length);
        if (lastVoice == lastVoiceClip) isVoiceFinished = true;
    }
}
