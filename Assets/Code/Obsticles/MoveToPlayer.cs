using System;
using System.Collections;
using System.Collections.Generic;
using Code.Infrastructure;
using Code.Tools;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private Transform _player;
    public SpriteRenderer SpriteRenderer;
    public float Speed;
    
    
    public void Construct(Transform player)
    {
        _player = player;
    }

    void Update()
    {
        if(_player.IsUnityNull())
            return;
        
        Vector2 direction = _player.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(transform.position, _player.position, Speed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SpriteRenderer.flipX = !(direction.x >= 0);

        if (SpriteRenderer.flipX)
            angle -= 180f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
