using System;
using System.Collections;
using System.Linq;
using Code.Data;
using Code.Events;
using Code.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class MusicLevelScroller : MonoBehaviour
    {
        private const float BackgroundScrollSpeedFactor = 0.75f;
        private TimeEventSlider _slider;
        private float _passedTime;

        public AudioSource MusicAudioSource;
        private BackgroundScroller _backgroundScroller;

        public float Speed { get; private set; }

        public void Construct(IGameFactory gameFactory, ISectoredGameFiled sectoredGameFiled, MusicLevel musicLevel, BackgroundScroller backgroundScroller)
        {
            _backgroundScroller = backgroundScroller;
            _slider = new TimeEventSlider(this, musicLevel, gameFactory, sectoredGameFiled);

            MusicAudioSource.clip = musicLevel.Music;
            MusicAudioSource.Play();
            
            SetSpeed(musicLevel.StartBackgroundSpeed);
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
            
            _backgroundScroller.Speed = Speed * BackgroundScrollSpeedFactor;
        }

        private void Update()
        {
            _passedTime += Time.deltaTime;
            _slider.OnUpdate(_passedTime);
        }
    }
}