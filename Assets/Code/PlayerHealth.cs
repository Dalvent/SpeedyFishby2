using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMover Mover;
    public event Action Died;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Mover.enabled = false;
        Died?.Invoke();
        Destroy(gameObject);
    }
}
