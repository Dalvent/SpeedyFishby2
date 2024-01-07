using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Infrastructure;
using Code.Obsticles;
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

        public Pool<PooledForwardBombFacade> BombPool { get; }
        public Pool<PooledLaserBeamMachineFacade> LaserBeamMachinePool { get; }
        //public Pool<PooledFac> LaserBeamMachinePool { get; }

        public TimeEventSlider(MusicLevelScroller musicLevelScroller, MusicLevel musicLevel, IGameFactory gameFactory, ISectoredGameFiled sectoredGameFiled)
        {
            MusicLevelScroller = musicLevelScroller;
            MusicLevel = musicLevel;
            SectoredGameFiled = sectoredGameFiled;
            Factory = gameFactory;

            _eventQueue = new List<ITimeEvent>();
            
            _eventQueue.AddRange(musicLevel.SpawnObstacleTimeEvents);
            _eventQueue.AddRange(musicLevel.SpawnGhostTimeEvents);
            _eventQueue.AddRange(musicLevel.SpawnLaserBeamMachineTimeEvents);
            _eventQueue.Sort((t1, t2) => t1.Time.CompareTo(t2.Time));
            
            BombPool = new Pool<PooledForwardBombFacade>(CreatePooledBomb);
            BombPool.WarmUp(30);

            LaserBeamMachinePool = new Pool<PooledLaserBeamMachineFacade>(CreateLaserBeamMachine);
            LaserBeamMachinePool.WarmUp(4);
        }

        private PooledForwardBombFacade CreatePooledBomb()
        {
            PooledForwardBombFacade bomb = Factory.CreateBomb(Vector2.zero).GetComponent<PooledForwardBombFacade>();
            bomb.Construct(BombPool);
            return bomb;
        }

        private PooledLaserBeamMachineFacade CreateLaserBeamMachine()
        {
            PooledLaserBeamMachineFacade bomb = Factory.CreateLaserBeamMachine().GetComponent<PooledLaserBeamMachineFacade>();
            bomb.Construct(LaserBeamMachinePool);
            return bomb;
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