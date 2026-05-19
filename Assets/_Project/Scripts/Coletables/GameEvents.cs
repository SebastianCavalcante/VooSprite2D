#region Using Statements

using System;

#endregion


namespace _Project.Scripts.Coletables
{
    public static class GameEvents
    {
        #region Events

        public static event Action<float> EventAddedBonusShield;
        public static event Action<int> EventAddLife;
        public static event Action<float> EventShieldTimeChanged;
        public static event Action<float> EventAddedBonusTime;
        public static event Action<float> EventReserveTimeChaged;

        // Novos eventos Senior
        public static event Action<float> EventGameTimeChanged;
        public static event Action<string> EventGameOverTriggered;
        
        
        #endregion

        #region Public Events CallBacks

        public static void ApllyBonus(float bonus) => EventAddedBonusShield?.Invoke(bonus);
        public static void ApplyLife(int bonus) => EventAddLife?.Invoke(bonus);
        
        public static void ApplyShieldTimeChanged(float bonus) => EventShieldTimeChanged?.Invoke(bonus);
        
        public static void ApplyBonusTime(float bonus) => EventAddedBonusTime?.Invoke(bonus);
        
        public static void ApplyReserveTimeChanged(float bonus) => EventReserveTimeChaged?.Invoke(bonus);
        
        
        // Novos metodos auxiliares
        public static void ApplyGameTimeChanged(float time) => EventGameTimeChanged?.Invoke(time);
        public static void ApplyGameOverTriggered(string triggered) => EventGameOverTriggered?.Invoke(triggered);

        #endregion
    }
}