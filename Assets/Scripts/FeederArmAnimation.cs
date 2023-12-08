using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeederArmAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerControllerSimplified playerController; // Reference to the PlayerControllerSimplified script

    public GameObject combine; // Reference to the player GameObject

    private bool animationStarted = false; // Flag to check if the animation has started

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerControllerSimplified>();
        if (playerController == null)
        {
            Debug.LogError("PlayerControllerSimplified script not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Make the feeder arm follow the player's position
        transform.position = combine.transform.position;

        if (!animationStarted)
        {
            // Get the player's rotation
            Quaternion combineRotation = combine.transform.rotation;

        }

        // Check the isUnloading status from the player controller
        if (playerController != null && playerController.isUnloading && !animationStarted)
        {
            animator.SetTrigger("FeederArmRotate"); // Start the unloading animation
            animationStarted = true; // Set the flag to true as the animation has started
        }
    }
}
