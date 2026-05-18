#region Using Statements

using _Project.Scripts.Bullets;
using _Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool; // NameSpace essencial para o pooling nativo

#endregion

namespace _Project.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private float fireRange;
        [SerializeField] private Bullet bulletsPrefab;  // Mudamos de GameObjec para Bullet / obtendo ganho de tipagem estatica aqui
        [SerializeField] private Transform firePosition;
        
        private PlayerSounds _playerSounds;
        private IObjectPool <Bullet> _bulletPool;
        
        //Configurações opcionais de otimização
        [SerializeField] private int defautlCapacity = 20;
        [SerializeField] private int maxPoolSize = 50;
        
        
        #endregion

        private void Awake()
        {
            _playerSounds = GetComponent<PlayerSounds>();
            
            // Iniciazação Senior do ObjectPool nativo da unity 6
            _bulletPool = new ObjectPool<Bullet>
            ( createFunc: CreateBullet,
                actionOnGet: OnTakeBulletFromPool,
                actionOnRelease: OnReturnBulletToPool,
                actionOnDestroy: OnDestroyBullet,
                collectionCheck: true, // evita que o objeto seja devolvido 2 vezes
                defaultCapacity: defautlCapacity,
                maxSize : maxPoolSize
                );
        }
        
        #region Update Methods
        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.gameOver) return;
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                _playerSounds.PlayFire();
                // Em vez de instanciarmos pedimos a bala ao pool
                Bullet currentBullet = _bulletPool.Get();
                
                // Posicionamos e rotacionamos a bala que veio do pool 
                
               currentBullet.transform.position = firePosition.position;
               currentBullet.transform.rotation = transform.rotation;
            }
        }
        
        #endregion
        

        private Bullet CreateBullet()
        {
            // Cria o objeto fisicamente na cena
            Bullet bulletInstance = Instantiate(bulletsPrefab);
            
            // Injeção de dependencia: passamos a refenrecia deste pool para a bala
            bulletInstance.SetPool(_bulletPool);
            return bulletInstance;
        }
        
        private void OnTakeBulletFromPool(Bullet bullet)
        {
            // Ativa o objeto para uso na cena
            bullet.gameObject.SetActive(true);
        }
        
        private void OnReturnBulletToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        
        private void OnDestroyBullet(Bullet bullet)
        {
            // Verificação senior: se o objeto ja foi destruido pelo encerramento da cena nao tentamos destruir ele!
            if (bullet == null) return;
            
            // Limpesa fisica caso ultrapasse  o limite de pool Size
            Destroy(bullet.gameObject);
        }
        
        #region Methods of Help
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePosition.position, fireRange);
        }
        #endregion
    }
}