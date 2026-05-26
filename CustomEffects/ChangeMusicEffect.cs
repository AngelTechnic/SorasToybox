using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class ChangeMusicEffect : EffectSO
    {
        public string musEvent = "";
        public bool nobronzo = true;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            if (nobronzo) {
                foreach (EnemyCombat enemy in stats.EnemiesOnField.Values)
                {
                    if(enemy.Name == "Bronzo5_EN") return false;
                }
            }
            
            if (CombatManager.Instance._soundManager.CurrentCombatMusic != musEvent)
            {
                CombatManager.Instance._soundManager.StopCombatMusicTrack();
                CombatManager.Instance._soundManager.PlayCombatMusicTrack(musEvent);
            }

            return true;
        }
    }
}
