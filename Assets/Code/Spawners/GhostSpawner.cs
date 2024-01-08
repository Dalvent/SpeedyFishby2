using Code.Characters;
using UnityEngine;

namespace Code.Events
{
    [CreateAssetMenu(menuName = "spawner/Ghost", fileName = "GhostSpawner")]
    public class GhostSpawner : PooledSpawner<GhostHealth>
    {
    }
}