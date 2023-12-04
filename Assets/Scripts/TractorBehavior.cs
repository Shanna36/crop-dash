using UnityEngine;
using System.Collections;

public class TractorBehavior : MonoBehaviour
{
    public float waitTime = 15f; // Time to wait before moving
    public float moveDistance = 200f; // Increased distance to move
    public float moveSpeed = 5f; // Speed at which the tractor will move
    private PlayerControllerSimplified playerController;

    private void Start()
    {
        // Find the PlayerControllerSimplified script on the player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerControllerSimplified>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitAndMove());
        }
    }

    private IEnumerator WaitAndMove()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Calculate the target position
        Vector3 targetPosition = transform.position - new Vector3(0, 0, moveDistance);

        // Move the tractor
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // Move a bit towards the target each frame
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Set the fillBar value to 0
        if (playerController != null && playerController.fillBar != null)
        {
            playerController.fillBar.value = 0;
        }
    }
}
