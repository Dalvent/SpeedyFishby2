using Code.Events;
using UnityEngine;

namespace Code.Characters.LaserBeam
{
    public class SpawnableEntity<TSpawnableEntity> : MonoBehaviour where TSpawnableEntity : SpawnableEntity<TSpawnableEntity>
    {
        public PooledSpawner<TSpawnableEntity> Spawner { get; set; }
        
        public void ReturnToSpawner()
        {
            Spawner.Return((TSpawnableEntity)this);
        }
    }
}