using UnityEngine;

namespace _Project.Scripts.Coletables
{
    public class BulletEffect : MonoBehaviour, IColletableEffect
    {
        [SerializeField] private int bulletIndex;
        
        public void AppyEffect()
        {
            GameEvents.ApplyChangeWeapon(bulletIndex);
        }
    }
}