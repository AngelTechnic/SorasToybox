using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items.Vanilla_Fool_DM_Unlocks
{
    public class AtomSmasher
    {
        public static void Add()
        {
            PerformEffect_Item atomSmasher = new PerformEffect_Item("ST_AtomSmasher_ID", null, false)
            {
                Item_ID = "AtomSmasher_TW",
                Name = "Atom Smasher",
                Flavour = "\"What do elephants have to do with radiation? ...oh.\"",
                Description = "On dealing damage to an enemy, inflict equal Collapse to them!",
                Icon = ResourceLoader.LoadSprite("item_atomsmasher"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<AtomSmasherCondition>()],
                IsShopItem = false,
                ShopPrice = 8,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
            };

            //unlock this
            string achievementID = "SorasToybox_Griffin_Antagonist_ACH";
            string unlockID = "SorasToybox_Griffin_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(atomSmasher.item, new ItemModdedUnlockInfo(atomSmasher.Item_ID, ResourceLoader.LoadSprite("item_atomSmasher_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, atomSmasher.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [atomSmasher.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Griffin", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Atom Smasher", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Griffin", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Atom Smasher.");
            }
        }
    }
}
