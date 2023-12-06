using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederArmAnimation : MonoBehaviour
{
    private Animator animator; 
    public bool isUnloading;

    public GameObject Tractor; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isUnloading = false; 
    }

    // This method is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUnloading = true; 
            animator.SetTrigger("FeederArmRotate"); // Start the unloading animation
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can put any code here that needs to be checked or updated every frame
    }
}