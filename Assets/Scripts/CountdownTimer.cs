using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class CountdownTimer : MonoBehaviour
{
    public float timeLeft = 120f; // Duration in seconds
    public TextMeshProUGUI timerText; //assign in inspector
    private bool timerEnded = false; 

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (!timerEnded)
        {
            timeLeft = 0;
            timerEnded = true; // Prevent this block from being called repeatedly
            Debug.Log("Time is up!"); // Log message to the console

            if (timerText != null)
            {
                timerText.text = "Time's up!"; // Update the text to display "Time's up"
            }

            // Additional logic for when the timer ends can be added here
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft).ToString();
        }
    }
}

