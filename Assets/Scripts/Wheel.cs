using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool powered = false;
    public float maxAngle = 90f;
    public float offset = 0f;

    private float turnAngle;
    private WheelCollider wcol;
    private Transform wmesh;

    private void Start()
    {
        wcol = GetComponentInChildren<WheelCollider>();
        wmesh = transform.Find("mesh_Wheel");

    if (wmesh == null)
    {
        Debug.LogError("Wheel mesh not found for " + gameObject.name);
    }
        
    }

    public void Steer(float steerInput)
    {
        turnAngle = steerInput * maxAngle + offset;
        wcol.steerAngle = turnAngle;
    }

    public void Accelerate(float powerInput)
    {
        if(powered) wcol.motorTorque = powerInput;
        else wcol.brakeTorque = 0;
    }

public void UpdatePosition()
{
    Vector3 pos;
    Quaternion rot;

    wcol.GetWorldPose(out pos, out rot);
    wmesh.transform.position = pos;

    // Apply rotation for steering (Y-axis) and rolling (Z-axis)
    wmesh.transform.rotation = Quaternion.Euler(wmesh.transform.rotation.eulerAngles.x, rot.eulerAngles.y, wmesh.transform.rotation.eulerAngles.z);
    
    Debug.Log("Wheel rotation being applied: " + wmesh.transform.rotation.eulerAngles);

}
}
