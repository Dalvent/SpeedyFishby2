using System;
using System.Threading.Tasks;
using Code.Commands;
using Code.Tools;
using UnityEngine;

namespace Code.Characters.LaserBeam
{
    public class ScreenAppear : MonoBehaviour
    {
        public Vector2 AppearOffset;
        private MoveToInTimeTimeCoroutine _moveInTime;

        public bool IsAppeared { get; private set; }

        public void Awake()
        {
            var thisTransform = transform;
            _moveInTime = new MoveToInTimeTimeCoroutine(thisTransform, AppearOffset);
        }

        public Task Appear(float time)
        {
            return this.StartCoroutineWithCallbackAsync(_moveInTime.Start(time))
                .ContinueWith(t => IsAppeared = true);
        }

        public Task Disappear(float time)
        {
            return this.StartCoroutineWithCallbackAsync(_moveInTime.CreateGoBackCoroutine().Start(time))
                .ContinueWith(t => IsAppeared = false);
        }
    }
}