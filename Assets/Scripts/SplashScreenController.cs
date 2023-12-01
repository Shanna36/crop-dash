using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    // You can set the name of your main game scene in the Inspector
    public string mainScene = "Tractor Whiteboxing Scene";

    private void Update()
    {
        // Check for a click or tap
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Load the main game scene
            SceneManager.LoadScene(mainScene);
        }
    }
}