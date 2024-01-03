using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure
{
    public interface IGameFactory
    {
        void WarmUp();
        GameObject CreateBomb(Vector2 position);
        GameObject CreatePlayer(Vector2 position);
        GameObject CreateHud(MusicLevelScroller musicLevelScroller);
        GameObject CreateGhost(Vector2 position, float speed);
        GameObject CreateLaserBeamMachine();
    }

    public class GameFactory : IGameFactory
    {
        private const string BombResource = "Bomb";
        private const string PlayerResource = "Player";
        private const string GhostResource = "Ghost";
        private const string LaserBeamMachineResource = "LaserBeamMachine";
        
        private readonly ICameraService _cameraService;
        private readonly IInputService _inputService;
        
        private GameObject _playerPrefab;
        private GameObject _bombPrefab;
        private GameObject _ghostPrefab;
        private Transform _playerTransform;
        private GameObject _laserBeamMachine;

        public GameFactory(ICameraService cameraService, IInputService inputService)
        {
            _cameraService = cameraService;
            _inputService = inputService;
        }
        
        public void WarmUp()
        {
            _playerPrefab = Resources.Load<GameObject>(PlayerResource);
            _bombPrefab = Resources.Load<GameObject>(BombResource);
            _ghostPrefab = Resources.Load<GameObject>(GhostResource);
            _laserBeamMachine = Resources.Load<GameObject>(LaserBeamMachineResource);
        }

        public GameObject CreateBomb(Vector2 position)
        {
            GameObject gameObject = Object.Instantiate(_bombPrefab, position, Quaternion.identity);
            gameObject.GetComponent<OutboundDisabler>().Construct(_cameraService);
            return gameObject;
        }

        public GameObject CreatePlayer(Vector2 position)
        {
            GameObject gameObject = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
            _playerTransform = gameObject.transform;
            gameObject.GetComponent<PlayerMover>().Construct(_cameraService, _inputService);
            return gameObject;
        }
        
        public GameObject CreateHud(MusicLevelScroller musicLevelScroller)
        {
            // NotImplemented
            return null;
        }

        public GameObject CreateGhost(Vector2 position, float speed)
        {
            GameObject gameObject = Object.Instantiate(_ghostPrefab, position, Quaternion.identity);
            MoveToPlayer moveToPlayer = gameObject.GetComponent<MoveToPlayer>();
            moveToPlayer.Construct(_playerTransform);
            moveToPlayer.Speed = speed;
            return gameObject;
        }

        public GameObject CreateLaserBeamMachine()
        {
            GameObject gameObject = Object.Instantiate(_laserBeamMachine);
            return gameObject;
        }
    }
}