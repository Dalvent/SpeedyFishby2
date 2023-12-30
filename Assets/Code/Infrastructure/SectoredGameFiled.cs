using System;
using UnityEngine;

namespace Code.Infrastructure
{
    public interface ISectoredGameFiled
    {
        const int SectorCount = 9;
        (float from, float to) GetSectorRange(int fromSector, int toSector);
        float ForwardX { get; }
    }

    public class SectoredGameFiled : ISectoredGameFiled
    {
        private readonly ICameraService _cameraService;

        public SectoredGameFiled(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        public (float from, float to) GetSectorRange(int fromSector, int toSector)
        {
            float horizontalSize = _cameraService.HalfVerticalSize * 2;
            float sectorSize = horizontalSize / ISectoredGameFiled.SectorCount;
            
            float from = -_cameraService.HalfVerticalSize + sectorSize * fromSector;
            return (from, from + sectorSize * (toSector - fromSector));
        }

        public float ForwardX => _cameraService.HalfHorizontalSize;
    }

    public static class SectoredGameFiledExtensions
    {
        public static (float from, float to) GetTopSector(this ISectoredGameFiled sectoredGameFiled) =>
            sectoredGameFiled.GetSectorRange(6, 9);

        public static (float from, float to) GetCenterSector(this ISectoredGameFiled sectoredGameFiled) =>
            sectoredGameFiled.GetSectorRange(3, 6);

        public static (float from, float to) GetBottomSector(this ISectoredGameFiled sectoredGameFiled) =>
            sectoredGameFiled.GetSectorRange(0, 3);
    }
}