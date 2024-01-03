using UnityEngine;

namespace Code.Obsticles
{
    public class GhostHealth : MonoBehaviour, IHealth
    {
        public void TakeDamage()
        {
            Die();
        }
    
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}