using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    
    private float _startXPosition;
    private float _positionXToRepeat;
    
    void Start()
    {
        _startXPosition = transform.position.x;
        _positionXToRepeat = _startXPosition - (_spriteRenderer.size.x * transform.localScale.x);
     
        _spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        _spriteRenderer.size *= new Vector2(3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (_speed * Time.deltaTime);
        if (transform.position.x <= _positionXToRepeat)
        {
            transform.position = new Vector2(
                transform.position.x - _positionXToRepeat + _startXPosition,
                transform.position.y
            );
        }
    }
    
    
}