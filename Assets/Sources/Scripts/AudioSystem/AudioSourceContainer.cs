using UnityEngine;

public class AudioSourceContainer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public AudioSource AudioSource => _audioSource;
}