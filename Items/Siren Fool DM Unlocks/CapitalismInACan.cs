using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class CapitalismInACan
    {
        public static void Add()
        {
            GainPlayerCurrencyEffect gimmeMoney = ScriptableObject.CreateInstance<GainPlayerCurrencyEffect>();

            LosePlayerCurrencyEffect loseyMoney = ScriptableObject.CreateInstance<LosePlayerCurrencyEffect>();

            DoublePerformEffect_Item capitalismInACan = new("ST_Capitalism_ID")
            {
                Icon = ResourceLoader.LoadSprite("item_capitalisminacan"),
                Item_ID = "CapitalismInACan_SW",
                Name = "Capitalism-in-a-Can",
                Flavour = "\"The rich get richer, the poor get f%#!ed\"",
                Description = "Gain 25 at the start of a fight. Lose 1 coin each time anything acts. Destroyed at the end of combat.",
                ConsumeOnTrigger = TriggerCalls.OnCombatEnd,
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects = [
                    Effects.GenerateEffect(gimmeMoney, 25, Targeting.Slot_SelfSlot),
                    ],
                DoesPopUpInfo = false,
                SecondaryTriggerOn = [TriggerCalls.OnAnyAbilityUsed],
                SecondaryEffects = [
                    Effects.GenerateEffect(loseyMoney, 1,Targeting.Slot_SelfSlot),
                    ],
                SecondaryDoesPopUpInfo = false,
                StartsLocked = true,
                IsShopItem = true,
                OnUnlockUsesTHE = false,
                ShopPrice = 1,
            };

            //Unlock this
            string achievementID = "SorasToybox_Igor_Antagonist_ACH";
            string unlockID = "SorasToybox_Igor_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(capitalismInACan.item, new ItemModdedUnlockInfo(capitalismInACan.Item_ID, ResourceLoader.LoadSprite("item_capitalisminacan_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, capitalismInACan.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [capitalismInACan.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Igor_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(capitalismInACan.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Igor", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Igor_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added Capitalism-in-a-Can.");
            }
        }
    }
}
