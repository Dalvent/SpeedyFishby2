using System.Collections;
using UnityEngine;

namespace Code.Commands
{
    public class GlowInTimeTimeCoroutineCommand : ITimeCoroutineCommand
    {
        private static readonly int GlowIntensity = Shader.PropertyToID("_GlowIntensity");

        private readonly SpriteRenderer _target;
        private readonly float _addedIntensity;

        private float _fromIntensity;

        public GlowInTimeTimeCoroutineCommand(SpriteRenderer target, float addedIntensity)
        {
            _target = target;
            _addedIntensity = addedIntensity;
        }

        public IEnumerator Start(float time)
        {
            _fromIntensity = _target.material.GetFloat(GlowIntensity);
            float toIntensity = _fromIntensity + _addedIntensity;

            if (time == 0)
            {
                _target.material.SetFloat(GlowIntensity, toIntensity);
                yield break;
            }
            
            float passedTime = 0;
            while (true)
            {
                passedTime += Time.deltaTime;

                float newIntensity = Mathf.Lerp(_fromIntensity, toIntensity, passedTime / time);
                _target.material.SetFloat(GlowIntensity, newIntensity);

                if (passedTime >= time)
                {
                    yield break;
                }
                
                yield return null;
            }
        }
        
        public ITimeCoroutineCommand CreateGoBackCoroutine()
        {
            return new GlowInTimeTimeCoroutineCommand(_target, -_addedIntensity);
        }
    }
}