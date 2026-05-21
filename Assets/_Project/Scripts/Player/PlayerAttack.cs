#region Using Statements

using System;
using System.Collections.Generic;
using _Project.Scripts.Bullets;
using _Project.Scripts.Coletables;
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
        [SerializeField] private Bullet[] bulletsPrefabs;  // Mudamos de GameObjec para Bullet / obtendo ganho de tipagem estatica aqui
        [SerializeField] private Transform firePosition;
        
        private PlayerSounds _playerSounds;
        private int _currentBulletIndex;
        
        //Configurações opcionais de otimização
        [SerializeField] private int defautlCapacity = 20;
        [SerializeField] private int maxPoolSize = 50;
        
        // Arquiteruta Senior: Um dicionario que armazena um pool para cada tipo de bala
        private Dictionary<int, IObjectPool <Bullet>> _poolsDictionary;
        
        #endregion

        private void Awake()
        {
            _playerSounds = GetComponent<PlayerSounds>();
            _poolsDictionary = new Dictionary<int, IObjectPool<Bullet>>();
            
            // Iniciazamos um Pool dedicado para cada prefab connfigurado no inspector
            for (int i = 0; i < bulletsPrefabs.Length; i++)
            {
                // Captura o escopo local para o delegate lamba
                int index = i;

                IObjectPool<Bullet> individualPool = new ObjectPool<Bullet>(
                    createFunc: () => CreateBullet(index), // passamos o index fixo deste pool
                    actionOnGet: OnTakeBulletFromPool,
                    actionOnRelease: OnReturnBulletToPool,
                    actionOnDestroy: OnDestroyBullet,
                    collectionCheck: true,
                    defaultCapacity: defautlCapacity,
                    maxSize: maxPoolSize
                    );
                _poolsDictionary.Add(index, individualPool);
            }
        }

        #region Update Methods
        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.gameOver) return;
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                _playerSounds.PlayFire(_currentBulletIndex);
                
                // Pega o Pool correspondende a arma  equipada atualmente
                IObjectPool<Bullet> currentPool = _poolsDictionary[_currentBulletIndex];
                
                // Solicita a bala correta do armario correto
                Bullet currentBullet = currentPool.Get();
                
                // Posicionamos e rotacionamos a bala que veio do pool 
                
               currentBullet.transform.position = firePosition.position;
               currentBullet.transform.rotation = transform.rotation;
            }
        }
        
        #endregion


      
        // Metodo de criação limpo sem switch e baseado no indice fixo do pool
        private Bullet CreateBullet(int bulletIndex)
        {
            // Cria o objeto fisicamente na cena
            Bullet bulletInstance = Instantiate(bulletsPrefabs[bulletIndex]);
            
            // Injeção de dependencia: Injeta  a referencia do pool especifico que é dono desta instancia de bala
            bulletInstance.SetPool(_poolsDictionary[bulletIndex]);
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
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePosition.position, fireRange);
        }

        private void OnEnable()
        {
            GameEvents.EventChangeWeapon += SelectBullet;
        }

        private void OnDisable()
        {
            GameEvents.EventChangeWeapon -=  SelectBullet;
        }

        private void SelectBullet(int bulletIndex)
        {
            if (bulletIndex >= 0 && bulletIndex < bulletsPrefabs.Length)
            {
                _currentBulletIndex = bulletIndex;
            }
        }
    }
}