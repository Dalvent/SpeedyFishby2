using GD.MinMaxSlider;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AudioReactSaturation : MonoBehaviour
{
    public AudioSource audioSource;
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    private float[] audioSampleBuffer = new float[1024];
    private float originalIntensity;
    [MinMaxSlider(-5f, 5f)] 
    public Vector2 IntensityRange = new(-0.25f, 0.25f);
    
    void Start()
    {
        if (audioSource == null || globalVolume == null)
        {
            Debug.LogError("AudioSource or Global Volume not assigned.");
            return;
        }

        // Get the Color Adjustments from the Volume Profile
        if (!globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            Debug.LogError("Color Adjustments not found in the Global Volume Profile.");
            return;
        }

        // Store the original intensity value
        originalIntensity = colorAdjustments.postExposure.value;
    }

    void Update()
    {
        if (audioSource == null || colorAdjustments == null)
        {
            return;
        }

        // Get the current volume level of the audio source
        audioSource.GetOutputData(audioSampleBuffer, 0);
        float currentVolume = GetCurrentRMSVolume(audioSampleBuffer);

        // Map the volume to the intensity range (-0.25 to +0.25 from original value)
        float intensityOffset = Mathf.Lerp(IntensityRange.x, IntensityRange.y, currentVolume);
        float newIntensity = Mathf.Clamp(originalIntensity + intensityOffset, 0f, 1f);
        colorAdjustments.postExposure.Override(newIntensity);
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