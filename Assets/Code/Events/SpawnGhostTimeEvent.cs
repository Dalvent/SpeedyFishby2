using System;
using Code.Characters;
using Code.Data;
using Code.Infrastructure;
using GD.MinMaxSlider;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("GhostSpawnerData")] public GhostSpawner ghostSpawner; 
        
        public void Apply(TimeEventSlider slider)
        {
            var position = GeneratePosition(slider.SectoredGameFiled);
            float generateSpeed = GenerateSpeed(slider);
            
            var ghost = slider.Factory.CreateGhost(position);
            MoveTo moveTo = ghost.GetComponent<MoveTo>();
            moveTo.Target = slider.Factory.PlayerTransform;
            moveTo.Speed = generateSpeed;
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
            return new Vector2(-sectoredGameFiled.ForwardX, 0);
        }
    }
}