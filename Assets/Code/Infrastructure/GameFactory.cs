using System;
using Code.Characters;
using Code.Characters.LaserBeam;
using Code.Data;
using Code.Events;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure
{
    public interface IGameFactory
    {
        Transform PlayerTransform { get; }
        
        void WarmUp();
        GameObject CreatePlayer(Vector2 position);
        GameObject CreateBomb(Vector2 position);
        GameObject CreateBonus(Vector2 position);
        GameObject CreateGhost(Vector2 position);
        GameObject CreateLaserBeamMachine(Vector2 position);
    }

    public class GameFactory : IGameFactory
    {
        private const string PlayerPrefabResource = "Player";
        
        private readonly ICameraService _cameraService;
        private readonly IInputService _inputService;

        public Transform PlayerTransform { get; private set; }

        private GameObject _playerPrefab;
        private GhostSpawner _ghostSpawner;
        private LaserBeamMachineSpawner _laserBeamMachineSpawner;
        private FlyForwardSpawner _bombSpawner;
        private FlyForwardSpawner _bonusShildSpawner;
        
        public GameFactory(ICameraService cameraService, IInputService inputService)
        {
            _cameraService = cameraService;
            _inputService = inputService;
        }
        
        public void WarmUp()
        {
            _playerPrefab = Resources.Load<GameObject>(PlayerPrefabResource);
            
            _laserBeamMachineSpawner = Resources.Load<LaserBeamMachineSpawner>("Spawner/LaserBeamMachineSpawner");
            _ghostSpawner = Resources.Load<GhostSpawner>("Spawner/GhostSpawner");
            _bombSpawner = Resources.Load<FlyForwardSpawner>("Spawner/BombSpawner");
            _bonusShildSpawner = Resources.Load<FlyForwardSpawner>("Spawner/BonusShieldSpawner");
        }

        public GameObject CreatePlayer(Vector2 position)
        {
            GameObject gameObject = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
            PlayerTransform = gameObject.transform;
            gameObject.GetComponent<PlayerMover>().Construct(_cameraService, _inputService);
            return gameObject;
        }

        public GameObject CreateBomb(Vector2 position)
        {
            var bomb = _bombSpawner.SpawnAt(position, Quaternion.identity);
            bomb.Construct(_cameraService);
            return bomb.gameObject;
        }

        public GameObject CreateBonus(Vector2 position)
        {
            var bonus = _bonusShildSpawner.SpawnAt(position, Quaternion.identity);
            bonus.Construct(_cameraService);
            return bonus.gameObject;
        }

        public GameObject CreateGhost(Vector2 position)
        {
            return _ghostSpawner.SpawnAt(position, Quaternion.identity).gameObject;
        }

        public GameObject CreateLaserBeamMachine(Vector2 position)
        {
            return _laserBeamMachineSpawner.SpawnAt(position, Quaternion.identity).gameObject;
        }
    }
}