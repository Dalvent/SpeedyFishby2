using Code.Events;

namespace Code.Data
{
    public interface ITimeEvent
    {
        public float Time { get; }
        void Apply(TimeEventSlider slider);
    }
}