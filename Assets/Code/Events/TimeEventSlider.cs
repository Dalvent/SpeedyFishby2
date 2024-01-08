using System.Collections.Generic;
using System.Linq;
using Code.Characters;
using Code.Characters.LaserBeam;
using Code.Data;
using Code.Infrastructure;
using Code.Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Events
{
    public class TimeEventSlider
    {
        private readonly List<ITimeEvent> _eventQueue;

        public MusicLevelScroller MusicLevelScroller { get; }
        public MusicLevel MusicLevel { get; }
        public IGameFactory Factory { get; }
        public ISectoredGameFiled SectoredGameFiled { get; }

        public TimeEventSlider(MusicLevelScroller musicLevelScroller, MusicLevel musicLevel, IGameFactory gameFactory, ISectoredGameFiled sectoredGameFiled)
        {
            MusicLevelScroller = musicLevelScroller;
            MusicLevel = musicLevel;
            SectoredGameFiled = sectoredGameFiled;
            Factory = gameFactory;

            _eventQueue = new List<ITimeEvent>();
            
            gameFactory.WarmUp();
            _eventQueue.AddRange(musicLevel.SpawnObstacleTimeEvents);
            _eventQueue.AddRange(musicLevel.SpawnGhostTimeEvents);
            _eventQueue.AddRange(musicLevel.SpawnLaserBeamMachineTimeEvents);
            _eventQueue.AddRange(musicLevel.SpawnBonusTimeEvent);
            _eventQueue.Sort((t1, t2) => t1.Time.CompareTo(t2.Time));
        }

        public void OnUpdate(float passedTime)
        {
            var closestEvent = _eventQueue.FirstOrDefault();
            if(closestEvent == null)
                return;

            if (closestEvent.Time <= passedTime)
            {
                closestEvent.Apply(this);
                _eventQueue.Remove(closestEvent);
            }
        }
    }
}