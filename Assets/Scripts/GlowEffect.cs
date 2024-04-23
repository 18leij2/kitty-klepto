using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public bool active = true;
    public float minIntensity = 0f; // Minimum intensity value
    public float maxIntensity = 5f; // Maximum intensity value
    public float variationSpeed = 1f; // Speed at which intensity varies
    private float currentIntensity; // Current intensity value
    private float direction = 1f; // Direction of variation (-1 for decreasing, 1 for increasing)
    private Light pointLight; // Reference to the Point Light component

    void Start()
    {
        pointLight = GetComponent<Light>(); // Getting reference to the Point Light component
        currentIntensity = minIntensity; // Starting intensity
    }

    void Update()
    {
        if (active)
        {
            // Increment or decrement intensity based on time and variation speed
            currentIntensity += direction * variationSpeed * Time.deltaTime;

            // Change direction when reaching the min or max intensity
            if (currentIntensity >= maxIntensity)
            {
                currentIntensity = maxIntensity;
                direction = -1f; // Start decreasing intensity
            }
            else if (currentIntensity <= minIntensity)
            {
                currentIntensity = minIntensity;
                direction = 1f; // Start increasing intensity
            }

            // Set the point light intensity
            pointLight.intensity = currentIntensity;
        }
        else
        {
            pointLight.intensity = 0f;
        }
        

        
    }
}
