using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [Tooltip("Duration of a full day in minutes")]
    public float dayLengthMinutes = 5f;
    
    [Range(0, 1)]
    [Tooltip("0 is midnight, 0.5 is solar noon")]
    public float timeOfDay = 0.25f; // Start at 6 AM

    [Header("Location: Trondheim, Norway")]
    [Tooltip("The max height of the sun in degrees (Approx 41° for late April)")]
    public float maxSolarAltitude = 41f;
    
    private float _rotationSpeed;

    void Update()
    {
        // Calculate how much timeOfDay increases per frame
        // (1 / (minutes * 60 seconds))
        _rotationSpeed = 1f / (dayLengthMinutes * 60f);
        timeOfDay += _rotationSpeed * Time.deltaTime;

        if (timeOfDay >= 1f) timeOfDay = 0f;

        UpdateSunRotation();
    }

    void UpdateSunRotation()
    {
        // 1. Calculate the rotation around the cycle (0 to 360 degrees)
        // We subtract 90 so that 0.5 (noon) is at the peak.
        var sunAngle = (timeOfDay * 360f) - 90f;

        // 2. Apply the inclination. 
        // Trondheim's sun doesn't go overhead (90°). 
        // We tilt the X rotation to match the max altitude and 
        // use the Y/Z to define the path.
        transform.localRotation = Quaternion.Euler(sunAngle, 180f, 0f);

        // Optional: Adjust tilt for Trondheim's latitude specifically
        // To get that low-hanging northern sun effect:
        var orbitTilt = new Vector3(sunAngle, -20f, 0f); 
        transform.localRotation = Quaternion.Euler(orbitTilt);
    }
}