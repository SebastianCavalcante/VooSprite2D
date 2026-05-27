using UnityEngine;
using _Project.Scripts.Coletables;
using _Project.Scripts.Managers;

namespace _Project.Scripts.Player
{
    public class PlayerLife : MonoBehaviour
    {
        private PlayerSounds _playerSounds;
        private SpriteRenderer _playerSprite;
        [SerializeField] private GameObject shieldObj;

        private void Awake()
        {
            _playerSounds = GetComponent<PlayerSounds>();
            _playerSprite = GetComponent<SpriteRenderer>();
        }

        [SerializeField] private int life;
        [SerializeField] private GameObject explosionEffect;

        public int Life
        {
            get => life;
            set => life = value;
        }

        private void Start()
        {
            Life = life;
        }

        private void OnEnable() => GameEvents.EventAddLife += UpdateLife;

        private void OnDisable() => GameEvents.EventAddLife -= UpdateLife;

        private void UpdateLife(int value)
        {
            life += value;
            CheckAlertLife();
            UiManager.Instance.UpdateLifeCountEvent(life);
        }

        public void DecreaseLife()
        {
            PlayerDefense defense = GetComponent<PlayerDefense>();

            if (defense != null && defense.IsShieldActive)
            {
                // Se o escudo estiver ativo interrompemos o script aqui! e assim nao tomamos dano
                return;
            }

            _playerSounds.PlayHit();
            life -= 1;
            CheckAlertLife();
            UiManager.Instance.UpdateLifeCountEvent(life);
            PlayerStatus();
        }

        private void PlayerStatus()
        {
            if (life <= 0)
            {
                _playerSounds.PlayDeath();

                GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
                GameManager.Instance.GameOver();
                gameObject.GetComponent<Collider2D>().enabled = false;
                shieldObj.gameObject.SetActive(false);
                _playerSprite.enabled = false;
                UiManager.Instance.UpdateLifeCountEvent(life);
                string gameOverCase = "You Are Dead";
                UiManager.Instance.ShowGameOverCase(gameOverCase);
                Destroy(gameObject, 1);
            }
        }

        public void PlayerDestroy()
        {
            _playerSounds.PlayDeath();
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            shieldObj.gameObject.SetActive(false);
            _playerSprite.enabled = false;
            life = 0;
            UiManager.Instance.UpdateLifeCountEvent(life);
            Destroy(gameObject, 1);
        }

        private void CheckAlertLife()
        {
            _playerSounds.PlayAlertExplosion(life == 1);
        }
    }
}