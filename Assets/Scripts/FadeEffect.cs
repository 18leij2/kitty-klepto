using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public float cycleSpeed = 1f; // Speed of alpha cycle
    public float minAlpha = 0.4f; // Minimum alpha value
    public float maxAlpha = 1f;   // Maximum alpha value

    private Image imageComponent;
    private TextMeshProUGUI textMeshPro;
    private float currentAlpha = 1f;
    private bool increasingAlpha = false;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        // Set initial alpha for both image and text
        SetAlpha(currentAlpha);
    }

    private void Update()
    {
        // Update alpha level
        UpdateAlpha();
    }

    private void UpdateAlpha()
    {
        // Update alpha value based on cycleSpeed
        if (increasingAlpha)
        {
            currentAlpha += cycleSpeed * Time.deltaTime;
            if (currentAlpha >= maxAlpha)
            {
                currentAlpha = maxAlpha;
                increasingAlpha = false;
            }
        }
        else
        {
            currentAlpha -= cycleSpeed * Time.deltaTime;
            if (currentAlpha <= minAlpha)
            {
                currentAlpha = minAlpha;
                increasingAlpha = true;
            }
        }

        // Update alpha for both image and text
        SetAlpha(currentAlpha);
    }

    private void SetAlpha(float alpha)
    {
        // Set alpha for image
        Color imageColor = imageComponent.color;
        imageColor.a = alpha;
        imageComponent.color = imageColor;

        // Set alpha for TMP text
        if (textMeshPro != null)
        {
            Color textColor = textMeshPro.color;
            textColor.a = alpha;
            textMeshPro.color = textColor;
        }
    }
}
