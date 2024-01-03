using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class BackgroundScroller : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public float Speed;
    
        private float _startXPosition;
        private float _positionXToRepeat;
    
        void Start()
        {
            _startXPosition = transform.position.x;
            _positionXToRepeat = _startXPosition - (SpriteRenderer.size.x * transform.localScale.x);
     
            SpriteRenderer.drawMode = SpriteDrawMode.Tiled;
            SpriteRenderer.size *= new Vector2(3, 1);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += Vector3.left * (Speed * Time.deltaTime);
            if (transform.position.x <= _positionXToRepeat)
            {
                transform.position = new Vector2(
                    transform.position.x - _positionXToRepeat + _startXPosition,
                    transform.position.y
                );
            }
        }
    
    
    }
}