using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof (Light2D))]
public class DayNightSystem : MonoBehaviour
{
    public float sunIntensityChangeFactor = 0.01f;
    public float minSunIntensity = 0.5f;
    public float maxSunIntensity = 1.5f;

    private TimeSystem timeSystem;
    private Light2D sun;

    private System.DateTime previousDateTime;

    public void Start()
    {
        timeSystem = FindObjectOfType<TimeSystem>();
        sun = GetComponent<Light2D>();
        previousDateTime = timeSystem.dateTime;
    }

    public void Update()
    {
        int hour = timeSystem.dateTime.Hour;
        int minute = timeSystem.dateTime.Minute;

        if (minute != previousDateTime.Minute)
        {
            float sunIntensity = sun.intensity;

            if (hour < 12)
            {
                sunIntensity += sunIntensityChangeFactor;
            }
            else
            {
                sunIntensity -= sunIntensityChangeFactor;
            }

            sun.intensity = Mathf.Clamp(sunIntensity, minSunIntensity, maxSunIntensity);
        }

        previousDateTime = timeSystem.dateTime;
    }
}
