using UnityEngine;
using _Project.Scripts.Coletables;

namespace _Project.Scripts.Player
{
    public class PlayerDefense : MonoBehaviour
    {
        private float _bonusShield;
        [SerializeField] private GameObject shieldProtection;

        // Propriedade publica para chegar se o shield esta ativo
        public bool IsShieldActive => _bonusShield > 0;

        private void Update()
        {
            // Se o shield estiver ativo, decrementamos o tempo dele
            if (_bonusShield > 0)
            {
                _bonusShield -= Time.deltaTime;
                // Chamamos o evento enviando o tempo de atualização restante
                GameEvents.ApplyShieldTimeChanged(_bonusShield);

                // Caso o shield acabe neste frame desativamos o escudo
                if (_bonusShield <= 0)
                {
                    _bonusShield = 0;
                    // garantimos que a ui receba o valor de 0 caso nao tenha mais shield
                    shieldProtection.SetActive(false);
                    GameEvents.ApplyShieldTimeChanged(0);
                }
            }
        }

        private void OnEnable() => GameEvents.EventAddedBonusShield += ActiveShieldProtection;

        private void OnDisable() => GameEvents.EventAddedBonusShield -= ActiveShieldProtection;

        private void ActiveShieldProtection(float bonus)
        {
            _bonusShield += bonus;
            shieldProtection.SetActive(_bonusShield > 0);
        }
    }
}