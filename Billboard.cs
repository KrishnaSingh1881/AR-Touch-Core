using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        // Find the main camera once and store it for efficiency.
        cameraTransform = Camera.main.transform;
    }

    // LateUpdate runs after all other Update calls, which is ideal for camera-facing logic.
    // This prevents jittery movement.
    void LateUpdate()
    {
        // Make this object's forward direction point towards the camera.
        transform.LookAt(cameraTransform);
    }
}