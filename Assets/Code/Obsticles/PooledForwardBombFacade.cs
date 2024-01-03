using System;
using Code.Tools;
using UnityEngine;

namespace Code.Obsticles
{
    public class PooledForwardBombFacade : PooledFacade<PooledForwardBombFacade>
    {
        public ForwardObstacle ForwardObstacle;
    }
}