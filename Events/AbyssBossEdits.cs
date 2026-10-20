using HarmonyLib;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Events
{
    [HarmonyPatch]
    public class AbyssBossEdits
    {
        public static void Add()
        {
            //this stuff doesn't actually work on my end so millie will have to add these values herself
            //if she wants to at least 
            FuckingDeadEffectCondition mdk = ScriptableObject.CreateInstance<FuckingDeadEffectCondition>();

            GameBoolDataSetEffect setNobodyHTBool = ScriptableObject.CreateInstance<GameBoolDataSetEffect>();
            setNobodyHTBool._VariableName = "NobodyKillForHeliotrope";
            setNobodyHTBool._ValueSet = true;

            LoadedAssetsHandler.GetEnemy("Nobody_BOSS").exitEffects.AddToArray<EffectInfo>(Effects.GenerateEffect(setNobodyHTBool, 1, Targeting.Slot_SelfSlot, mdk));
            LoadedAssetsHandler.GetEnemy("TrueNobody_BOSS").exitEffects.AddToArray<EffectInfo>(Effects.GenerateEffect(setNobodyHTBool, 1, Targeting.Slot_SelfSlot, mdk));

            GameBoolDataSetEffect setKatalixiHTBool = ScriptableObject.CreateInstance<GameBoolDataSetEffect>();
            setKatalixiHTBool._VariableName = "KatalixiKillForHeliotrope";
            setKatalixiHTBool._ValueSet = true;

            LoadedAssetsHandler.GetEnemy("Nolocimes_Batretne_BOSS").exitEffects.AddToArray<EffectInfo>(Effects.GenerateEffect(setKatalixiHTBool, 1, Targeting.Slot_SelfSlot, mdk));

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[EVENTS] Did a little trolling.");
            }
        }
    }
}
