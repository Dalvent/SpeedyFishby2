using Code.Tools;
using UnityEngine;

namespace Code.Obsticles
{
    public abstract class PooledFacade<TPooledFacade> : MonoBehaviour, IPooled where TPooledFacade : PooledFacade<TPooledFacade>
    {
        private Pool<TPooledFacade> _pool;
        
        public void Construct(Pool<TPooledFacade> pool)
        {
            _pool = pool;
        }
        
        public void Return()
        {
            _pool.Return((TPooledFacade)this);
        }

        private void OnDestroy()
        {
            Return();
        }
    }
}