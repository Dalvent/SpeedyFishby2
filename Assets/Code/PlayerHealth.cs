using System;
using UnityEngine;

namespace Code
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public PlayerMover Mover;
        public event Action Died;
        
        public void ActivateOneDamageShield()
        {
        }
        
        public void TakeDamage()
        {
            Die();
        }
    
        private void Die()
        {
            Mover.enabled = false;
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}