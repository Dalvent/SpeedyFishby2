using UnityEngine;

namespace Code.Infrastructure
{
    public interface IInputService
    {
        Vector2 Cursor { get; }
    }
    
    public class InputService : IInputService
    {
        public Vector2 Cursor => Input.mousePosition;
    }
}