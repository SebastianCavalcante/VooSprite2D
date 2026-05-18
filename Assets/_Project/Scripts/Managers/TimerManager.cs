#region Using

using _Project.Scripts.Coletables;
using _Project.Scripts.Player;
using UnityEngine;

#endregion

namespace _Project.Scripts.Managers
{
    public class TimerManager : MonoBehaviour
    {
        #region Singleton

        public static TimerManager Instance;

        #endregion

        #region Variables

        public float timer;
        public float reserverTimer;

        private GameObject _player;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Instance = this;
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            UpdateTimer();
        }

        #endregion

        #region Other Methods

        public void UpdateTimer()
        {
            if (GameManager.Instance.gameOver) return;

            timer -= Time.deltaTime;

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
                    string gameOverCase = "Game Over Your Time Is Up";
                    UiManager.Instance.ShowGameOverCase(gameOverCase);
                    _player.GetComponent<PlayerLife>().PlayerDestroy();
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

        #endregion
    }
}