using System;
using System.Collections;
using CMSR.EnemySystem;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicClip[] musicClip;

    private QueueObj<MusicClip> _queue;
    private float _time;
    
    private void Start()
    {
        _queue = new QueueObj<MusicClip>(musicClip);
        
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        while (true)
        {
            _time -= Time.deltaTime;
            
            if (_time < 0)
            {
                MusicClip musicClip = _queue.Get();
                audioSource.clip = musicClip.audioClip;
                audioSource.Play();
                _time = musicClip.time;
            }
            
            yield return null;
        }
    }
}

[Serializable]
public class MusicClip
{
    public AudioClip audioClip;
    public float time;
}
