using UnityEngine;

namespace Code.Infrastructure
{
    public interface IResourceLoader
    {
        
    }

    public class ResourceLoader : IResourceLoader
    {
        private GameObject _player;
        private GameObject _bomb;
        private GameObject _background;
        
        private const string BombResource = "Bomb";
        private const string BackgroundResource = "Background";
        private const string PlayerResource = "Player";

        private void Load()
        {
            _player = Resources.Load<GameObject>(PlayerResource);
            _bomb = Resources.Load<GameObject>(BombResource);
            _background = Resources.Load<GameObject>(BackgroundResource);
        }
        
        
    }
}