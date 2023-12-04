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

        [System.Serializable]
         public class AxleInfo
    {   
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
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
        if (Input.GetKeyDown(switchKey))
        {
            sideViewCamera.enabled = !sideViewCamera.enabled;
            topCamera.enabled = !topCamera.enabled; 
        }
    //feeder arm animaiton, will return if I have time
         /*if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isUnloading = true;
        }*/
    }
    
    void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        foreach (AxleInfo axleInfo in axleInfos) 
        {
            if (axleInfo.steering) 
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) 
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }



   void OnTriggerEnter(Collider other)
    {
    
        if (other.CompareTag("Wheat") && fillBar.value < 600)
        {
            Destroy(other.gameObject);
            //slider logic for fill bar
             fillBar.value += 1;
        
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
}