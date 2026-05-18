using UnityEngine;

namespace _Project.Scripts.Coletables
{
    public class ShieldEffect : MonoBehaviour, IColletableEffect
    {
        [SerializeField] private float minBonusShield;
        [SerializeField] private float maxBonusShield;
        
        public void AppyEffect()
        {
            float bonus = Random.Range(minBonusShield, maxBonusShield);
            GameEvents.ApllyBonus(bonus);
        }
    }
}