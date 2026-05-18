#region Using Statements

using UnityEngine;

#endregion

namespace _Project.Scripts.Data
{
    public class SaveData : MonoBehaviour
    {
        #region Singleton

        public static SaveData Instance;

        #endregion

        #region Initialization

        private void Awake()
        {
            Instance = this;
        }
        
        public void SaveHightScore(int score)
        {
           int oldScore =  PlayerPrefs.GetInt("HightScore");
           if (score > oldScore) PlayerPrefs.SetInt("HightScore", score); 
        }

        #endregion
    }
}