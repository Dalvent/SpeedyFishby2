using System;
using Code.Data;
using Code.Infrastructure;
using GD.MinMaxSlider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Events
{
    [Serializable]
    public class SpawnGhostTimeEvent : ITimeEvent
    {
        [SerializeField] private float _time;
        
        [MinMaxSlider(0,10)] 
        [SerializeField] private Vector2 _randomSpeedMultiplayer = Vector2.one;
        
        public float Time => _time;
        public GhostSpawnerData GhostSpawnerData; 
        
        public void Apply(TimeEventSlider slider)
        {
            var position = GeneratePosition(slider.SectoredGameFiled);
            slider.Factory.CreateGhost(position, GenerateSpeed(slider));
        }

        public float GenerateSpeed(TimeEventSlider slider)
        {
            if (_randomSpeedMultiplayer.x == 1 && _randomSpeedMultiplayer.y == 1)
                return slider.MusicLevel.StartBackgroundSpeed;
            
            var multiplayer = Random.Range(_randomSpeedMultiplayer.x, _randomSpeedMultiplayer.y);
            return slider.MusicLevelScroller.Speed * multiplayer;
        }

        
        private Vector2 GeneratePosition(ISectoredGameFiled sectoredGameFiled)
        {
            return new Vector2(-sectoredGameFiled.ForwardX + GhostSpawnerData.OffsetX, 0);
        }
    }
}