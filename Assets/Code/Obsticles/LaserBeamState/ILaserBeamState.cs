namespace Code.Obsticles.LaserBeamState
{
    public interface ILaserBeamState
    {
        void Enter(LaserBeamMachine machine);
        void Update(LaserBeamMachine machine);
        void Exit(LaserBeamMachine machine);
    }
}