using Code.Events;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(menuName = "MusicLevel", fileName = "MusicLevel")]
    public class MusicLevel : ScriptableObject
    {
        public AudioClip Music;
        public GameObject BackgroundPrefab;
        public float StartBackgroundSpeed;

        public SpawnObstacleTimeEvent[] SpawnObstacleTimeEvents;
        public SpawnGhostTimeEvent[] SpawnGhostTimeEvents;
        public SpawnLaserBeamMachineTimeEvent[] SpawnLaserBeamMachineTimeEvents;
    }
}