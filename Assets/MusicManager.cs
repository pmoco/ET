using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks; // Array of music tracks to play
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Play the first music track in a loop
        PlayMusicTrack(0, true);
    }

    // Method to play a specific music track
    public void PlayMusicTrack(int trackIndex, bool loop = false)
    {
        // Check if the track index is valid
        if (trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            // Stop the currently playing music track
            audioSource.Stop();

            // Assign the new music track to the AudioSource
            audioSource.clip = musicTracks[trackIndex];

            // Set loop mode
            audioSource.loop = loop;

            // Play the new music track
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid track index: " + trackIndex);
        }
    }

    // Method to stop the currently playing music track
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
