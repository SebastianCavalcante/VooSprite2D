using _Project.Scripts.Coletables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private GameObject gameBoards;

        public bool gameOver;

        private void Awake()
        {
            Instance = this;
        }
        
        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void GameOver()
        {
            if (gameOver) return; // Evita disparar o evento varias vezes
            gameOver = true;
            gameBoards.SetActive(false);
            // Arquitetura senior. Em vez de caçar o UI Manager na cena nós avisamos o jogo
            GameEvents.ApplyGameOverTriggered("Game Over Your Time Is Up");
            
        }
    }
}