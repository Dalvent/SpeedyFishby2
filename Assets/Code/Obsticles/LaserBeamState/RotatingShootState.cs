using UnityEngine;

namespace Code.Obsticles.LaserBeamState
{
    public class RotatingShootState : ILaserBeamState
    {
        private readonly float _time;
        private readonly float _angel;
        private float _passedTime;
        private Quaternion _targetRotation;
        private Quaternion _startRotation;

        public RotatingShootState(float time, float angel)
        {
            _time = time;
            _angel = angel;
        }

        public void Enter(LaserBeamMachine machine)
        {
            machine.ShowLaser();
            
            _startRotation = machine.transform.rotation;
            _targetRotation = Quaternion.Euler(0, 0, 90);
        }

        public void Exit(LaserBeamMachine machine) =>
            machine.HideLaser();

        public void Update(LaserBeamMachine machine)
        {
            machine.transform.rotation = Quaternion.Lerp(_startRotation, _targetRotation, _passedTime / _time);
            _passedTime += Time.deltaTime;
            if (_time > _passedTime)
                return;
            
            machine.NextState();
        }
    }
}