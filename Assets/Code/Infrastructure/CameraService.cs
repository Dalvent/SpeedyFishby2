using System;
using Code.Tools;
using UnityEngine;

namespace Code.Infrastructure
{
    public interface ICameraService
    {
        Vector2 ToWorldPoint(Vector2 displayPosition);
    }
    
    public class CameraService : ICameraService
    {
        private Camera MainCamera => Camera.main;
        
        public Vector2 ToWorldPoint(Vector2 displayPosition)
        {
            var target = InDisplay(MainCamera.ScreenToWorldPoint(Input.mousePosition));
            return target;
        }

        private Vector2 InDisplay(Vector3 position)
        {
            var verticalSize = MainCamera.orthographicSize;
            var horizontalSize = verticalSize * Screen.width / Screen.height;
            return new Vector2(position.x.ValueInRange(-horizontalSize, horizontalSize), position.y.ValueInRange(-verticalSize, verticalSize));
        }
    }
}