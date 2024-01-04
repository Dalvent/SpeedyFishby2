using System;
using GD.MinMaxSlider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Events
{
    [Serializable]
    public class SpawnBombTimeEvent : SpawnObstacleTimeEvent
    {
        [MinMaxSlider(0,3)] 
        [SerializeField] private Vector2 _randomSizeMultiplayer = Vector2.one;

        protected override ForwardObstacle Spawn(TimeEventSlider slider)
        {
            var spawned = slider.BombPool.Rent();
            MakeRandomResize(spawned.transform);
            return spawned.ForwardObstacle;
        }

        private void MakeRandomResize(Transform target)
        {
            if (_randomSizeMultiplayer.x == 1 && _randomSizeMultiplayer.y == 1)
                return;

            target.localScale *= Random.Range(_randomSizeMultiplayer.x, _randomSizeMultiplayer.y);
        }
    }
}