using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform.
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Offset from the player's position.
    public float smoothSpeed = 10f; // Smoothing factor for camera movement.

    private Vector3 desiredPosition;

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position based on the player's position and the offset.
            desiredPosition = target.position + offset;

            // Smoothly move the camera to the desired position using SmoothDamp.
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.fixedDeltaTime);

            // Look at the player's position.
            transform.LookAt(target);
        }
    }

    private Vector3 velocity = Vector3.zero; // Velocity for SmoothDamp.

    // Call this method to reset camera position instantly (e.g., after a scene change).
    public void ResetCameraPosition()
    {
        if (target != null)
        {
            // Set the camera position to the desired position instantly.
            desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }
    }
}
