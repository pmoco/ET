using UnityEngine;
using UnityEngine.Audio;

public class SnapshotManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the AudioMixer
    public AudioMixerSnapshot[] snapshots; // Array of AudioMixerSnapshots
    public Collider triggerCollider; // Collider that triggers snapshot transitions

    private bool isPlayerInsideCollider = false; // Flag to track if the player is inside the collider

    // Method to transition to a specific snapshot
    public void TransitionToSnapshot(AudioMixerSnapshot snapshot, float transitionTime)
    {
        snapshot.TransitionTo(transitionTime); // Transition to the specified snapshot
    }

    // Method to transition to a snapshot by index
    public void TransitionToSnapshot(int snapshotIndex, float transitionTime)
    {
        if (snapshotIndex >= 0 && snapshotIndex < snapshots.Length)
        {
            snapshots[snapshotIndex].TransitionTo(transitionTime); // Transition to the snapshot at the specified index
        }
        else
        {
            Debug.LogWarning("Snapshot index out of range.");
        }
    }

    // Method to transition to a snapshot by name
    public void TransitionToSnapshot(string snapshotName, float transitionTime)
    {
        AudioMixerSnapshot snapshot = FindSnapshotByName(snapshotName);
        if (snapshot != null)
        {
            snapshot.TransitionTo(transitionTime); // Transition to the snapshot with the specified name
        }
        else
        {
            Debug.LogWarning("Snapshot not found: " + snapshotName);
        }
    }

    // Helper method to find a snapshot by name
    private AudioMixerSnapshot FindSnapshotByName(string snapshotName)
    {
        foreach (AudioMixerSnapshot snapshot in snapshots)
        {
            if (snapshot.name == snapshotName)
            {
                return snapshot;
            }
        }
        return null; // Snapshot not found
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is inside the collider
        if (isPlayerInsideCollider)
        {
            // Transition to a specific snapshot when the player is inside the collider
            TransitionToSnapshot("SnapshotInsideCollider", 0.1f);
        }
        else
        {
            // Transition to a different snapshot when the player is outside the collider
            TransitionToSnapshot("SnapshotOutsideCollider", 0.1f);
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider entered is the player or another GameObject
        if (other.CompareTag("Player"))
        {
            isPlayerInsideCollider = true; // Set flag to true when the player enters the collider
        }
    }

    // OnTriggerExit is called when the Collider other exits the trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the collider exited is the player or another GameObject
        if (other.CompareTag("Player"))
        {
            isPlayerInsideCollider = false; // Set flag to false when the player exits the collider
        }
    }
}
