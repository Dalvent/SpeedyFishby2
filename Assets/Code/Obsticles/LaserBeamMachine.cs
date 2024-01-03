using System.Collections.Generic;
using Code.Obsticles.LaserBeamState;
using UnityEngine;

namespace Code.Obsticles
{
    public class LaserBeamMachine : MonoBehaviour
    {
        private readonly Queue<ILaserBeamState> _statesQueue = new();
        private ILaserBeamState _currentState;

        private IPooled _pooled;
    
        public GameObject LaserObject;

        public void Awake()
        {
            _pooled = GetComponent<IPooled>();
        }

        private void Update() =>
            _currentState?.Update(this);

        public void AddStates(IEnumerable<ILaserBeamState> states)
        {
            _statesQueue.Clear();
            foreach (ILaserBeamState state in states)
                _statesQueue.Enqueue(state);
            NextState();
        }
    
        public void NextState()
        {
            _currentState?.Exit(this);
            if (_statesQueue.Count == 0)
            {
                RemoveFromScene();
                return;
            }
        
            _currentState = _statesQueue.Dequeue();
            _currentState.Enter(this);
        }

        private void RemoveFromScene()
        {
            if(_pooled != null)
                _pooled.Return();
            else
                Destroy(gameObject);
        }

        public void ShowLaser() => 
            LaserObject.SetActive(true);

        public void HideLaser() => 
            LaserObject.SetActive(false);
    }
}