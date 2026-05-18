#region Using Statements

using _Project.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Project.Scripts.Coletables
{
    public class BonusTime : MonoBehaviour, IColetable, ICollisionDestroy
    {
        #region Variables

        [SerializeField] private GameObject pickupParticles;
        [SerializeField] private float maxBonusTime;
        [SerializeField] private float minBonusTime;
        [SerializeField] private float minActiveBonus;
        [SerializeField] private float maxActiveBonus;
        private float _bonusTime;
        private Animator _animator;
        private readonly int _playAnim = Animator.StringToHash("Play");
        private float _currentTime;
        #endregion
        
        #region Initialization

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Start()
        {
            _bonusTime =  Random.Range(minBonusTime, maxBonusTime);
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

        #endregion

        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IPlayerCollision playerSounds))
            {
                playerSounds.PlayTime();
            }
        }

        #endregion
        
        #region Other Public Methods
        public void Pickup()
        {
            GameObject effect = Instantiate(pickupParticles, transform.position, Quaternion.identity);
            GameEvents.ApplyBonusTime(_bonusTime);
            DestroyObject();
        }
        
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
        
        #endregion
    }
}