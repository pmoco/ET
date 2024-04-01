using UnityEngine;
using UnityEngine.Audio;

public class ReverbZone : MonoBehaviour
{
    public AudioMixerSnapshot roomSnapshot; // Snapshot for the "Room" reverb effect
    public AudioMixerSnapshot outsideSnapshot; // Snapshot for the "Outside" reverb effect

    public Collider roomCollider; // Collider for the "Room" zone
    public Collider outsideCollider; // Collider for the "Outside" zone

    // OnTriggerEnter is called when the Collider other enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider entered is the player or another GameObject
        if (other.CompareTag("Player"))
        {
            // Check which collider was triggered
            if (other == roomCollider)
            {
                // Transition to the "Room" snapshot when the player enters the room zone
                roomSnapshot.TransitionTo(0.1f);
            }
            else if (other == outsideCollider)
            {
                // Transition to the "Outside" snapshot when the player enters the outside zone
                outsideSnapshot.TransitionTo(0.1f);
            }
        }
    }

    // OnTriggerExit is called when the Collider other exits the trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the collider exited is the player or another GameObject
        if (other.CompareTag("Player"))
        {
            // Check which collider was triggered
            if (other == roomCollider)
            {
                // Transition to the "Outside" snapshot when the player exits the room zone
                outsideSnapshot.TransitionTo(0.1f);
            }
            else if (other == outsideCollider)
            {
                // Transition to the "Room" snapshot when the player exits the outside zone
                roomSnapshot.TransitionTo(0.1f);
            }
        }
    }
}
