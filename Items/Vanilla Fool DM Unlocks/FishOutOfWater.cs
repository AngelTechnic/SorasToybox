using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class FishOutOfWater
    {
        public static void Add()
        {
            ConsumeItemEffect yummy = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            CopyAndSpawnCustomCharacterAnywhereEffect summonVenza = ScriptableObject.CreateInstance<CopyAndSpawnCustomCharacterAnywhereEffect>();
            summonVenza._characterCopy = "Venza_CH";
            summonVenza._permanentSpawn = true;
            summonVenza._rank = 0;
            summonVenza._extraModifiers = [];

            PerformEffect_Item fishOutOfWater = new PerformEffect_Item("ST_FishOutOfWater_ID", null, false)
            {
                Item_ID = "FishOutOfWater_FishW",
                Name = "Fish Out Of Water",
                Flavour = "\"You caught... this unfortunate fool.\"",
                Description = "At the start of combat, summon an expendable party member and destroy this item.",
                TriggerOn = TriggerCalls.OnCombatStart,
                IsShopItem = false,
                ShopPrice = 2011,
                OnUnlockUsesTHE = true,
                ConsumeOnTrigger = TriggerCalls.Count,
                ConsumeOnUse = true,
                Icon = ResourceLoader.LoadSprite("item_fishoutofwater", null, 32, null),
                DoesPopUpInfo = false,
                Effects =
                [
                    Effects.GenerateEffect(summonVenza, 1, Targeting.Slot_SelfSlot),
                ],
                UsesSpecialUnlockText = true,
                SpecialUnlockID = UILocID.ItemFishLocationLabel,
            };

            //Unlock this
            string achievementID = "SorasToybox_Mung_Antagonist_ACH";
            string unlockID = "SorasToybox_Mung_Antagonist_Unlock";

            ItemUtils.AddItemToCustomStatsCategoryAndGamePool(fishOutOfWater.item, "Fish", "Fish", new ItemModdedUnlockInfo(fishOutOfWater.Item_ID, ResourceLoader.LoadSprite("item_fishoutofwater_locked", null, 32, null), achievementID));
            ItemUtils.AddItemFishingRodPool(fishOutOfWater.item, 2, fishOutOfWater.item.startsLocked);
            ItemUtils.AddItemCanOfWormsPool(fishOutOfWater.item, 2, fishOutOfWater.item.startsLocked);

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, fishOutOfWater.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [fishOutOfWater.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mung", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Fish Out Of Water", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Mung", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Mung_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Fish Out Of Water.");
            }
        }
    }
}
