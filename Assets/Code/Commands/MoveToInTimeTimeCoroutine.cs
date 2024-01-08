using System.Collections;
using UnityEngine;

namespace Code.Commands
{
    public class MoveToInTimeTimeCoroutine : IResetbleCoroutineCommand
    {
        private const float DistanceProximity = 0.0001f;

        private readonly Transform _target;
        private readonly Vector2 _distance;

        private Vector2 _startPosition;
        
        public MoveToInTimeTimeCoroutine(Transform target, Vector2 distance)
        {
            _target = target;
            _distance = distance;
        }

        public IEnumerator Start(float time)
        {
            _startPosition = _target.position;

            Vector2 toPosition = _startPosition + _distance;

            if (time == 0)
            {
                _target.position = toPosition;
                yield break;
            }
            
            float speed = Vector3.Distance(_startPosition, toPosition) / time;

            while (true)
            {
                _target.position = Vector3.MoveTowards(_target.position, toPosition,
                    speed * Time.deltaTime);

                if (Vector3.Distance(_target.position, toPosition) < DistanceProximity)
                {
                    yield break;
                }
                yield return null;
            }
        }

        public ITimeCoroutineCommand CreateGoBackCoroutine()
        {
            return new MoveToInTimeTimeCoroutine(_target, -_distance);
        }
    }
}