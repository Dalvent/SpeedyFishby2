using UnityEngine;

namespace Code.Events
{
    [CreateAssetMenu(menuName = "SpawnData/Ghost", fileName = "GhostSpawnerData")]
    public class GhostSpawnerData : ScriptableObject
    {
        public GameObject GhostPrefab;
        public float OffsetX = 0;
    }
}