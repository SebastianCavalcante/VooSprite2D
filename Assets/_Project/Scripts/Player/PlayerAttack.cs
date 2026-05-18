#region Using Statements

using _Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace _Project.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private float fireRange;
        [SerializeField] private GameObject[] bullets;
        [SerializeField] private Transform firePosition;
        private PlayerSounds _playerSounds;
        #endregion

        private void Awake()
        {
            _playerSounds = GetComponent<PlayerSounds>();
        }

        #region Update Methods
        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.gameOver) return;
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                _playerSounds.PlayFire();
                GameObject currentBullet = Instantiate(bullets[0], firePosition.position, Quaternion.identity);
                currentBullet.GetComponent<Transform>().rotation = transform.rotation;
            }
        }
        
        #endregion
        
        #region Methods of Help
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePosition.position, fireRange);
        }
        #endregion
    }
}