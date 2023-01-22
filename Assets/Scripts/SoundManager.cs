using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class SoundManager
{
    private static AudioSource bgmAudioSource;
    private static AudioSource oneShotAudioSource;

    public static AudioSource BgmAudioSource { get => bgmAudioSource; set => bgmAudioSource = value; }
    public static AudioSource OneShotAudioSource { get => oneShotAudioSource; set => oneShotAudioSource = value; }

    // Plays the given sound once. Used for one time events like jumps and shots.
    public static void PlaySoundOnce(AudioClip sound)
    {
        PlaySoundOnce(sound, 1);
    }

    // volumeScale goes from 0 to 1
    public static void PlaySoundOnce(AudioClip sound, float volumeScale)
    {
        if (!oneShotAudioSource)
        {
            if(GameManager.Instance == null)
            {
                Debug.Log("GameManager fehlt");
                return;
            }
            oneShotAudioSource = GameManager.Instance.gameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(sound, volumeScale);
    }

    // volumeScale goes from 0 to 1
    public static void PlayBGM(AudioClip sound, float volumeScale)
    {
        if (!bgmAudioSource)
        {
            if (GameManager.Instance == null)
            {
                Debug.Log("GameManager fehlt");
                return;
            }
            bgmAudioSource = GameManager.Instance.gameObject.AddComponent<AudioSource>();
            bgmAudioSource.clip = sound;
            bgmAudioSource.loop = true;
        }
        bgmAudioSource.Play();
    }


    public static void UpdateVolumeBGM(float volume)
    {
        if (bgmAudioSource == null) return;
        bgmAudioSource.volume = volume;
    }

    public static void UpdateVolumeSFX(float volume)
    {
        if (oneShotAudioSource == null) return;
        oneShotAudioSource.volume = volume;
    }

}
