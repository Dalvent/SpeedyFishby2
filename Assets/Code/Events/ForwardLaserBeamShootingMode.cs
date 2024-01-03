using Code.Obsticles.LaserBeamState;
using UnityEngine;

namespace Code.Events
{
    [CreateAssetMenu(menuName = "LaserBeam/Forward", fileName = "ForwardLaserBeam", order = 0)]
    public class ForwardLaserBeamShootingMode : LaserBeamShootingMode
    {
        public override ILaserBeamState CreateShootingState(float shootTime)
        {
            return new ForwardShootState(shootTime);
        }
    }
}