
using _Project.Scripts.Coletables;
using _Project.Scripts.Player;
using UnityEngine;


namespace _Project.Scripts.Managers
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager Instance;
        
        public float timer;
        public float reserverTimer;

        private GameObject _player;
        
        private void Awake()
        {
            Instance = this;
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            UpdateTimer();
        }
        
        public void UpdateTimer()
        {
            if (GameManager.Instance.gameOver) return;

            timer -= Time.deltaTime;
            
            GameEvents.ApplyGameTimeChanged(timer);

            if (timer <= 0)
            {
                if (reserverTimer > 0)
                {
                    timer = reserverTimer;
                    reserverTimer = 0;
                    GameEvents.ApplyReserveTimeChanged(reserverTimer);
                }
                else
                {
                    timer = 0;
                    GameManager.Instance.GameOver();
                    if (_player != null && _player.TryGetComponent(out PlayerLife playerLife))
                    {
                        playerLife.PlayerDestroy();
                    }
                }
            }
        }

        private void OnEnable() => GameEvents.EventAddedBonusTime += AddedTimeBonus;
        private void OnDisable() => GameEvents.EventAddedBonusTime -= AddedTimeBonus;


        private void AddedTimeBonus(float bonus)
        {
            reserverTimer += bonus;
            GameEvents.ApplyReserveTimeChanged(reserverTimer);
        }
        
    }
}