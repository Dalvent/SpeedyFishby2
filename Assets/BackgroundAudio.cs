using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    private Color originalColor;
    private float[] audioSampleBuffer = new float[1024];
    private Color targetColor;
    private Color currentColor;
    public float colorChangeSpeed = 1.0f;

    void Start()
    {
        if (spriteRenderer == null || audioSource == null)
        {
            Debug.LogError("SpriteRenderer or AudioSource not assigned.");
            return;
        }

        originalColor = spriteRenderer.color; // Save the original color
        currentColor = originalColor;
    }

    void Update()
    {
        if (spriteRenderer == null || audioSource == null)
        {
            return;
        }

        // Get the current volume level of the audio source
        audioSource.GetOutputData(audioSampleBuffer, 0);
        float currentVolume = GetCurrentRMSVolume(audioSampleBuffer);

        // Map the volume to a color offset range
        float colorOffset = Mathf.Lerp(-25f, 25f, currentVolume);

        // Adjust the original color by the color offset
        targetColor = originalColor + new Color(colorOffset, colorOffset, colorOffset, 0f) / 255f;
        currentColor = Color.Lerp(currentColor, targetColor, colorChangeSpeed * Time.deltaTime);
        spriteRenderer.color = currentColor;
    }

    private float GetCurrentRMSVolume(float[] audioSamples)
    {
        float sumOfSquares = 0f;
        for (int i = 0; i < audioSamples.Length; i++)
        {
            sumOfSquares += audioSamples[i] * audioSamples[i];
        }
        return Mathf.Sqrt(sumOfSquares / audioSamples.Length);
    }
}
