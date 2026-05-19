using _Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _camera;
        private Rigidbody2D _rb;
        private PlayerSounds _playerSounds;
        
        [Header("Movement Settings")]
        [SerializeField] private float speed;
        [SerializeField] private float maxSpeed;
        
        [Header("Visual Effects")]
        [SerializeField] private GameObject propellant;
        [SerializeField] private GameObject propellantParticles;

        // Variaveis de controle interno(dados lidos no update e aplicados no fixedupdate
        private Vector2 _moveDirection;
        private bool _isTrhusting;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerSounds = GetComponent<PlayerSounds>();
        }

        private void Start()
        {
            _camera = Camera.main;
            // Impulso inicial para dar dinamismo ao jogo
            _rb.AddForce(transform.up * 1, ForceMode2D.Impulse);
        }
        
        private void Update()
        {
            if (GameManager.Instance.gameOver)
            {
                StopThruster();
                return;
            }

            HandleInputs(); 
            
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.gameOver)return;
            ApplyPhysicsMovement();
        }

        private void HandleInputs()
        {
            // Som do propulsor ao precionar o botao
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _playerSounds.StartThrusterSound();
            }
            
            // Segurando o botao: Calcula direção e ativa efeitos visuais
            if (Mouse.current.leftButton.isPressed)
            {
                _isTrhusting = true;
                Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
                _moveDirection = (mousePosition - transform.position).normalized;
                
                //Rotação visual imediata e suave voltada para o mouse
                transform.up = _moveDirection;
                
                propellant.SetActive(true);
                propellantParticles.SetActive(true);
            }
            
            //Soltou o botão: Cessa a aceleração da nave
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                StopThruster();
            }
        }

        private void ApplyPhysicsMovement()
        {
            if (_isTrhusting)
            {
                // Aplica a velocidade constante na direção calculada
                _rb.linearVelocity = _moveDirection * speed;
                
                // Reseta a fisica angular, fisica para travar rotações indesejadas causadas por batidas fisicas
                // Já que controlamos a rotação atraves do transform.up via script
                _rb.angularVelocity = 0; 
                
                // Limita a velocidade máxima de forma elegante
                if (_rb.linearVelocity.magnitude > maxSpeed)
                {
                    _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
                }
            }
        }

        private void StopThruster()
        {
            _isTrhusting = false;
            _playerSounds.StopThrusterSound();
            propellant.SetActive(false);
            propellantParticles.SetActive(false);
        }
    }
}