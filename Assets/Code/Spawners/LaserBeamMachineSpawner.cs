using System;
using Code.Characters.LaserBeam;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Events
{
    [CreateAssetMenu(menuName = "spawner/LaserBeamMachine", fileName = "LaserBeamMachineSpawner", order = 0)]
    public class LaserBeamMachineSpawner : PooledSpawner<LaserBeamMachineFacade>
    {
    }
}