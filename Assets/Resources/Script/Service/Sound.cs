using UnityEngine;
using System.Collections;

public class Sound
{
    public static void PlayOneShot(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public static void PlayOneShot(AudioSource audioSource, AudioClip[] audioClip)
    {
        audioSource.PlayOneShot(audioClip[Random.Range(0, audioClip.Length - 1)]);
    }

}
