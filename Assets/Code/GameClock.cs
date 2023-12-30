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
    public class GameClock : MonoBehaviour
    {
        private TimeEventSlider _slider;
        private float _passedTime;

        public AudioSource MusicAudioSource;
        public MusicLevel MusicLevel;

        public void Construct(IGameFactory gameFactory, ISectoredGameFiled sectoredGameFiled)
        {
            MusicAudioSource.clip = MusicLevel.Music;
            MusicAudioSource.Play();
            _slider = new TimeEventSlider(MusicLevel, gameFactory, sectoredGameFiled);
        }
        
        private void Update()
        {
            _passedTime += Time.deltaTime;
            _slider.OnUpdate(_passedTime);
        }
    }
}