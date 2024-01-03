using Code.Obsticles.LaserBeamState;
using UnityEngine;
using UnityEngine.Serialization;

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