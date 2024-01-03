using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Tools
{
    public class Pool<TMonoBehaviour> where TMonoBehaviour : MonoBehaviour
    {
        private readonly Func<TMonoBehaviour> _creator;
        public Stack<TMonoBehaviour> Existed = new();

        public Pool(Func<TMonoBehaviour> creator)
        {
            _creator = creator;
        }
        
        public void WarmUp(int count)
        {
            for (int i = Existed.Count; i < count; i++)
            {
                var monoBehaviour = _creator.Invoke();
                monoBehaviour.gameObject.SetActive(false);
                Existed.Push(monoBehaviour);
            }
        }
        
        public TMonoBehaviour Rent()
        {
            TMonoBehaviour monoBehaviour = Existed.Count == 0 ? _creator.Invoke() : Existed.Pop();
            monoBehaviour.gameObject.SetActive(true);
            return monoBehaviour;
        }

        public void Return(TMonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
            Existed.Push(monoBehaviour);
        }
    }
}