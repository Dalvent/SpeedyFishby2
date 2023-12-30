using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public class MusicPlayerWindow : EditorWindow
    {
        private AudioSource selectedAudioSource;
        private float trackPosition = 0f;

        private const float TimelineMargin = 10f;
        private GUIStyle timeTextStyle;

        private float sampleStep = 0.1f; // Step size in seconds
        private Texture2D waveformTexture;
        private const int TextureWidth = 3768; // Smaller texture width
        private const int TextureHeight = 50;  // Fixed texture height
    
        private TimestampedObjectList myData; // Assign this via the Unity Editor
        private SerializedObject serializedData;
        private SerializedProperty serializedList;
    
        [System.Serializable]
        public struct TimestampedObject
        {
            public float time;
            public ScriptableObject scriptableObject;
        }

        [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TimestampedObjectList", order = 1)]
        public class TimestampedObjectList : ScriptableObject
        {
            public List<TimestampedObject> items;
        }
    
    
        [MenuItem("Window/Music Player")]
        public static void ShowWindow()
        {
            GetWindow<MusicPlayerWindow>("Music Player");
        }

        private int d = 0;
        private Rect timelineRect;
        private string currentTimeText;
        private float lastTime;

        private void OnEnable()
        {
        }

        void Update()
        {
            var time = selectedAudioSource.time;
            if (Math.Abs(lastTime - time) > 0.1f)
            {
                lastTime = time;
                Repaint();
            }
        }
    
    
        void OnGUI()
        {
            // Allow the user to select an AudioSource
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select AudioSource:");
            selectedAudioSource = (AudioSource)EditorGUILayout.ObjectField(selectedAudioSource, typeof(AudioSource), true);
            EditorGUILayout.EndHorizontal();
            if(selectedAudioSource == null)
                return;

            if (timeTextStyle == null)
            {
                timeTextStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleLeft,
                    fontStyle = FontStyle.Bold
                };
            }
        
            if (selectedAudioSource != null && selectedAudioSource.clip != null) {
                // Generate waveform texture if necessary
                if (waveformTexture == null) {
                    waveformTexture = GenerateWaveformTexture(selectedAudioSource.clip);
                    return;
                }
            
                if (true) {
                    timelineRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(50));
                    GUI.DrawTexture(timelineRect, waveformTexture);
                
                    // Calculate the cursor position
                    trackPosition = selectedAudioSource.time / selectedAudioSource.clip.length;
                    float cursorPosition = timelineRect.x + (timelineRect.width * trackPosition);

                    // Draw the cursor
                    Rect cursorRect = new Rect(cursorPosition - 2, timelineRect.y, 4, timelineRect.height);
                    EditorGUI.DrawRect(cursorRect, Color.red);
                
                    // Play and Pause buttons
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(selectedAudioSource.isPlaying ? "Pause" : "Play", GUILayout.Width(70)) && selectedAudioSource != null) {
                        if (!selectedAudioSource.isPlaying)
                            selectedAudioSource.Play();
                        else
                            selectedAudioSource.Pause();
                    }
                
                    GUILayout.FlexibleSpace();
                
                    EditorGUILayout.EndHorizontal();
                
                    string currentTimeText = $"{TimeSpan.FromSeconds(selectedAudioSource.time):mm\\:ss} / {TimeSpan.FromSeconds(selectedAudioSource.clip.length):mm\\:ss}";
                    EditorGUI.LabelField(new Rect(timelineRect.x, timelineRect.yMax + 5, 100, 20), currentTimeText, timeTextStyle);
                }
            
            
                // Update AudioSource time if the timeline is clicked
                Event e = Event.current;
                if (e.type == EventType.MouseDown && timelineRect.Contains(e.mousePosition))
                {
                    selectedAudioSource.time = (e.mousePosition.x - timelineRect.x) / timelineRect.width * selectedAudioSource.clip.length;
                    e.Use();
                }
            }

        }

        Texture2D GenerateWaveformTexture(AudioClip clip) {
            Texture2D texture = new Texture2D(TextureWidth, TextureHeight);
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            // Calculate the step size based on the audio clip length and texture width
            int stepSize = Mathf.Max(1, samples.Length / TextureWidth);

            for (int i = 0; i < TextureWidth; i++) {
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

        void DrawBlock(Texture2D texture, int blockIndex, float amplitude) {
            Color color = Color.white;
            for (int y = 0; y < texture.height; y++) {
                texture.SetPixel(blockIndex, y, y < amplitude * texture.height ? color : Color.clear);
            }
        }
    }
}