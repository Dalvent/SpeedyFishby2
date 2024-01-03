using System;
using UnityEngine;

namespace Code.Obsticles
{
    public class DamageArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponent<IHealth>()?.TakeDamage();
        }
    }
}