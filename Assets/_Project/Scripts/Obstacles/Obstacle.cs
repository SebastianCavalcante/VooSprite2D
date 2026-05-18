#region Using Statements

using _Project.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace _Project.Scripts.Obstacles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Obstacle : MonoBehaviour, IObstacle, ICollisionDestroy
    {
        private Rigidbody2D _rb;
        private int _obstacleLives;
        [SerializeField]private AudioSource _audioSource;
        [SerializeField] private int minLives;
        [SerializeField] private int maxLives;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeedSpin;
        [SerializeField] private float maxSpeedSpin;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private AudioClip[] impactSounds;
        [SerializeField] private GameObject[] powerUpsDrops;

        private int _randomPowerUp;
        private int _currentSoundIndex;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            
        }

        private void Start()
        {
            
            #region Random Values

            int randomLives = Random.Range(minLives, maxLives);
            _randomPowerUp = Random.Range(0, powerUpsDrops.Length);
            float randomSpeed = Random.Range(minSpeed, maxSpeed);
            float randomSpeedSpin = Random.Range(minSpeedSpin, maxSpeedSpin);
            Vector2 randomDirection = Random.insideUnitCircle;
            _currentSoundIndex = Random.Range(0,  impactSounds.Length);
            #endregion

            _obstacleLives = randomLives;
            _rb.AddForce(randomDirection * randomSpeed);
            _rb.AddTorque(randomSpeedSpin);
        }

        #region Other Methods

        public void Impact()
        {
            OnDamage();
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IObstacle>(out IObstacle obstacle))
            {
                obstacle.Impact();
            }
            Vector2 contactPoint = other.GetContact(0).point;
            //PlayImpactSound();
            GameObject impact = Instantiate(impactEffect, contactPoint, Quaternion.identity);
            Destroy(impact, 1f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullets"))
            {
                OnDamage();
            }
        }

        private void OnDamage()
        {
            _obstacleLives--;
            if (_obstacleLives == 0)
            {
                CreatePowerUp();
                DestroyObject();
            }
        }

        private void PlayImpactSound()
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(impactSounds[_currentSoundIndex]);
            }
            
        }

        private void CreatePowerUp()
        {
            Instantiate(powerUpsDrops[_randomPowerUp], transform.position, Quaternion.identity);
        }

        #endregion
    }
}