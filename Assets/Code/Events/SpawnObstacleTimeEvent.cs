using System;
using System.Buffers;
using Code.Data;
using Code.Infrastructure;
using Code.Tools;
using GD.MinMaxSlider;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.Events
{
    public enum RandomLocation
    {
        All,
        TopBottom,
        Center,
        OnlyTop,
        OnlyBottom
    }


    public abstract class SpawnObstacleTimeEvent : ITimeEvent
    {
        [SerializeField] private float _time;
        
        [MinMaxSlider(0,10)] 
        [SerializeField] private Vector2 _randomSpeedMultiplayer = Vector2.one;
    
        public ObstacleSpawnData ObstacleSpawnData;
        public RandomLocation RandomLocation ;
        public float Time => _time;
        
        public void Apply(TimeEventSlider slider)
        {
            var spawned = Spawn(slider);
            spawned.Speed = GenerateSpeed(slider);
            spawned.transform.position = GeneratePosition(slider.SectoredGameFiled);
        }

        protected abstract ForwardObstacle Spawn(TimeEventSlider slider);
        
        private float GenerateSpeed(TimeEventSlider slider)
        {
            if (_randomSpeedMultiplayer.x == 1 && _randomSpeedMultiplayer.y == 1)
                return slider.MusicLevel.StartBackgroundSpeed;
            
            var multiplayer = Random.Range(_randomSpeedMultiplayer.x, _randomSpeedMultiplayer.y);
            return slider.MusicLevelScroller.Speed * multiplayer;
        }
        
        private Vector2 GeneratePosition(ISectoredGameFiled sectoredGameFiled)
        {
            (float from, float to) = RandomSectorRange(GetSectorsByLocation(sectoredGameFiled));
            return new Vector2(sectoredGameFiled.ForwardX + ObstacleSpawnData.OffsetX, Random.Range(from, to));
        }

        private (float from, float to) RandomSectorRange((float from, float to)[] ranges)
        {
            if(ranges.Length == 1)
                return ranges[0];

            return ranges[Random.Range(0, ranges.Length)];
        }

        private (float from, float to)[] GetSectorsByLocation(ISectoredGameFiled sectoredGameFiled)
        {
            switch (RandomLocation)
            {
                case RandomLocation.All:
                    return new [] { sectoredGameFiled.GetSectorRange(0, 9) };
                case RandomLocation.TopBottom:
                    return new [] { sectoredGameFiled.GetTopSector(), sectoredGameFiled.GetBottomSector() };
                case RandomLocation.Center:
                    return new [] { sectoredGameFiled.GetCenterSector() };
                case RandomLocation.OnlyTop:
                    return new [] { sectoredGameFiled.GetTopSector() };
                case RandomLocation.OnlyBottom:
                    return new [] { sectoredGameFiled.GetBottomSector() };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class SpawnBonusTimeEvent : SpawnObstacleTimeEvent
    {
        protected override ForwardObstacle Spawn(TimeEventSlider slider)
        {
            return null;
        }
    }
}