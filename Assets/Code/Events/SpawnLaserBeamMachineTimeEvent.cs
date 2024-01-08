using System;
using Code.Characters.LaserBeam;
using Code.Data;
using Code.Infrastructure;
using UnityEngine;

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
        
        public LaserBeamMachinePosition Position;
        public LaserBeamTime AppearTime;
        public bool UseRotation;
        
        public void Apply(TimeEventSlider slider)
        {
            var laserBeamMachine = slider.Factory.CreateLaserBeamMachine(CreatePosition(slider));
            laserBeamMachine.GetComponent<LaserBeamMachineFacade>().ShowWithBeam(AppearTime, UseRotation);
        }

        private Vector2 CreatePosition(TimeEventSlider slider)
        {
            return new Vector2(slider.SectoredGameFiled.ForwardX, GetMiddle(GetSector(slider)));
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
    }
}