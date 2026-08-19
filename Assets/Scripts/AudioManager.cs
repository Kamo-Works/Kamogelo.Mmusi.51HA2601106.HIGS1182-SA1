using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;   // Single shared reference so any script can play sounds easily

    public AudioSource musicSource;        // Dedicated source for looping background music
    public AudioSource sfxSource;          // Dedicated source for one-off sound effects

    public AudioClip backgroundMusic;
    public AudioClip pickupSound;
    public AudioClip shootSound;
    public AudioClip hitSound;

    void Start()
    {
        instance = this;

        // Start background music looping immediately when the game begins
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    void Update()
    {
    }

    // Plays a one-off sound effect without interrupting the music or any other sound currently playing
    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}