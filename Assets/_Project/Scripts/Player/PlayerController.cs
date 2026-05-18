#region Using Statements

using _Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace _Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private Camera _camera;
        private Rigidbody2D _rb;
        private PlayerSounds _playerSounds;
        [SerializeField] private float speed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private GameObject propellant;
        [SerializeField] private GameObject propellantParticles;

        #endregion

        #region Initialization

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerSounds = GetComponent<PlayerSounds>();
        }

        private void Start()
        {
            _camera = Camera.main;
            _rb.AddForce(transform.up * 1, ForceMode2D.Impulse);
        }

        #endregion

        #region Update Methods

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && !GameManager.Instance.gameOver)
            {
                _playerSounds.StartThrusterSound();
            }
            
            if (Mouse.current.leftButton.isPressed && !GameManager.Instance.gameOver)
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
                Vector2 direction = (mousePosition - transform.position).normalized;
                transform.up = direction;

                _rb.linearVelocity = direction * speed;
                _rb.angularVelocity = 0; // Reseta a fisica de rotação "preciso estudar mais sobre isso"

                if (_rb.linearVelocity.magnitude > maxSpeed)
                {
                    _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
                }
               
                propellant.SetActive(true);
                propellantParticles.SetActive(true);
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                _playerSounds.StopThrusterSound();
                propellant.SetActive(false);
                propellantParticles.SetActive(false);
                
            }
        }

        #endregion
    }
}