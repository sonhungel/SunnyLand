using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = .5f;
    [Range(0.5f, 1.5f)] public float pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void SetUpAudio()// setting volume and pitch
    {
        source.volume = volume;
        source.pitch = pitch;
    }
    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

    public void PlayeOnce()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    public void Stop()
    {
        source.Stop();
    }
}


