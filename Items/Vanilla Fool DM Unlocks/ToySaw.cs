using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class ToySaw
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect malfunctionByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            malfunctionByPrevious._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");
            malfunctionByPrevious._MultPreviousExitValueForEntry = true;

            ChangeMaxHealthByCurrentHealthEffect trimHealth = ScriptableObject.CreateInstance<ChangeMaxHealthByCurrentHealthEffect>();

            Ability hackOff = new Ability("ST_HackOff_A")
            {
                Name = "Hack Off",
                Description = "Remove all excess health from the Opposing enemy. Inflict Malfunction onto them equal to the amount removed.",
                Cost = [Pigments.Red, Pigments.Purple],
                AbilitySprite = ResourceLoader.LoadSprite("toysaw_hackoff.png"),
                Visuals = Visuals.Slash,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(trimHealth, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(malfunctionByPrevious, 1, Targeting.Slot_Front),
                ],
            };
            hackOff.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth), "Status_Malfunction"]);

            ExtraAbility_Wearable_SMS hackOffWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            hackOffWearable._extraAbility = hackOff.GenerateCharacterAbility();

            PerformEffect_Item toySaw = new PerformEffect_Item("ST_ToySaw_ID")
            {
                Item_ID = "ToySaw_SW",
                Name = "Toy Saw",
                Flavour = "\"You've done it now, you little twat.\"",
                Description = "Gain \"Hack Off\" as an extra ability, allowing you to cut enemies to size.",
                EquippedModifiers = [hackOffWearable],
                TriggerOn = TriggerCalls.Count,
                StartsLocked = true,
                ShopPrice = 7,
                Icon = ResourceLoader.LoadSprite("item_toysaw"),
                IsShopItem = true,
                OnUnlockUsesTHE = true,
            };

            //unlock this
            string achievementID = "SorasToybox_LongLiver_Antagonist_ACH";
            string unlockID = "SorasToybox_LongLiver_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(toySaw.item, new ItemModdedUnlockInfo(toySaw.Item_ID, ResourceLoader.LoadSprite("item_toysaw_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, toySaw.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [toySaw.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("LongLiver", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(toySaw.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_LongLiver", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("LongLiver_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Toy Saw.");
            }

        }
    }
}
