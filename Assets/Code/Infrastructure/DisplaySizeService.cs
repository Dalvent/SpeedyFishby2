using System;
using UnityEngine;

namespace Code.Infrastructure
{
    public interface IDisplaySizeService
    {
        int Width { get; }
        int Height { get; }
        event Action DisplayResized;
    }

    public class DisplaySizeService : IDisplaySizeService
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public event Action DisplayResized;

        public DisplaySizeService()
        {
            Application.onBeforeRender += OnBeforeRender;
        }

        private void OnBeforeRender()
        {
            if (Width == Screen.width && Height == Screen.height) 
                return;
            
            Width = Screen.width;
            Height = Screen.height;
            DisplayResized?.Invoke();
        }
    }
}