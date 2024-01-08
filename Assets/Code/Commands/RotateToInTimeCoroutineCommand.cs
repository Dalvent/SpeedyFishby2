using System.Collections;
using UnityEngine;

namespace Code.Commands
{
    public class RotateToInTimeCoroutineCommand : IResetbleCoroutineCommand
    {
        private readonly Transform _target;
        private readonly Quaternion _to;
        private readonly float _angel;

        private Quaternion _startRotation;

        public RotateToInTimeCoroutineCommand(Transform target, Quaternion to)
        {
            _target = target;
            _to = to;
            _startRotation = target.transform.rotation;
        }

        public RotateToInTimeCoroutineCommand(Transform target, float angle) 
            : this(target, Quaternion.Euler(0, 0, angle))
        {
        }

        public IEnumerator Start(float time)
        {
            _startRotation = _target.transform.rotation;

            if (time == 0)
            {
                _target.transform.rotation = _to;
                yield break;
            }
            
            float passedTime = 0f;
            
            while (true)
            {
                passedTime += Time.deltaTime;

                _target.transform.rotation = Quaternion.Lerp(_startRotation, _to, passedTime / time);

                if (passedTime >= time)
                {
                    yield break;
                }

                yield return null;
            }
        }

        public ITimeCoroutineCommand CreateGoBackCoroutine()
        {
            return new RotateToInTimeCoroutineCommand(_target, _startRotation);
        }
    }
}