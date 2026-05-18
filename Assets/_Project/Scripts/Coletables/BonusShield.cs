#region Usings Lybraries

using _Project.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Project.Scripts.Coletables
{
    public class BonusShield : MonoBehaviour, IColetable, ICollisionDestroy
    {
        #region Variables

        [Header("Shild Settings")] [Tooltip("Reference the pickup particles")] [SerializeField]
        private GameObject pickupParticles;

        [Tooltip("Min value for bonus")] [SerializeField]
        private float minBonusShield;

        [Tooltip("Max value for bonus")] [SerializeField]
        private float maxBonusShield;

        
        [SerializeField] private float minActiveBonus;
        [SerializeField] private float maxActiveBonus;
        private float _bonusShield;
        private Animator _animator;
        private readonly int _playAnim = Animator.StringToHash("Play");
        private float _currentTime;

        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void Start()
        {
            _bonusShield = Random.Range(minBonusShield, maxBonusShield);
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
            if (other.gameObject.TryGetComponent(out IPlayerCollision playerCollision))
            {
                playerCollision.PlayShield();
            }
        }

        #endregion

        #region Overrides Methods

        // Logic picked bonus shild
        public void Pickup()
        {
            GameObject effect = Instantiate(pickupParticles, transform.position, Quaternion.identity);
            GameEvents.ApllyBonus(_bonusShield);
            DestroyObject();
        }

        // Logic destroy the bonus shild
        public void DestroyObject()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}