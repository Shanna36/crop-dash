using UnityEngine;
using UnityEngine.UI;

public class TimerToggle : MonoBehaviour
{
    public CountdownTimer countdownTimer; // Reference to your CountdownTimer script
    public Toggle toggle;

    private void Start()
    {
        // Subscribe to the toggle's onValueChanged event
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // When the toggle value changes, enable or disable the timer accordingly
        if (countdownTimer != null)
        {
            countdownTimer.enabled = isOn;
        }
    }
}
