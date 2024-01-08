using UnityEngine;

namespace Code
{
    public static class AudioSourceExtensions
    {
        public static void PlayForDuration(this AudioSource audioSource, float duration)
        {
            if (audioSource == null || audioSource.clip == null || duration <= 0f)
            {
                Debug.LogError("Invalid parameters for PlayForDuration.");
                return;
            }

            // Calculate the pitch required to play the clip for the specified duration
            float originalClipLength = audioSource.clip.length;
            float pitch = originalClipLength / duration;

            // Set the clip and pitch
            audioSource.pitch = pitch;

            // Play the clip
            audioSource.Play();
        }
    }
}