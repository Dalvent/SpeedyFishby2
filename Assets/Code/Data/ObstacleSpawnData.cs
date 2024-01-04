using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(menuName = "SpawnData/Obstacle", fileName = "ObstacleSpawnData")]
    public class ObstacleSpawnData : ScriptableObject
    {
        public float OffsetX = 0;
    }
}