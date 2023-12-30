using Code.Events;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(menuName = "MusicLevel", fileName = "MusicLevel")]
    public class MusicLevel : ScriptableObject
    {
        public AudioClip Music;
        public SpawnObstacleTimeEvent[] SpawnObstacleTimeEvents;
    }
}