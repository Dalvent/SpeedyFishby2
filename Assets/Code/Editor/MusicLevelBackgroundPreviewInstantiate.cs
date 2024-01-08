using Code.Data;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public class MusicLevelBackgroundPreviewInstantiate : IMusicLevelPreviewInstantiate
    {
        public void Instantiate(Transform root, MusicLevel musicLevel)
        {
            Object.Instantiate(musicLevel.BackgroundPrefab, root);
        }
    }

    public class MusicLevelMusicPreviewInstantiate : IMusicLevelPreviewInstantiate
    {
        public AudioSource AudioSource { get; private set; }
        public void Instantiate(Transform root, MusicLevel musicLevel)
        {
            GameObject previewObject = new("Audio Preview", typeof(AudioSource));
            AudioSource = previewObject.GetComponent<AudioSource>();
            previewObject.hideFlags = HideFlags.HideAndDontSave;
        }
    }
}