using Code.Characters.LaserBeam;
using UnityEngine;

namespace Code.Characters
{
    public class GhostHealth : SpawnableEntity<GhostHealth>, IHealth
    {
        public void TakeDamage()
        {
            Die();
        }
    
        private void Die()
        {
            ReturnToSpawner();
        }
    }
}