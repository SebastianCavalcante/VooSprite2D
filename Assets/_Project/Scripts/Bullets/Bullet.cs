using UnityEngine;
using UnityEngine.Pool; // Adicionamo o namespace nativo de Pooling da unity

namespace _Project.Scripts.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        public float speed;
        public Rigidbody2D rb;
        public int totalShots;

        //Guarda a referencia do pool que gerencia esta bala
        protected IObjectPool <Bullet> objectPool;

        // Flag Senior  para evitar dupla devolução no mesmo frame
        private bool _isReleased;
        //Método que o shooter/Manager vai usar para configurar o pool na bala
        public void SetPool(IObjectPool<Bullet> pool)
        {
            objectPool = pool;
        }

        // Toda vez que  a bala sair do Pool resetamos a flag no OnEnable nativo
        protected virtual void OnEnable()
        {
            _isReleased = false;
        }
        
        //Método senior: em vez de Destroy(gameObject) a bala se desativa e volta pro pool
        protected virtual void ReleaseToPool()
        {
            if (_isReleased) return; // Se ja devolvida neste frame, ignora qualquer chamada extra
            
            if (objectPool != null)
            {
                _isReleased = true; // Marcamos como devolvida imediatamente
                objectPool.Release(this);
            }
            else
            {
                // FallBack caso a bala tenha sido colodaca na cena manualmente
                Destroy(gameObject);
            }
        }
        
        public virtual void OnFire()
        {
        }
        
        
    }
}