using System.Threading.Tasks;
using Code.Commands;
using Code.Tools;
using UnityEngine;

namespace Code.Characters.LaserBeam
{
    public class LaserBeamDeathRotate : MonoBehaviour
    {
        private RotateToInTimeCoroutineCommand _rotateToInTime;
        public float Angel = 80f;

        public Task RotateIn(float time)
        {
            var angle = transform.position.y > 0 ? Angel : -Angel;
            _rotateToInTime = new RotateToInTimeCoroutineCommand(transform, angle);

            return this.StartCoroutineWithCallbackAsync(_rotateToInTime.Start(time));
        }
        
        public Task RotateBack(float time)
        {
            return this.StartCoroutineWithCallbackAsync(_rotateToInTime.CreateGoBackCoroutine().Start(time));
        }
    }
}