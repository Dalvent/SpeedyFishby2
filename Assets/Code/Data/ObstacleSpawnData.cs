using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(menuName = "ObstacleSpawnData", fileName = "ObstacleSpawnData")]
    public class ObstacleSpawnData : ScriptableObject
    {
        public GameObject ObstaclePrefab;
        public float OffsetX = 0;
    }
}