using System;
using Code.Infrastructure;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class GameInitializer : MonoBehaviour
    {
        private readonly IDisplaySizeService _displaySizeService;
        private readonly ISectoredGameFiled _sectoredGameFiled;
        private readonly ICameraService _cameraService;
        private readonly IInputService _inputService;
        private readonly IGameFactory _gameFactory;

        public GameObject GameClock;
        public GameObject PlayerPrefab;
        public Transform PlayerSpawnLocation;
        private GameClock _gameClock;

        public GameInitializer()
        {
            _displaySizeService = new DisplaySizeService();
            _cameraService = new CameraService(_displaySizeService);
            _sectoredGameFiled = new SectoredGameFiled(_cameraService);
            _inputService = new InputService();
            _gameFactory = new GameFactory();
        }

        private void Awake()
        {
            InitializeGameClock();
            InitializePlayer();
        }

        private void InitializeGameClock()
        {
            GameObject instantiate = Instantiate(GameClock, Vector3.zero, quaternion.identity);
            _gameClock = instantiate.GetComponent<GameClock>();
            _gameClock.Construct(_gameFactory, _sectoredGameFiled);
        }

        private void InitializePlayer()
        {
            GameObject instantiate = Instantiate(PlayerPrefab, PlayerSpawnLocation.position, quaternion.identity);
            instantiate.GetComponent<PlayerMover>().Construct(_cameraService, _inputService);
            instantiate.GetComponent<PlayerHealth>().Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _gameClock.enabled = false;
            _gameClock.MusicAudioSource.Stop();
        }
    }
}