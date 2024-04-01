using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance
    
    public AudioSource soundEffectSource; // AudioSource component for sound effects
    public AudioSource musicSource; // AudioSource component for music

    public AudioClip cameraFlashClip;
    public AudioClip footstepsClip;

    private bool isMusicPlaying = false;

    void Awake()
    {
        // Ensure only one instance of the AudioManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Keep the AudioManager GameObject alive between scenes
        DontDestroyOnLoad(gameObject);
    }

    // Other methods for controlling audio playback would go here

   
}
