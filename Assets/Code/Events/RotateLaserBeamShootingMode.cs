using Code.Obsticles.LaserBeamState;
using UnityEngine;

namespace Code.Events
{
    [CreateAssetMenu(menuName = "LaserBeam/Rotate", fileName = "RotateLaserBeam", order = 0)]
    public class RotateLaserBeamShootingMode : LaserBeamShootingMode
    {
        public float Degree = 0;
        public override ILaserBeamState CreateShootingState(float shootTime)
        {
            return new RotatingShootState(shootTime, Degree);
        }
    }
}