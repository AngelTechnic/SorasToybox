using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class DriveItem
    {
        public static void Add()
        {
            FieldEffectCheckEffect amIConstricted = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
            amIConstricted._fields = [StatusField.Constricted];

            StatusEffect_Apply_Effect getOverclock = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getOverclock._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            TargetPerformEffectViaSubaction driveEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            driveEffects.effects =
            [
                Effects.GenerateEffect(amIConstricted, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(getOverclock, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
            ];

            FieldEffect_Apply_Effect getConstricted = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            getConstricted._Field = StatusField.Constricted;

            DoublePerformEffect_Item clivesWillToLive = new DoublePerformEffect_Item("ST_Drive_ID", null, false)
            {
                Item_ID = "ST_Drive_TW",
                Name = "Drive",
                Flavour = "\"Sometimes you're stuck in the ground, and you never even knew it.\"",
                Description = "On turn start, apply 1 Overclock to all party members that are standing in Constricted.\nOn turn end, apply 2 Constricted to this party member's slot.",
                Icon = ResourceLoader.LoadSprite("item_drive"),
                TriggerOn = TriggerCalls.OnTurnFinished,
                OnUnlockUsesTHE = false,
                IsShopItem = false,
                ShopPrice = 6,
                StartsLocked = true,
                Effects =
                [
                    Effects.GenerateEffect(getConstricted, 2, Targeting.Slot_SelfSlot),
                ],
                SecondaryTriggerOn = [TriggerCalls.OnTurnStart],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(driveEffects, 1, Targeting.Unit_AllAllies),
                ],
            };


            //unlock this
            string achievementID = "SorasToybox_Clive_Antagonist_ACH";
            string unlockID = "SorasToybox_Clive_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(clivesWillToLive.item, new ItemModdedUnlockInfo(clivesWillToLive.Item_ID, ResourceLoader.LoadSprite("item_drive_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, clivesWillToLive.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [clivesWillToLive.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Formosus", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Drive", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Clive", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Clive_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Drive.");
            }
        }
    }
}
