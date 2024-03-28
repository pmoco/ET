using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player's transform
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    public Vector3 offset; // Offset of the camera from the player

    public float minX = -5f; // Minimum x position of the camera
    public float maxX = 5f; // Maximum x position of the camera
    public float minY = -5f; // Minimum y position of the camera
    public float maxY = 5f; // Maximum y position of the camera




// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Vector3 vec = target.position +  offset;

        this.transform.position = new Vector3 (vec.x, vec.y, this.transform.position.z) ;


    }



    void FixedUpdate()
    {
        if (target != null)
        {
            // Desired position for the camera
            Vector3 desiredPosition = target.position + offset;





            // Clamp camera's position within defined bounds
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
            desiredPosition = new Vector3(desiredPosition.x, desiredPosition.y, this.transform.position.z);

            // Smoothly move the camera towards the desired position

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            
            transform.position = smoothedPosition;


        }
    }
}