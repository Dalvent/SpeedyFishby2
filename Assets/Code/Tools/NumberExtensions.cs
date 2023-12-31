﻿using System;
using UnityEngine;

namespace Code.Tools
{
    public static class NumberExtensions
    {
        public static float ValueInRange(this float value, float min, float max)
        {
            return Math.Max(Math.Min(value, max), min);
        }
        
        public static Vector2 AddX(this Vector3 value, float x)
        {
            return new Vector2(value.x + x, value.y);
        }
        
        public static Vector2 ValueInRange(this Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(value.x.ValueInRange(min.x, max.x), value.y.ValueInRange(min.y, max.y));
        }
        
        public static Vector3 ValueInRange(this Vector3 value, Vector2 min, Vector2 max)
        {
            return new Vector3(value.x.ValueInRange(min.x, max.x), value.y.ValueInRange(min.y, max.y));
        }
        
        public static Vector3 Abs(this Vector3 value) 
        {
            return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
        }
    }
}