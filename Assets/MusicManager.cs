using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks; // Array of music tracks to play
    private AudioSource audioSource; // Reference to the AudioSource component
    public int currentTrackIndex = 0; // Index of the currently playing track

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Play the first music track
        PlayMusicTrack(currentTrackIndex);
    }

    // Method to play a specific music track
    public void PlayMusicTrack(int trackIndex)
    {
        // Check if the track index is valid
        if (trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            // Stop the currently playing music track
            audioSource.Stop();

            // Assign the new music track to the AudioSource
            audioSource.clip = musicTracks[trackIndex];

            // Play the new music track
            audioSource.Play();

            // If the track is not looped, schedule the next track to start after the current one finishes
            if (!IsTrackLooped(trackIndex))
            {
                float delay = audioSource.clip.length; // Get the duration of the current track
                currentTrackIndex = (trackIndex + 1) % musicTracks.Length; // Move to the next track
                Invoke("PlayNextTrack", delay); // Schedule the next track to play after the delay
            }
        }
        else
        {
            Debug.LogWarning("Invalid track index: " + trackIndex);
        }
    }

    // Method to play the next music track (called after a delay)
    private void PlayNextTrack()
    {
        // Play the next music track
        PlayMusicTrack(currentTrackIndex);
    }

    // Method to check if a music track should be looped
    private bool IsTrackLooped(int trackIndex)
    {
        // Add logic here to determine if the track should be looped
        // For example, you can check if the track is an intro or a full song
        // You can use trackIndex or any other criteria to make this determination
        // For demonstration purposes, I'll assume the first two tracks are looped and the rest are not
        return trackIndex == 2 || trackIndex == 4;
    }

    // Method to stop the currently playing music track
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
