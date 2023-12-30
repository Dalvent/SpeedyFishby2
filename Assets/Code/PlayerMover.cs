using Code.Infrastructure;
using UnityEngine;

namespace Code
{
    public class PlayerMover : MonoBehaviour
    {
        private IInputService _inputService;
        private ICameraService _cameraService;
        
        public float Speed;

        public void Construct(ICameraService cameraService, IInputService inputService)
        {
            _cameraService = cameraService;
            _inputService = inputService;
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _cameraService.ToWorldPoint(_inputService.Cursor), Speed * Time.deltaTime);
        }
    }
}
