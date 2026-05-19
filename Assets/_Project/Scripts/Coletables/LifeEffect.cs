using UnityEngine;

namespace _Project.Scripts.Coletables
{
    public class LifeEffect : MonoBehaviour, IColletableEffect
    {
        public void AppyEffect()
        {
            //Executa apena a ação de aplicar a vida
            GameEvents.ApplyLife(1);
        }
    }
}