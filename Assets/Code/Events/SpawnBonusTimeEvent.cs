using System;
using Code.Characters;
using UnityEngine;

namespace Code.Events
{
    [Serializable]
    public class SpawnBonusTimeEvent : SpawnFlyForwardTimeEvent
    {
        protected override FlyForward Spawn(TimeEventSlider slider, Vector2 position)
        {
            var bonus = slider.Factory.CreateBonus(position);
            return bonus.GetComponent<FlyForward>();
        }
    }
}