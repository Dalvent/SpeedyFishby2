using System;
using Code.Tools;
using UnityEngine;

namespace Code.Infrastructure
{
    public interface ICameraService
    {
        float HalfVerticalSize { get; }
        float HalfHorizontalSize { get; }
        Vector2 ToWorldPoint(Vector2 displayPosition);
    }
    
    public class CameraService : ICameraService
    {
        private readonly IDisplaySizeService _displaySizeService;
        
        public float HalfVerticalSize { get; set; }
        public float HalfHorizontalSize { get; set; }
        private Camera MainCamera => Camera.main;

        public CameraService(IDisplaySizeService displaySizeService)
        {
            _displaySizeService = displaySizeService;
            _displaySizeService.DisplayResized += OnDisplaySizeResized;
        }

        private void OnDisplaySizeResized()
        {
            HalfVerticalSize = MainCamera.orthographicSize;
            HalfHorizontalSize = HalfVerticalSize * _displaySizeService.Width / _displaySizeService.Height;
        }

        public Vector2 ToWorldPoint(Vector2 displayPosition) => 
            InDisplay(MainCamera.ScreenToWorldPoint(Input.mousePosition));

        private Vector2 InDisplay(Vector3 position)
        {
            return new Vector2(position.x.ValueInRange(-HalfHorizontalSize, HalfHorizontalSize), position.y.ValueInRange(-HalfVerticalSize, HalfVerticalSize));
        }
    }
}