using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code.Characters.LaserBeam
{
    public class LaserBeamMachineFacade : SpawnableEntity<LaserBeamMachineFacade>
    {
        public LaserPrepare Prepare;
        public LaserShooter Shooter;
        public ScreenAppear Appear;
        public LaserBeamDeathRotate Rotate;

        public async void ShowWithBeam(LaserBeamTime time, bool useRotation)
        {
            await Appear.Appear(time.AppearTime);
            await Prepare.Prepare(time.PrepareTime);

            await Task.WhenAll(StartShootingTasks(time, useRotation));
            await Task.WhenAll(StartDisappearTasks(time, useRotation));
            
            ReturnToSpawner();
        }

        private List<Task> StartShootingTasks(LaserBeamTime time, bool useRotation)
        {
            var shootingTasks = new List<Task>()
            {
                Shooter.Shoot(time.ShootingTime)
            };

            if (useRotation)
                shootingTasks.Add(Rotate.RotateIn(time.ShootingTime));
            return shootingTasks;
        }

        private List<Task> StartDisappearTasks(LaserBeamTime time, bool useRotation)
        {
            var disappearTasks = new List<Task>()
            {
                Appear.Disappear(time.DisappearTime),
                Prepare.ResetPrepare(time.DisappearTime)
            };

            if (useRotation)
                disappearTasks.Add(Rotate.RotateBack(time.DisappearTime));
            return disappearTasks;
        }
    }

    [Serializable]
    public struct LaserBeamTime
    {
        public float AppearTime;
        public float PrepareTime;
        public float ShootingTime;
        public float DisappearTime;
    }
}