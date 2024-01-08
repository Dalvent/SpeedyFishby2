using System;
using Code.Characters;
using GD.MinMaxSlider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Events
{
    [Serializable]
    public class SpawnBombTimeEvent : SpawnFlyForwardTimeEvent
    {
        [MinMaxSlider(0,3)] 
        [SerializeField] private Vector2 _randomSizeMultiplayer = Vector2.one;

        protected override FlyForward Spawn(TimeEventSlider slider, Vector2 position)
        {
            var bomb = slider.Factory.CreateBomb(position);
            MakeRandomResize(bomb.transform);
            return bomb.GetComponent<FlyForward>();
        }

        private void MakeRandomResize(Transform target)
        {
            if (_randomSizeMultiplayer.x == 1 && _randomSizeMultiplayer.y == 1)
                return;

            target.localScale *= Random.Range(_randomSizeMultiplayer.x, _randomSizeMultiplayer.y);
        }
    }
}