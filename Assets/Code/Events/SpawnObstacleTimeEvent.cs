using System;
using Code.Data;
using Code.Infrastructure;
using UnityEngine;
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

    [Serializable]
    public class SpawnObstacleTimeEvent : ITimeEvent
    {
        [SerializeField] private float _time;
    
        public ObstacleSpawnData ObstacleSpawnData;
        public RandomLocation randomLocation ;

        public float Time => _time;

        public void Apply(TimeEventSlider slider)
        {
            var position = GeneratePosition(slider.SectoredGameFiled);
            UnityEngine.Object.Instantiate(ObstacleSpawnData.ObstaclePrefab, position, Quaternion.identity);
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
            switch (randomLocation)
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
}