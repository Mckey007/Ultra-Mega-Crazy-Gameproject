using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static GameObject oneShotSoundPlayer;
    private static AudioSource oneShotAudioSource;

    private static Dictionary<AudioClip, float> timeOfAudioClip;
    private static Dictionary<AudioClip, float> delayOfAudioClip;

    // Plays the given sound once. Used for one time events like jumps and shots.
    public static void PlaySoundOnce(AudioClip sound)
    {
        if(!oneShotAudioSource)
        {
            oneShotSoundPlayer = new GameObject("SoundPlayer");
            oneShotAudioSource = oneShotSoundPlayer.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(sound);
    }
}
