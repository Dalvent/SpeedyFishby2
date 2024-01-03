using UnityEngine;

namespace Code.Events
{
    [CreateAssetMenu(menuName = "SpawnData/LaserBeamMachine", fileName = "LaserBeamMachineSpawnData", order = 0)]
    public class LaserBeamMachineSpawnData : ScriptableObject
    {
        public float OffsetOutOfMap = 0;
        public float OffsetOnAppeared = 0;
    }
}