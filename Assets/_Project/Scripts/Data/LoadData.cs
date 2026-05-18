#region Using Statements

using UnityEngine;

#endregion

namespace _Project.Scripts.Data
{
    public class LoadData : MonoBehaviour
    {
        #region Singleton

        public static LoadData Instance;
        
        #endregion

        #region Initialization

        private void Awake()
        {
            Instance = this;
        }

        public int LoadHightScore()
        {
            return PlayerPrefs.GetInt("HightScore");
        }

        #endregion
    }
}