using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Code.Commands;
using Code.Tools;
using UnityEngine;

namespace Code.Characters.LaserBeam
{
    public class LaserPrepare : MonoBehaviour
    {
        [Category("Audio")]
        public AudioSource PrepareAudioSource;
        public float _audioAdditionalTime;
        
        [Category("Visual")]
        public SpriteRenderer MachineSprite;
        public float AddedIntensity;
        
        private GlowInTimeTimeCoroutineCommand _glowInTimeTime;
        private Action _onDone;
        
        public bool IsPrepared { get; private set; }

        public Task Prepare(float prepareTime)
        {
            _glowInTimeTime = new GlowInTimeTimeCoroutineCommand(MachineSprite, AddedIntensity);
            
            if(prepareTime != 0)
                PrepareAudioSource.PlayForDuration(prepareTime + _audioAdditionalTime);
            
            return this.StartCoroutineWithCallbackAsync(_glowInTimeTime.Start(prepareTime))
                .ContinueWith(t => IsPrepared = true);
        }

        public Task ResetPrepare(float time)
        {
            return this.StartCoroutineWithCallbackAsync(_glowInTimeTime.CreateGoBackCoroutine().Start(time))
                .ContinueWith(t => IsPrepared = false);
        }
    }
}