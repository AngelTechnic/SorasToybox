using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class MirrorPositionEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (!caster.IsUnitCharacter)
            {
                if (caster.SlotID == 0)
                {
                    int num;
                    int num2;
                    bool flag2 = stats.combatSlots.CanEnemiesSwap(caster.SlotID, 4, out num, out num2);
                    if (flag2)
                    {
                        bool flag3 = stats.combatSlots.SwapEnemies(caster.SlotID, num, 4, num2, false, "");
                        if (flag3)
                        {
                            exitAmount++;
                            return exitAmount > 0;
                        }
                    }
                }
                else
                {
                    if (caster.SlotID == 1)
                    {
                        int num3;
                        int num4;
                        bool flag5 = stats.combatSlots.CanEnemiesSwap(caster.SlotID, 3, out num3, out num4);
                        if (flag5)
                        {
                            bool flag6 = stats.combatSlots.SwapEnemies(caster.SlotID, num3, 3, num4, false, "");
                            if (flag6)
                            {
                                exitAmount++;
                                return exitAmount > 0;
                            }
                        }
                    }
                    else
                    {
                        if (caster.SlotID == 3)
                        {
                            int num5;
                            int num6;
                            bool flag8 = stats.combatSlots.CanEnemiesSwap(caster.SlotID, 1, out num5, out num6);
                            if (flag8)
                            {
                                bool flag9 = stats.combatSlots.SwapEnemies(caster.SlotID, num5, 1, num6, false, "");
                                if (flag9)
                                {
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                        }
                        else
                        {
                            if (caster.SlotID == 4)
                            {
                                int num7;
                                int num8;
                                bool flag11 = stats.combatSlots.CanEnemiesSwap(caster.SlotID, 0, out num7, out num8);
                                if (flag11)
                                {
                                    bool flag12 = stats.combatSlots.SwapEnemies(caster.SlotID, num7, 0, num8, false, "");
                                    if (flag12)
                                    {
                                        exitAmount++;
                                        return exitAmount > 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (caster.SlotID == 0)
                {
                    //Debug.Log("swap 0 with 4");
                    if (stats.combatSlots.SwapCharacters(caster.SlotID, 4, true, ""))
                    {
                        exitAmount++;
                        return exitAmount > 0;
                    }
                }
                else
                {
                    if (caster.SlotID == 1)
                    {
                        //Debug.Log("swap 1 with 3");
                        if (stats.combatSlots.SwapCharacters(caster.SlotID, 3, true, ""))
                        {
                            exitAmount++;
                            return exitAmount > 0;
                        }
                    }
                    else
                    {
                        if (caster.SlotID == 3)
                        {
                            //Debug.Log("swap 3 with 1");
                            if (stats.combatSlots.SwapCharacters(caster.SlotID, 1, true, ""))
                            {
                                exitAmount++;
                                return exitAmount > 0;
                            }
                        }
                        else
                        {
                            if (caster.SlotID == 4)
                            {
                                //Debug.Log("swap 4 with 0");
                                if (stats.combatSlots.SwapCharacters(caster.SlotID, 0, true, ""))
                                {
                                    exitAmount++;
                                    return exitAmount > 0;
                                }
                            }
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
