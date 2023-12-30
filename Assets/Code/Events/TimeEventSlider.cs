using System.Collections.Generic;
using Code.Data;
using Code.Infrastructure;

namespace Code.Events
{
    public class TimeEventSlider
    {
        private readonly Queue<ITimeEvent> _eventQueue;
        public IGameFactory Factory { get; }
        public ISectoredGameFiled SectoredGameFiled { get; }

        public TimeEventSlider(MusicLevel musicLevel, IGameFactory gameFactory, ISectoredGameFiled sectoredGameFiled)
        {
            SectoredGameFiled = sectoredGameFiled;
            Factory = gameFactory;

            _eventQueue = new Queue<ITimeEvent>(musicLevel.SpawnObstacleTimeEvents);
        }

        public void OnUpdate(float passedTime)
        {
            if(_eventQueue.Count == 0)
                return;
            
            if (_eventQueue.Peek().Time <= passedTime)
            {
                ITimeEvent timeEvent = _eventQueue.Dequeue();
                timeEvent.Apply(this);
            }
        }
    }
}