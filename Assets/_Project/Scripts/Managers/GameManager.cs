#region Using

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace _Project.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;
        [SerializeField] private GameObject gameBoards;
        #endregion

        #region Variables

        public bool gameOver;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Other Methods

        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void GameOver()
        {
            gameOver = true;
            gameBoards.SetActive(false);
        }

        #endregion
    }
}