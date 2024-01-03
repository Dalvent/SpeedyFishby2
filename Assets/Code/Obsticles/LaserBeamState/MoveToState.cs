using UnityEngine;

namespace Code.Obsticles.LaserBeamState
{
    public class MoveToState : ILaserBeamState
    {
        private const float DistanceProximity = 0.0001f;
        
        private readonly Vector2 _to;
        private readonly float _appearTime;
        private float _speed;
        
        public MoveToState(Vector2 to, float appearTime)
        {
            _to = to;
            _appearTime = appearTime;
        }

        public void Enter(LaserBeamMachine machine) => 
            _speed = Vector3.Distance(machine.transform.position, _to) / _appearTime;

        public void Exit(LaserBeamMachine machine) { }

        public void Update(LaserBeamMachine machine)
        {
            machine.transform.position = Vector3.MoveTowards(machine.transform.position, _to, _speed * Time.deltaTime);

            if (Vector3.Distance(machine.transform.position, _to) < DistanceProximity)
                machine.NextState(); 
        }
    }
}