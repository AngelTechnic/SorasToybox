using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace SorasToybox.MiscPatches
{
    [HarmonyPatch]
    public class FireBlockerPatch
    {
        [HarmonyPatch(typeof(OnFireFE_SO), nameof(OnFireFE_SO.OnEventCall_03))]
        [HarmonyPrefix]
        public static bool EarlyReturnIfFireproofPatch(FieldEffect_Holder holder, object sender, object args)
        {
            IUnit unit = sender as IUnit;
            if (unit.ContainsPassiveAbility("MadeOfFire"))
            {
                UnityEngine.Debug.Log("Fire Blocker Patch | Made Of Fire detected! interrupting action early");
                return false;
            }
            return true;
        }
    }
}
