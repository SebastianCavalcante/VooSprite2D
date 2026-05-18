
using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class TimeScore : MonoBehaviour
    {
        public static TimeScore Instance;
        public int currentScore;
        private void Awake()
        {
            Instance = this;
        }

        private float _current;
        private void Update()
        {
            if (!GameManager.Instance.gameOver)
            {
                 _current +=  Time.deltaTime * 4;
                currentScore = Mathf.FloorToInt(_current);
            }
            else
            {
                SaveData.Instance.SaveHightScore(currentScore);
            }
        }
    }
}