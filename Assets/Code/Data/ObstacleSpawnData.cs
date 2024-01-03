using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(menuName = "SpawnData/Obstacle", fileName = "ObstacleSpawnData")]
    public class ObstacleSpawnData : ScriptableObject
    {
        public GameObject ObstaclePrefab;
        public float OffsetX = 0;
    }
}