using System.Collections;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Player
{
    public class PlayerSounds : MonoBehaviour, IPlayerCollision
    {
        private  AudioSource _alertAudioSource;
        private Coroutine _alertCoroutine;
        private SpriteRenderer _playerSprite;
        [SerializeField] private AudioSource effectsAudioSource; 
        [SerializeField] private AudioSource fireAudioSource; 
        [SerializeField] private AudioSource thAudioSource; 
        [SerializeField] AudioClip alertSound;
        [SerializeField] AudioClip lifeSound;
        [SerializeField] AudioClip shieldSound;
        [SerializeField] AudioClip timeSound;
        [SerializeField] AudioClip fireSound;
        [SerializeField] private AudioClip thRuster;
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private AudioClip deathSound;
        

        private void Awake()
        {
            _alertAudioSource = GetComponent<AudioSource>();
            _playerSprite = GetComponent<SpriteRenderer>();
        }
        public void PlayLife()
        {
            effectsAudioSource.PlayOneShot(lifeSound);
        }

        public void PlayShield()
        {
            effectsAudioSource.PlayOneShot(shieldSound);
        }

        public void PlayTime()
        {
            effectsAudioSource.PlayOneShot(timeSound);
        }

        public void PlayFire()
        {
            fireAudioSource.PlayOneShot(fireSound);
        }

        public void PlayHit()
        {
            effectsAudioSource.PlayOneShot(hitSound);
        }

        public void PlayDeath()
        {
            effectsAudioSource.PlayOneShot(deathSound);
        }

        public void StartThrusterSound()
        {
            // Se o som ja estiver tocando nao fazemos nada
            if (thAudioSource.isPlaying) return;
            
            thAudioSource.clip = thRuster;
            thAudioSource.loop = true;
            thAudioSource.Play();
        }

        public void StopThrusterSound()
        {
            if (thAudioSource.isPlaying)
            {
                thAudioSource.Stop();
            }
        }
        
        public void PlayAlertExplosion(bool check)
        {
            if (check)
            {
                // Garantir que nao iniaciaremos duas coroutinas ao mesmo tempo
                if (_alertCoroutine == null)
                {
                    _alertCoroutine = StartCoroutine(AlertRoutine());
                }
            }
            else
            {
                if (_alertCoroutine != null)
                {
                    StopCoroutine(_alertCoroutine);
                    _alertCoroutine = null;
                    if (_alertAudioSource != null) _alertAudioSource.Stop();
                }
            }
        }

        private IEnumerator AlertRoutine()
        {
            while (true) // loop para quando a coroutina estiver ativa
            {
                Color32 critialColor = new Color32(255, 115, 115, 255);
                Color32 normalColor = new Color32(255, 255, 255, 255);
                _alertAudioSource.PlayOneShot(alertSound);
                gameObject.GetComponent<SpriteRenderer>().color = critialColor;
                yield return new WaitForSeconds(0.8f);
                gameObject.GetComponent<SpriteRenderer>().color = normalColor;
                yield return new WaitForSeconds(2f);
                
            }
        }
    }
}