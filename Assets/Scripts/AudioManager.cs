using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayAudioClip(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioManager: Aucun clip audio spécifié !");
        }
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
