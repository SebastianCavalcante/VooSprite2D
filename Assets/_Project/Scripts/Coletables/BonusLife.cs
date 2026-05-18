#region Using Statements


using _Project.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Project.Scripts.Coletables
{
    public class BonusLife : MonoBehaviour, IColetable, ICollisionDestroy
    {
        

        #region Variables
        
        [SerializeField] private GameObject pickupParticles;
        [SerializeField] private float minActiveBonus;
        [SerializeField] private float maxActiveBonus;
        private Animator _animator;
        #endregion

        private readonly int _playAnim = Animator.StringToHash("Play");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private float _currentTime;
        private void Start()
        {
            float timerDestroy = Random.Range(minActiveBonus, maxActiveBonus);
            _currentTime = timerDestroy;
            Destroy(gameObject, timerDestroy);
            
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 2)
            {
                _animator.SetBool(_playAnim, true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IPlayerCollision playerSounds))
            {
                playerSounds.PlayLife();
            }
        }

        #region Other Public Methods

        public void Pickup()
        {
            GameObject effect = Instantiate(pickupParticles, transform.position, Quaternion.identity);
            
            // Eviar uma vida ao player
            GameEvents.ApplyLife(1);
            DestroyObject();
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}