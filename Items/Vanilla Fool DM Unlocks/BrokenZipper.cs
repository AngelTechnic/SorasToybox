using BrutalAPI.Items;
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class BrokenZipper
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS overtunedWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            overtunedWearable._extraPassiveAbility = Passives.GetCustomPassive("ST_Overtuned_PA");

            StatusEffect_Apply_Effect getWeakness = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getWeakness._Status = StatusField.GetCustomStatusEffect("Weakness_ID");

            PerformEffect_Item brokenZipper = new PerformEffect_Item("ST_BrokenZipper_ID")
            {
                Item_ID = "BrokenZipper_TW",
                Name = "Broken Zipper",
                Flavour = "\"Nothing you ever do will be good enough for them.\"",
                Description = "This party member is now Overtuned. At the start of each turn, gain 3 Weakness.",
                EquippedModifiers = [overtunedWearable],
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(getWeakness, 3, Targeting.Slot_SelfSlot),
                ],
                Icon = ResourceLoader.LoadSprite("item_zipper", null, 32, null),
                IsShopItem = false,
                ShopPrice = 0,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
            };

            //Unlock this
            string achievementID = "SorasToybox_Burnout_Antagonist_ACH";
            string unlockID = "SorasToybox_Burnout_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(brokenZipper.item, new ItemModdedUnlockInfo(brokenZipper.Item_ID, ResourceLoader.LoadSprite("item_zipper_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, brokenZipper.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [brokenZipper.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Burnout", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(brokenZipper.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Burnout", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Burnout_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Broken Zipper.");
            }
        }
    }
}
