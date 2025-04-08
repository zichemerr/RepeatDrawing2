using UnityEngine;

public class AudioSystem
{
    private AudioSource _audioSource;

    public AudioSystem()
    {
        AudioSource prefab = Data.Prefabs.AudioContainer.AudioSource;

        _audioSource = GameObject.Instantiate(prefab);
    }
        
    public void Play(AudioSource audioSource, AudioClip clip, float volume = 1, bool loop = false)
    {
        audioSource.loop = loop;
        audioSource.PlayOneShot(clip, volume);
    }
        
    public void Play(AudioClip clip, float volume = 1, bool loop = false)
    {
        Play(_audioSource, clip, volume, loop);
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}