using System;
using UnityEngine;

namespace Code.Obsticles.LaserBeamState
{
    public class ForwardShootState : ILaserBeamState
    {
        private readonly float _time;
        private float _passedTime;

        public ForwardShootState(float time)
        {
            _time = time;
        }
        
        public void Enter(LaserBeamMachine machine) =>
            machine.ShowLaser();

        public void Exit(LaserBeamMachine machine) =>
            machine.HideLaser();

        public void Update(LaserBeamMachine machine)
        {
            _passedTime += Time.deltaTime;
            if (_time > _passedTime)
                return;
            
            machine.NextState();
        }
    }
}