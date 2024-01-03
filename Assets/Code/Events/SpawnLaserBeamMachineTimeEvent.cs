using System;
using System.Collections.Generic;
using Code.Data;
using Code.Infrastructure;
using Code.Obsticles;
using Code.Obsticles.LaserBeamState;
using Code.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Events
{
    public enum LaserBeamMachinePosition
    {
        ForwardUpper,
        ForwardCenter,
        ForwardDown
    }
    
    [Serializable]
    public class SpawnLaserBeamMachineTimeEvent : ITimeEvent
    {
        [SerializeField] private float _time;
        
        public float Time => _time;

        public LaserBeamMachineSpawnData SpawnData;
        public LaserBeamShootingMode Mode;
        public LaserBeamMachinePosition Position;
        public float AppearTime;
        public float PrepareTime;
        public float ShootingTime;

        public void Apply(TimeEventSlider slider)
        {
            var pooled = slider.LaserBeamMachinePool.Rent();
            pooled.transform.position = CreatePosition(slider);
            pooled.LaserBeamMachine.AddStates(CreateStates(pooled.transform));
        }

        private Vector2 CreatePosition(TimeEventSlider slider)
        {
            return new Vector2(slider.SectoredGameFiled.ForwardX + SpawnData.OffsetOutOfMap, GetMiddle(GetSector(slider)));
        }
        
        
        private (float from, float to) GetSector(TimeEventSlider slider)
        {
            return Position switch
            {
                LaserBeamMachinePosition.ForwardUpper => slider.SectoredGameFiled.GetTopSector(),
                LaserBeamMachinePosition.ForwardCenter => slider.SectoredGameFiled.GetCenterSector(),
                LaserBeamMachinePosition.ForwardDown => slider.SectoredGameFiled.GetBottomSector(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static float GetMiddle((float from, float to) sector)
        {
            float sectorFrom = (sector.from - sector.to) / 2;
            return sector.to + sectorFrom;
        }

        private IEnumerable<ILaserBeamState> CreateStates(Transform transform)
        {
            var position = transform.position;
            var movePosition = position.AddX(-SpawnData.OffsetOutOfMap - SpawnData.OffsetOnAppeared);
            return new[]
            {
                new MoveToState(movePosition, AppearTime),
                new PreparingState(PrepareTime),
                Mode.CreateShootingState(ShootingTime),
                new MoveToState(position, AppearTime),
            };
        }
    }

    public abstract class LaserBeamShootingMode : ScriptableObject
    {
        public abstract ILaserBeamState CreateShootingState(float shootTime);
    }
}