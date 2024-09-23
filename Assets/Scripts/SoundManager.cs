using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum Sound
{
    click,
    move,
    emote,
    damaged
}


public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    List<AudioSource> _soundsList = new List<AudioSource>();
    const float VOLUME = 0.1f;

    void Awake()
    {
        Initialize();
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        AudioClip click = Resources.Load<AudioClip>("Sound/Click");
        AudioClip move = Resources.Load<AudioClip>("Sound/Move");
        AudioClip emote = Resources.Load<AudioClip>("Sound/Emote");
        AudioClip damaged = Resources.Load<AudioClip>("Sound/Damaged");
        SetSoundsList(click);
        SetSoundsList(move);
        SetSoundsList(emote);
        SetSoundsList(damaged);
    }

    void SetSoundsList(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = VOLUME;
        audioSource.clip = clip;
        _soundsList.Add(audioSource);
    }



    public void SoundPlay(Sound e)
    {
        AudioSource sound = _soundsList[(int)e];
        sound.Play();
    }
}
