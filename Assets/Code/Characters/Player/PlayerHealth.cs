using System;
using UnityEngine;

namespace Code
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public AudioSource DieSound;
        public PlayerMover Mover;
        public ShowEnergyShield ShowEnergyShield;
        public event Action Died;

        private bool _isInsensible;
        
        public void OnEnable()
        {
            ShowEnergyShield.FullyDispersed += OnEnergyShieldFullyDispersed;
        }

        public void OnDisable()
        {
            ShowEnergyShield.FullyDispersed -= OnEnergyShieldFullyDispersed;
        }

        public void ActivateOneDamageShield()
        {
            _isInsensible = true;
            ShowEnergyShield.IsShowing = true;
        }

        public void TakeDamage()
        {
            if (_isInsensible)
            {
                ShowEnergyShield.IsShowing = false;
                return;
            }

            Die();
        }

        private void Die()
        {
            Mover.enabled = false;
            DieSound.Play();
            Died?.Invoke();
            
            transform.position = Vector3.one * 300;
            Destroy(gameObject, 3);
        }

        private void OnEnergyShieldFullyDispersed()
        {
            _isInsensible = false;
        }
    }
}