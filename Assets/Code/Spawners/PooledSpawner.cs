using System.Collections.Generic;
using Code.Characters.LaserBeam;
using UnityEngine;

namespace Code.Events
{
    public abstract class PooledSpawner<TSpawnableEntity> : ScriptableObject where TSpawnableEntity : SpawnableEntity<TSpawnableEntity>
    {
        public GameObject Prefab;
        public Vector2 SpawnOffset;
        public int StartPoolCount;
        
        public Stack<TSpawnableEntity> Existed = new();

        public void Awake()
        {
            var spawnable = Prefab.GetComponent<TSpawnableEntity>();
            if(spawnable == null)
                Debug.LogError($"{Prefab.name} must have {nameof(TSpawnableEntity)} component!");
        }

        public void WarmUp()
        {
            Existed.Clear();
            for (int i = Existed.Count; i < StartPoolCount; i++)
            {
                Existed.Push(InstantiateNew());
            }
        }

        public TSpawnableEntity SpawnAt(Vector2 position, Quaternion rotation)
        {
            var spawnableEntity = Existed.Count == 0 ? InstantiateNew() : Existed.Pop();
            
            var transform = spawnableEntity.transform;
            transform.position = position + SpawnOffsetBy(position);
            transform.rotation = rotation;
            
            spawnableEntity.gameObject.SetActive(true);
            return spawnableEntity;
        }

        public void Return(TSpawnableEntity spawnableEntity)
        {
            spawnableEntity.gameObject.SetActive(false);
            Existed.Push(spawnableEntity);
        }

        private TSpawnableEntity InstantiateNew()
        {
            var gameObject = Object.Instantiate(Prefab);
            gameObject.gameObject.SetActive(false);
            TSpawnableEntity spawnableEntity = gameObject.GetComponent<TSpawnableEntity>();
            spawnableEntity.Spawner = this;
            return spawnableEntity;
        }

        private Vector2 SpawnOffsetBy(Vector2 position)
        {
            float x = position.x >= 0 ? SpawnOffset.x : -SpawnOffset.x;
            float y = position.y >= 0 ? SpawnOffset.y : -SpawnOffset.y;
            return new Vector2(x, y);
        }
    }
}