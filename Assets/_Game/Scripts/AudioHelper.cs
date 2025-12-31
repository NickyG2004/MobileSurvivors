using UnityEngine;

public static class AudioHelper
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        // create new audio source
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        // configure to be 2D
        audioSource.clip = clip;
        audioSource.volume = volume;

        // play the audio 
        audioSource.Play();

        // destroy when its done playing
        Object.Destroy(audioObject, clip.length);

        // retun it
        return audioSource;

    }
}
