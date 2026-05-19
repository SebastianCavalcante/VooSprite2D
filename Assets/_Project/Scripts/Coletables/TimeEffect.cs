using UnityEngine;

namespace _Project.Scripts.Coletables
{
    public class TimeEffect : MonoBehaviour, IColletableEffect
    {
        [SerializeField] private float minBonusTime;
        [SerializeField] private float maxBonusTime;
        public void AppyEffect()
        {
            float bonus =  Random.Range(minBonusTime, maxBonusTime);
            GameEvents.ApplyBonusTime(bonus);
        }
    }
}