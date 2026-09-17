using BrutalAPI.Items;
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class WeatherCycle
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getWarded = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getWarded._Status = StatusField.GetCustomStatusEffect("Warded_ID");

            StatusEffect_Apply_Effect getSealed = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getSealed._Status = StatusField.GetCustomStatusEffect("Sealed_ID");

            PerformEffect_Item weatherCycle = new PerformEffect_Item("ST_WeatherCycle_ID")
            {
                Name = "Weather Cycle",
                Flavour = "\"Global warming or global freezing? Decide, already!\"",
                Item_ID = "WeatherCycle_TW",
                Description = "At the start of your turn, gain 1 Sealed.\n50% to instead gain 1 Warded.",
                Icon = ResourceLoader.LoadSprite("item_weathercycle", null, 32, null),
                IsShopItem = false,
                ShopPrice = 0,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
                TriggerOn = TriggerCalls.OnTurnStart,
                DoesPopUpInfo = true,
                Effects =
                [
                    Effects.GenerateEffect(getSealed, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50)),
                    Effects.GenerateEffect(getWarded, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                ],

            };

            

            //Unlock this
            string achievementID = "SorasToybox_SmokeStacks_Antagonist_ACH";
            string unlockID = "SorasToybox_SmokeStacks_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(weatherCycle.item, new ItemModdedUnlockInfo(weatherCycle.Item_ID, ResourceLoader.LoadSprite("item_weathercycle_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, weatherCycle.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [weatherCycle.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("SmokeStacks", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(weatherCycle.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_SmokeStacks", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("SmokeStacks_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Weather Cycle.");  
            }
        }
    }
}
