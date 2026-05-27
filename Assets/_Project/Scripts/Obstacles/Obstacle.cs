using UnityEngine;
using _Project.Scripts.Interfaces;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Obstacles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Obstacle : MonoBehaviour, IObstacle, ICollisionDestroy
    {
        private Rigidbody2D _rb;
        private int _obstacleLives;
        private int _randomPowerUp;
        
        [SerializeField] private int minLives;
        [SerializeField] private int maxLives;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float minSpeedSpin;
        [SerializeField] private float maxSpeedSpin;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private GameObject[] powerUpsDrops;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
           
            int randomLives = Random.Range(minLives, maxLives);  // random Lives
            float randomSpeed = Random.Range(minSpeed, maxSpeed); // Random move speed
            float randomSpeedSpin = Random.Range(minSpeedSpin, maxSpeedSpin); // Random Spin
            _randomPowerUp = Random.Range(0, powerUpsDrops.Length); //Random Power Up to drop
            
            Vector2 randomDirection = Random.insideUnitCircle;

            _obstacleLives = randomLives;
            _rb.AddForce(randomDirection * randomSpeed);
            _rb.AddTorque(randomSpeedSpin);
        }

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

        private void CreatePowerUp()
        {
            Instantiate(powerUpsDrops[_randomPowerUp], transform.position, Quaternion.identity);
        }
    }
}