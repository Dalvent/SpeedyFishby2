using Code.Characters.LaserBeam;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Characters
{
    public class OutboundDisabler : SpawnableEntity<OutboundDisabler>
    {
        private ICameraService _cameraService;
        
        public void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x > _cameraService.HalfHorizontalSize * -2)
                return;
        
            ReturnToSpawner();
        }
    }
}
