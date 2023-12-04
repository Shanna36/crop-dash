using UnityEngine;
using System.Collections;

public class TractorBehavior : MonoBehaviour
{
    public float waitTime = 3f;
    public float moveDistance = 500f;
    public float moveSpeed = 5f;
    private PlayerControllerSimplified playerController;

    private void Start()
    {
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
    if (playerController != null && playerController.fillBar != null)
    {
        playerController.fillBar.value = 0;
    }
    yield return new WaitForSeconds(waitTime);

    Vector3 targetPosition = transform.position - new Vector3(0, 0, moveDistance);

    while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
    {
        // Move towards the target
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Adjust the Y position based on terrain height
        if (Terrain.activeTerrain != null)
        {
            // Ensure the position is in world coordinates
            Vector3 worldPosition = new Vector3(newPosition.x, 0, newPosition.z);
            float terrainHeight = Terrain.activeTerrain.SampleHeight(worldPosition);
            newPosition.y = terrainHeight + Terrain.activeTerrain.GetPosition().y; // Adding terrain's Y position
        }

        transform.position = newPosition;
        yield return null;
    }

    
}

}
