using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerSimplified : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public KeyCode switchKey;
    public Camera sideViewCamera;
    public Camera topCamera;

    private Rigidbody playerRb;
    public GameObject centerOfMass;

    public Slider fillBar;

    public bool isUnloading;

    public Vector3 centerOfMassOffset;

    public float steeringSpeed = 2.5f; 

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheelCollider;
        public WheelCollider rightWheelCollider;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
        public Transform leftWheelMesh;
        public Transform rightWheelMesh;

       
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.localPosition + centerOfMassOffset;
        isUnloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(switchKey))
        {
            sideViewCamera.enabled = !sideViewCamera.enabled;
            topCamera.enabled = !topCamera.enabled;
        }

         foreach (AxleInfo axleInfo in axleInfos) //moved to update to fix jitter
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheelCollider.steerAngle = steering;
                axleInfo.rightWheelCollider.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheelCollider.motorTorque = motor;
                axleInfo.rightWheelCollider.motorTorque = motor;
            }

            // Rotate the wheel meshes for the current axle
        RotateWheelMesh(axleInfo.leftWheelCollider, axleInfo.leftWheelMesh, axleInfo.steering);
        RotateWheelMesh(axleInfo.rightWheelCollider, axleInfo.rightWheelMesh, axleInfo.steering);
        }
    }
    
void RotateWheelMesh(WheelCollider wheelCollider, Transform wheelMesh, bool isSteering)
{
        Vector3 currentRotation = wheelMesh.localEulerAngles;

    if (isSteering)
    {
        // Apply steering rotation (around the Y-axis)
        float steerAngle = wheelCollider.steerAngle;
        steerAngle = Mathf.Clamp(steerAngle, -maxSteeringAngle, maxSteeringAngle);
        currentRotation.y = steerAngle;
    }
    else
    {
        // Fix the Z and Y rotation for back wheels to prevent jitter
        currentRotation.y = 0;
        currentRotation.z = 0;
    }

    // Apply rotation for wheel spinning (around the X-axis)
    float rotationAngle = wheelCollider.rpm / 60 * 360 * Time.deltaTime;
    currentRotation.x += rotationAngle;

    // Fix the rotation on the Z-axis to zero
    currentRotation.z = 0;

    wheelMesh.localEulerAngles = currentRotation;
}



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wheat") && fillBar.value < 300)
        {
            Destroy(other.gameObject);
            // slider logic for fill bar
            fillBar.value += 1;
        }

        if (other.CompareTag("Tractor_Trailer"))
        {
            isUnloading = true;
        }
    }

    void OnDrawGizmos()
    {
        if (!playerRb)
        {
            playerRb = GetComponent<Rigidbody>();
        }
        Gizmos.color = Color.red;
        // Draw a red sphere at the transformed center of mass
        Gizmos.DrawSphere(transform.position + transform.TransformPoint(playerRb.centerOfMass), 0.1f);
    }
}
