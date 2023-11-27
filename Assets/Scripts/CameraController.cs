using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Camera sideViewCamera;
    public Camera topCamera;

    public GameObject controlPanel; 

    private Vector3 sideCameraOffset = new Vector3(19, 13, -22); // Offset for side camera
    private Vector3 topCameraOffset = new Vector3(10, 6, 0); // Offset for top camera
     private Quaternion topCameraRotationOffset = Quaternion.Euler(110, 0, 0); 
    void Update()
    {
        // Check which camera is active and set position and rotation accordingly
        if (sideViewCamera.enabled)
        {
            sideViewCamera.transform.position = player.transform.position + sideCameraOffset;
            // Additional rotation adjustments for side camera can be added here if needed
        }
        else if (topCamera.enabled)
        {
            topCamera.transform.position = player.transform.position + topCameraOffset;
            
            // Match top camera's rotation with player's rotation
             topCamera.transform.position = controlPanel.transform.position + topCameraOffset;
            topCamera.transform.rotation = controlPanel.transform.rotation * topCameraRotationOffset;
        }
    }
}
