﻿using System;
using System.Collections.Generic;
using Code.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public class MusicLevelEditorWindow : EditorWindow
    {
        private const int TextureWidth = 3768;
        private const int TextureHeight = 50;

        private MusicLevelPreviewRoot _preview;
        private MusicLevelMusicPreviewInstantiate _musicLevelMusicPreviewInstantiate;
        private readonly IMusicLevelPreviewInstantiate[] _previews;

        private MusicLevel _selectedMusicLevel;

        private GUIStyle _timeTextStyle;
        private Texture2D _waveformTexture;

        private Rect _timelineRect;
        private bool _isInitialized;

        public MusicLevelEditorWindow()
        {
            _musicLevelMusicPreviewInstantiate = new MusicLevelMusicPreviewInstantiate();
            _previews = new IMusicLevelPreviewInstantiate[]
            {
                new MusicLevelBackgroundPreviewInstantiate(),
                _musicLevelMusicPreviewInstantiate
            };
        }
        
        [MenuItem("Window/Music Level Editor Window")]
        public static void ShowWindow()
        {
            GetWindow<MusicLevelEditorWindow>("Music Level Editor Window");
        }
        
        void OnEnable()
        {
            if (_preview != null)
            {
                _preview.Enabled = true;
                _preview.SubscribeEditorEvents();
            }
        }
        
        void OnDisable()
        {
            if (_preview != null)
            {
                _preview.Enabled = false;
                _preview.UnsubscribeEditorEvents();
            }
        }
        
        private float _lastMusicTime;
        void Update()
        {
            if(_musicLevelMusicPreviewInstantiate.AudioSource.IsUnityNull())
                return;
            
            var time = _musicLevelMusicPreviewInstantiate.AudioSource.time;
            if (Math.Abs(_lastMusicTime - time) > 0.1f)
            {
                _lastMusicTime = time;
                Repaint();
            }
        }
        
         void OnGUI()
        {
            InitFields();
            
            LevelSelector();

            if(_musicLevelMusicPreviewInstantiate?.AudioSource == null || _musicLevelMusicPreviewInstantiate.AudioSource.IsUnityNull())
                return;

            MusicTimeline();
            TimelineCursor();
            MusicLevelCursor();
            
            MusicControllButtons();
            MusicTimeText();
            
            EventTimelineClicked();
        }

         private void InitFields()
        {
            if(_isInitialized)
                return;
            _isInitialized = true;
            
            _timeTextStyle = new(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontStyle = FontStyle.Bold
            };
        }

         private void EventTimelineClicked()
        {
            if (Event.current.type == EventType.MouseDown && _timelineRect.Contains(Event.current.mousePosition))
            {
                _musicLevelMusicPreviewInstantiate.AudioSource.time = (Event.current.mousePosition.x - _timelineRect.x) / _timelineRect.width * _musicLevelMusicPreviewInstantiate.AudioSource.clip.length;
                Event.current.Use();
            }
        }

         private void MusicTimeText()
        {
            string currentTimeText = $"{TimeSpan.FromSeconds(_musicLevelMusicPreviewInstantiate.AudioSource.time):mm\\:ss} / {TimeSpan.FromSeconds(_musicLevelMusicPreviewInstantiate.AudioSource.clip.length):mm\\:ss}";
            EditorGUI.LabelField(new Rect(_timelineRect.x, _timelineRect.yMax + 5, 100, 20), currentTimeText, _timeTextStyle);
        }

         private void MusicControllButtons()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(_musicLevelMusicPreviewInstantiate.AudioSource.isPlaying ? "Pause" : "Play", GUILayout.Width(70)) && !_musicLevelMusicPreviewInstantiate.AudioSource.IsUnityNull())
            {
                if (!_musicLevelMusicPreviewInstantiate.AudioSource.isPlaying)
                    _musicLevelMusicPreviewInstantiate.AudioSource.Play();
                else
                    _musicLevelMusicPreviewInstantiate.AudioSource.Pause();
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }

         private void MusicTimeline()
        {
            _timelineRect =  GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(50));
            GUI.DrawTexture(_timelineRect, _waveformTexture);
        }

         private void TimelineCursor()
        {
            float trackPosition = _musicLevelMusicPreviewInstantiate.AudioSource.time / _musicLevelMusicPreviewInstantiate.AudioSource.clip.length;
            float cursorPosition = _timelineRect.x + (_timelineRect.width * trackPosition);
            Rect cursorRect = new(cursorPosition - 2, _timelineRect.y, 4, _timelineRect.height);

            EditorGUI.DrawRect(cursorRect, Color.red);
        }

         private void MusicLevelCursor()
         {
             foreach (var timeEvent in _selectedMusicLevel.SpawnObstacleTimeEvents)
             {
                 float trackPosition = timeEvent.Time / _musicLevelMusicPreviewInstantiate.AudioSource.clip.length;
                 float cursorPosition = _timelineRect.x + (_timelineRect.width * trackPosition);
             
                 Rect cursorRect = new(cursorPosition - 2, _timelineRect.y, 2, _timelineRect.height);
                 EditorGUI.DrawRect(cursorRect, Color.cyan);
             }
         }

         private void LevelSelector()
        {
            // Allow the user to select an AudioSource
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select AudioSource:");
            MusicLevel selectedMusicLevel = (MusicLevel)EditorGUILayout.ObjectField(_selectedMusicLevel, typeof(MusicLevel), true);
            if (!selectedMusicLevel.IsUnityNull() && selectedMusicLevel != _selectedMusicLevel)
            {
                _selectedMusicLevel = selectedMusicLevel;
                if (_preview?.Enabled == true)
                    _preview.Enabled = false;
                
                if (!_selectedMusicLevel.IsUnityNull())
                {
                    if (_preview != null)
                        _preview.PreviewEnabled -= OnPreviewEnabledChanged;
                    
                    _preview = new MusicLevelPreviewRoot(_selectedMusicLevel, _previews);
                    _preview.PreviewEnabled += OnPreviewEnabledChanged;
                    _preview.Enabled = true;
                }
                
                _waveformTexture = GenerateWaveformTexture(_musicLevelMusicPreviewInstantiate.AudioSource.clip);
            }
            
            EditorGUILayout.EndHorizontal();
        }

         private void OnPreviewEnabledChanged(bool isEnabled)
         {
             if (isEnabled)
             {
                 _musicLevelMusicPreviewInstantiate.AudioSource.clip = _selectedMusicLevel.Music;   
             }
         }

         private Texture2D GenerateWaveformTexture(AudioClip clip) 
        {
            Texture2D texture = new Texture2D(TextureWidth, TextureHeight);
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            int stepSize = Mathf.Max(1, samples.Length / TextureWidth);

            for (int i = 0; i < TextureWidth; i++) 
            {
                float maxAmplitude = 0f;
                int sampleIndex = i * stepSize;

                for (int j = 0; j < stepSize && (sampleIndex + j) < samples.Length; j++) {
                    maxAmplitude = Mathf.Max(maxAmplitude, Mathf.Abs(samples[sampleIndex + j]));
                }

                DrawBlock(texture, i, maxAmplitude);
            }

            texture.Apply();
            return texture;
        }

        private void DrawBlock(Texture2D texture, int blockIndex, float amplitude) 
        {
            Color color = Color.white;
            for (int y = 0; y < texture.height; y++) {
                texture.SetPixel(blockIndex, y, y < amplitude * texture.height ? color : Color.clear);
            }
        }
    }
}