using UnityEngine;

namespace _Project.Scripts.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        public float speed;
        public Rigidbody2D rb;
        public int totalShots;

        public virtual void OnFire()
        {
        }
        
        
    }
}