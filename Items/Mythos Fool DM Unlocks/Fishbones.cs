using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Fishbones
    {
        public static void Add()
        {
            ConsumeItemEffect yummers = ScriptableObject.CreateInstance<ConsumeItemEffect>();
            
            GainLootRandomCharacterEffect reincarcinogen = ScriptableObject.CreateInstance<GainLootRandomCharacterEffect>();
            


            PerformEffect_Item fishbones = new("ST_Fishbones_ID")
            {
                Item_ID = "Fishbones_FishW",
                Name = "Fishbones",
                Flavour = "\"Into the trash, where you belong.\"",
                Description = "On killing an enemy there is a 15% chance you will gain a random party member..? Destroy this item on activation.",
                Icon = ResourceLoader.LoadSprite("item_fishbones"),
                TriggerOn = TriggerCalls.OnKill,
                DoesPopUpInfo = false,
                IsShopItem = false,
                ShopPrice = 0,
                StartsLocked = false,
                OnUnlockUsesTHE = false,
                Effects =
                [
                    Effects.GenerateEffect(reincarcinogen, 1, null, Effects.ChanceCondition(15)),
                    Effects.GenerateEffect(yummers, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],

            };



            //Unlock this
            string achievementID = "SorasToybox_Abraham_Antagonist_ACH";
            string unlockID = "SorasToybox_Abraham_Antagonist_Unlock";

            ItemUtils.AddItemToCustomStatsCategoryAndGamePool(fishbones.item, "Fish", "Fish", new ItemModdedUnlockInfo(fishbones.Item_ID, ResourceLoader.LoadSprite("item_fishbones_locked", null, 32, null), achievementID));
            ItemUtils.AddItemFishingRodPool(fishbones.item, 3, fishbones.item.startsLocked);
            ItemUtils.AddItemCanOfWormsPool(fishbones.item, 3, fishbones.item.startsLocked);

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, fishbones.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [fishbones.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Abraham_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(fishbones.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Abraham", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Abraham_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added Fishbones.");
            }
        }
    }
}
