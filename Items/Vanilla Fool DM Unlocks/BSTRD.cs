using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class BSTRD
    {
        public static void Add()
        {
            AddPassiveEffect everyoneBecomesOvertunedOhFuckFuckFuck = ScriptableObject.CreateInstance<AddPassiveEffect>();
            everyoneBecomesOvertunedOhFuckFuckFuck._passiveToAdd = Passives.GetCustomPassive("ST_Overtuned_PA");

            RankChange_Wearable_SMS maxLevel = ScriptableObject.CreateInstance<RankChange_Wearable_SMS>();
            maxLevel._rankAdditive = 3;

            PerformEffect_Item bstrd = new PerformEffect_Item("ST_BSTRD_ID")
            {
                Item_ID = "BSTRD_TW",
                Name = "OUTSIDE INTERFERENCE",
                Flavour = "\"SICK >:}\"",
                Description = "Maximizes this party member's level.\nEnables Evil Mode...?",
                StartsLocked = true,
                ShopPrice = 80,
                Icon = ResourceLoader.LoadSprite("item_BSTRD"),
                IsShopItem = false,
                OnUnlockUsesTHE = false,
                Effects =
                [
                    Effects.GenerateEffect(everyoneBecomesOvertunedOhFuckFuckFuck, 1, Targeting.Unit_AllOpponents),

                ],
                TriggerOn = TriggerCalls.OnTurnStart_Early,
                EquippedModifiers = [maxLevel],
            };

            //unlock this
            string achievementID = "SorasToybox_Mordrake_Antagonist_ACH";
            string unlockID = "SorasToybox_Mordrake_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(bstrd.item, new ItemModdedUnlockInfo(bstrd.Item_ID, ResourceLoader.LoadSprite("item_BSTRD_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, bstrd.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [bstrd.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mordrake", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(bstrd.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Mordrake", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Mordrake_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added OUTSIDE INTERFERENCE.");
            }
        }
    }
}
