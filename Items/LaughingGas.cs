using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class LaughingGas
    {
        public static void Add()
        {
            StatusEffect_ApplyRestrictor_Effect permanentEcstasy = ScriptableObject.CreateInstance<StatusEffect_ApplyRestrictor_Effect>();
            permanentEcstasy._Status = StatusField.GetCustomStatusEffect("Ecstasy_ID");

            DirectDeathEffect overdose = ScriptableObject.CreateInstance<DirectDeathEffect>();

            StatusEffectCheckerEffect hasEcstasy = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasEcstasy._status = StatusField.GetCustomStatusEffect("Ecstasy_ID");

            PerformEffect_Item laughingGas = new PerformEffect_Item("ST_LaughingGas_ID", null, false)
            {
                Item_ID = "LaughingGas_SW",
                Name = "Laughing Gas",
                Flavour = "\"Enjoy the last ten minutes of your life!\"",
                Description = "On turn start, gain 1 permanent Ecstasy; if you have 5 or more Ecstasy, die.",
                Icon = ResourceLoader.LoadSprite("item_laughinggas"),
                TriggerOn = TriggerCalls.OnTurnStart_Early,
                Effects =
                [
                    Effects.GenerateEffect(hasEcstasy, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(overdose, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(permanentEcstasy, 1, Targeting.Slot_SelfSlot),
                ],
                IsShopItem = true,
                ShopPrice = 8,
                StartsLocked = true,
            };

            //unlock this
            string achievementID = "SorasToybox_Thype_Antagonist_ACH";
            string unlockID = "SorasToybox_Thype_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(bigSpoon.item, new ItemModdedUnlockInfo(laughingGas.Item_ID, ResourceLoader.LoadSprite("item_laughinggas_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, laughingGas.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [laughingGas.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Thype", unlockData);


            ModdedAchievements unlockAchievement = new ModdedAchievements("Entrenching Tool", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Thype", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");
        }
    }
}
