using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstacles;
        [SerializeField] private Transform[] spawnPoints;
        int _totalObstacles;
        
        private void Update()
        {
            _totalObstacles = GameObject.FindGameObjectsWithTag("Obstacle").Length;
            if (_totalObstacles < 5)
            {
                int obstacleIndex = Random.Range(0, obstacles.Length);
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(obstacles[obstacleIndex],  spawnPoints[spawnIndex].position, Quaternion.identity);
            }
        }
    }
}