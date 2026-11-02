using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox
{
    public class ForceTurnCleanupAction : CombatAction
    {
        public IUnit Unit;
        public ForceTurnCleanupAction(IUnit unit)
        {
            Unit = unit;
        }
        public override IEnumerator Execute(CombatStats stats)
        {
            if (Unit == null) yield break;
            Unit.SimpleSetStoredValue("Shortwave_ForcedTurn_A", 0);
            yield return null;
        }
    }
}
