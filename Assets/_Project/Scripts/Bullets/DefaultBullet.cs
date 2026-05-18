using UnityEngine;

namespace _Project.Scripts.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DefaultBullet : Bullet
    {
        [SerializeField] private GameObject impactEffect;
        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;
        private float _timeToPool;

        private void Awake()
        {
            // Otimização Sênio:
            // Cache de componentes para evitar GetComponent  no update/trigger
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnEnable()
        {
            // Resetamos o estado da bala sempre que ela sai do Pool
            _timeToPool = 0;
            _spriteRenderer.enabled = true;
            _circleCollider2D.enabled = true;
        }

        private void Update()
        {
            _timeToPool += Time.deltaTime;
            if (_timeToPool >= 2)
            {
                ReleaseToPool();
                return; // Parar a execução no update imediatamente
            }
            
            // Movimentação física usando o Rigidbody2D cacheado da classe base Bullet
            rb.linearVelocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Cria o efeito visual do impacto da bala
            GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(impact, 0.2f);
            
            // Em vez de desativarmos aqui os componentes Sprite e Circle, apenas develvemos a bala pro Pool
            // O OnEnable se encarrega de resetar a bala e deixa tudo pronto para o proximo tiro
            ReleaseToPool();
        }
    }
}