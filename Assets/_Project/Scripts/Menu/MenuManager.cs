using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Menu
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        private void Awake()
        {
            Instance = this;
        }

        public void LoadGameLevel()
        {
            SceneManager.LoadSceneAsync("GameLevel");
        }
    }
}