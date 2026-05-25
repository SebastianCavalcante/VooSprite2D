using _Project.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Coletables
{
    public class ColletableItem : MonoBehaviour
    {
        [Header("Visual Effects")] 
        [SerializeField] private GameObject pickupParticles;

        [Header("LifeTime Settings")] 
        [SerializeField] private float minActiveBonus;
        [SerializeField] private float maxActiveBonus;

        private Animator _animator;
        private float _currentTime;
        private readonly int _playerAnim = Animator.StringToHash("Play");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
           float timerDestroy = Random.Range(minActiveBonus, maxActiveBonus);
           _currentTime = timerDestroy;
           Destroy(gameObject, timerDestroy);
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;
            
            if (_currentTime <= 2f  && _animator != null)
            {
                _animator.SetBool(_playerAnim, true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
            {
                IPlayerCollision playerCollision = other.GetComponentInChildren<IPlayerCollision>() ?? other.GetComponentInParent<IPlayerCollision>();

                if (playerCollision != null)
                {
                    TriggerSoundFeedback(playerCollision);
                }
                if (pickupParticles != null)
                {
                    GameObject particle = Instantiate(pickupParticles, transform.position, Quaternion.identity);
                    Destroy(particle, 1);
                }
                
                // TODO chamar o efeito especifico life/tempo/escudo
                if (TryGetComponent(out IColletableEffect effect))
                {
                    effect.AppyEffect();
                }
                
                // Destroy o coletavel
                Destroy(gameObject);
            }
        }

        private void TriggerSoundFeedback(IPlayerCollision playerCollision)
        {
            if(GetComponent<LifeEffect>()) playerCollision.PlayLife();
            else if (GetComponent<ShieldEffect>()) playerCollision.PlayShield();
            else if (GetComponent<TimeEffect>()) playerCollision.PlayTime();
        }
    }
}