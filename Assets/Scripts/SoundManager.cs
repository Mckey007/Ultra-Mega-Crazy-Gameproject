using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static GameObject oneShotSoundPlayer;
    private static AudioSource oneShotAudioSource;

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
            oneShotSoundPlayer = new GameObject("SoundPlayer");
            oneShotAudioSource = oneShotSoundPlayer.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(sound, volumeScale);
    }
}
