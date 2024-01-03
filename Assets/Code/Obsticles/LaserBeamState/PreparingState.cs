using UnityEngine;

namespace Code.Obsticles.LaserBeamState
{
    public class PreparingState : ILaserBeamState
    {
        private readonly float _time;
        private float _passedTime;

        public PreparingState(float time)
        {
            _time = time;
        }

        public void Enter(LaserBeamMachine machine)
        {
            
        }

        public void Update(LaserBeamMachine machine)
        {
            _passedTime += Time.deltaTime;
            if (_time > _passedTime)
                return;
            
            machine.NextState();
        }

        public void Exit(LaserBeamMachine machine)
        {
        }
    }
}