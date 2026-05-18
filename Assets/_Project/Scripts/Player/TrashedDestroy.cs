#region Using Statements

using UnityEngine;

#endregion

namespace _Project.Scripts.Player
{
    public class TrashedDestroy : MonoBehaviour
    {
        #region Unity Methods

        private void Start()
        {
            Destroy(gameObject, 2f);
        }

        #endregion
    }
}