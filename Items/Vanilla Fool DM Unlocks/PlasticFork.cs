using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace SorasToybox.Items
{
    public class PlasticFork
    {
        public static void Add()
        {
            RankChange_Wearable_SMS levelUpWearable = ScriptableObject.CreateInstance<RankChange_Wearable_SMS>();
            levelUpWearable._rankAdditive = 1;

            ConsumeItemEffect yummers = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            //flavor rando attempt
            String flavorText = "";
            if (UnityEngine.Random.Range((float)0.0, (float)1.0) > 0.9)
            {
                flavorText = "\"What if Prosthetics was, like, actually good?\"";
            }
            else
            {
                flavorText = "\"To be used and disposed of.\"";
            }


            PerformEffect_Item plasticFork = new PerformEffect_Item("ST_PlasticFork_ID", null, false)
            {
                Name = "Plastic Fork",
                Item_ID = "PlasticFork_FishW",
                Flavour = flavorText,
                Icon = ResourceLoader.LoadSprite("item_plasticfork", null, 32, null),
                Description = "This party member is one level higher than usual. At the end of combat, destroy the item this party member is holding.",
                TriggerOn = TriggerCalls.OnCombatEnd,
                DoesPopUpInfo = false,
                EquippedModifiers = [levelUpWearable],
                ShopPrice = 1,
                IsShopItem = false,
                OnUnlockUsesTHE = true,
                Effects =
                [
                    Effects.GenerateEffect(yummers, 1, Targeting.Slot_SelfSlot),
                ],
                UsesSpecialUnlockText = true,
                SpecialUnlockID = UILocID.ItemFishLocationLabel,

            };
            //Unlock this
            string achievementID = "SorasToybox_ShellyK_Antagonist_ACH";
            string unlockID = "SorasToybox_ShellyK_Antagonist_Unlock";

            ItemUtils.AddItemToCustomStatsCategoryAndGamePool(plasticFork.item, "Fish", "Fish", new ItemModdedUnlockInfo(plasticFork.Item_ID, ResourceLoader.LoadSprite("item_plasticfork_locked", null, 32, null), achievementID));
            ItemUtils.AddItemFishingRodPool(plasticFork.item, 3, plasticFork.item.startsLocked);
            ItemUtils.AddItemCanOfWormsPool(plasticFork.item, 3, plasticFork.item.startsLocked);

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, plasticFork.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [plasticFork.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("ShellyK", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Plastic Fork", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_ShellyK", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("ShellyK_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Plastic Fork.");
            }
        }
    }
}
