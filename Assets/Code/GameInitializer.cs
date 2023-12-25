using System;
using Code.Infrastructure;
using Unity.Mathematics;
using UnityEngine;

namespace Code
{
    public class GameInitializer : MonoBehaviour
    {
        private readonly ICameraService _cameraService = new CameraService();
        private readonly IInputService _inputService = new InputService();
        
        public GameObject PlayerPrefab;
        public Transform PlayerSpawnLocation;
        
        public GameObject EnemyPrefab;

        private void Awake()
        {
            InitlizePlayer();
        }

        private void InitlizePlayer()
        {
            GameObject instantiate = Instantiate(PlayerPrefab, PlayerSpawnLocation.position, quaternion.identity);
            instantiate.GetComponent<PlayerMover>().Construct(_cameraService, _inputService);
        }
    }
}