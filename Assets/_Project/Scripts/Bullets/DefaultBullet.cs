using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DefaultBullet : Bullet
    {
        [SerializeField] private GameObject impactEffect;

        private void Start()
        {
            Destroy(gameObject, 2f);
        }

        private void Update()
        {
            rb.linearVelocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(impact, 0.2f);
            Destroy(gameObject);
            
        }
    }
}