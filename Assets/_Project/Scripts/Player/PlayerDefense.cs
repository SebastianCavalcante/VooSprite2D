#region Using Statements

using _Project.Scripts.Coletables;
using UnityEngine;

#endregion

namespace _Project.Scripts.Player
{
    public class PlayerDefense : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject shieldProtection;
        private float _bonusShield;
        public bool IsShieldActive => BonusShield > 0;

        #endregion

        #region Properties

        public float BonusShield
        {
            get => _bonusShield;
            private set => _bonusShield = value;
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            BonusShield = _bonusShield;
        }

        private void Update()
        {
            if (BonusShield > 0)
            {
                BonusShield -= Time.deltaTime;
                // Chamamos o evento enviando o tempo de atualização
                GameEvents.ApplyShieldTimeChanged(BonusShield);
            }
            else if (shieldProtection.activeSelf)
            {
                // garantimos que a ui receba o valor de 0 caso nao tenha mais shield
                ActiveShieldProtection(0);
                GameEvents.ApplyShieldTimeChanged(0);
            }
        }

        private void OnEnable()
        {
            GameEvents.EventAddedBonusShield += ActiveShieldProtection;
        }

        private void OnDisable()
        {
            GameEvents.EventAddedBonusShield -= ActiveShieldProtection;
        }

        #endregion
        
        #region Other Methods

        private void ActiveShieldProtection(float bonus)
        {
            BonusShield += bonus;
            shieldProtection.SetActive(bonus > 0);
        }

        #endregion
    }
}