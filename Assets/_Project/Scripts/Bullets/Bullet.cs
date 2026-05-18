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
        
        //Método que o shooter/Manager vai usar para configurar o pool na bala
        public void SetPool(IObjectPool<Bullet> pool)
        {
            objectPool = pool;
        }
        
        //Método senior: em vez de Destroy(gameObject) a bala se desativa e volta pro pool
        protected virtual void ReleaseToPool()
        {
            if (objectPool != null)
            {
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