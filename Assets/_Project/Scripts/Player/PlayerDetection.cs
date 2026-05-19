#region MyRegion

using _Project.Scripts.Coletables;
using _Project.Scripts.Interfaces;
using UnityEngine;
#endregion
namespace _Project.Scripts.Player
{
    public class PlayerDetection : MonoBehaviour, IObstacle
    {
        #region Components

        private PlayerLife _playerLife;
        
        #endregion

        private void Awake()
        {
            _playerLife = GetComponent<PlayerLife>();
        }

        #region Unity Methods

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IObstacle>(out IObstacle obstacle)) // Detecta a colisão com rochas 
            {
                obstacle.Impact();
            }

            if (other.gameObject.CompareTag("Boards"))
            {
                Impact();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<ColletableItem>(
                    out ColletableItem coletable)) // Detecta a colisão com itens coletaveis
            {
            }
        }

        #endregion
        
        #region Interface Implementation
        public void Impact()
        {
            _playerLife.DecreaseLife();
        }
        
        #endregion
    }
}