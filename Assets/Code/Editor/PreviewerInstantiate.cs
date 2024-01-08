using System;
using System.Collections.Generic;
using Code.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Editor
{
    public interface IMusicLevelPreviewInstantiate
    {
        void Instantiate(Transform root, MusicLevel musicLevel);
    }
    
    public class MusicLevelPreviewRoot
    {
        public IMusicLevelPreviewInstantiate[] Previews { get; }
        private readonly MusicLevel _musicLevel;
        private bool _enabled;
        private GameObject _root;

        public event Action<bool> PreviewEnabled;
        
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if(_enabled == value)
                    return;
                
                _enabled = value;
                if (value)
                    Enable();
                else
                    Disable();
            }
        }

        public MusicLevelPreviewRoot(MusicLevel musicLevel, IMusicLevelPreviewInstantiate[] previews)
        {
            Previews = previews;
            _musicLevel = musicLevel;
        }

        public void SubscribeEditorEvents()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        
        public void UnsubscribeEditorEvents()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void Enable()
        {
           CreateRoot();
            PreviewEnabled?.Invoke(true);
        }
        
        private void Disable()
        {
            DestroyRoot();
            PreviewEnabled?.Invoke(false);
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                CreateRoot();
            }
            else if (state == PlayModeStateChange.EnteredPlayMode)
            {
                DestroyRoot();
            }
        }

        private void CreateRoot()
        {
            if(!_root.IsUnityNull())
                return;
            _root = new GameObject("PreviewRoot");
            foreach (var preview in Previews)
            {
                preview.Instantiate(_root.transform, _musicLevel);
            }
            _root.hideFlags = HideFlags.HideAndDontSave;
        }

        private void DestroyRoot()
        {
            if(_root.IsUnityNull())
                return;
            Object.DestroyImmediate(_root);
        }
    }
}