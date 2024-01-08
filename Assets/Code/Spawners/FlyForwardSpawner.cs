using Code.Characters;
using Code.Events;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(menuName = "spawner/Obstacle", fileName = "FlyForwardSpawner")]
    public class FlyForwardSpawner : PooledSpawner<OutboundDisabler>
    {
    }
}