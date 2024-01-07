using System;
using Code.Data;
using Code.Infrastructure;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Code
{
    public class GameInitializer : MonoBehaviour
    {
        private const string PlayerSpawnPositionTag = "PlayerSpawnPosition";
        
        private readonly IDisplaySizeService _displaySizeService;
        private readonly ISectoredGameFiled _sectoredGameFiled;
        private readonly ICameraService _cameraService;
        private readonly IInputService _inputService;
        private readonly IGameFactory _gameFactory;

        private Transform _playerSpawner;
        
        public GameObject MusicLevelScrollerPrefab;
        public MusicLevel MusicLevel;
        private MusicLevelScroller _musicLevelScroller;
        private BackgroundScroller _backgroundScroller;

        public GameInitializer()
        {
            _displaySizeService = new DisplaySizeService();
            _cameraService = new CameraService(_displaySizeService);
            _sectoredGameFiled = new SectoredGameFiled(_cameraService);
            _inputService = new InputService();
            _gameFactory = new GameFactory(_cameraService, _inputService);
        }

        private void Awake()
        {
            _gameFactory.WarmUp();
            
            _playerSpawner = GameObject.FindGameObjectWithTag(PlayerSpawnPositionTag).transform;

            InitializeBackground();
            InitializeMusicLevelScroller();
            InitializeHud();
            InitializePlayer();
        }

        private void InitializeHud()
        {
            _gameFactory.CreateHud(_musicLevelScroller);
        }

        private void InitializeBackground()
        {
            var background = Instantiate(MusicLevel.BackgroundPrefab, Vector3.zero, quaternion.identity)
                .GetComponent<BackgroundScroller>();
            _backgroundScroller = background;
        }

        private void InitializeMusicLevelScroller()
        {
            GameObject instantiate = Instantiate(MusicLevelScrollerPrefab, Vector3.zero, quaternion.identity);
            _musicLevelScroller = instantiate.GetComponent<MusicLevelScroller>();
            _musicLevelScroller.Construct(_gameFactory, _sectoredGameFiled, MusicLevel, _backgroundScroller);
            _backgroundScroller.GetComponent<BackgroundAudio>().audioSource = _musicLevelScroller.MusicAudioSource;

            Volume component = GameObject.FindWithTag("GlobalVolume").GetComponent<Volume>();
            instantiate.GetComponent<AudioReactSaturation>().globalVolume = component;
        }

        private void InitializePlayer()
        {
            GameObject instantiate = _gameFactory.CreatePlayer(_playerSpawner.position);
            instantiate.GetComponent<PlayerHealth>().Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _musicLevelScroller.enabled = false;
            _musicLevelScroller.MusicAudioSource.Stop();
        }
    }
}