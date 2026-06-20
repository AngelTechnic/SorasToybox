using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SorasToybox.Items
{
    public class MemoryOfGriyadin
    {
        public static void Add()
        {
            StatusEffect_ApplyRestrictor_Effect getPermaMalfunction = ScriptableObject.CreateInstance<StatusEffect_ApplyRestrictor_Effect>();
            getPermaMalfunction._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");

            PerformEffect_Item evilSpaceStation = new PerformEffect_Item("ST_EvilSpaceStation_ID", null, false)
            {
                Name = "Memory of Griyadin",
                Item_ID = "MemoryOfGriyadin_TW",
                Flavour = "\"I still have nightmares about this place.\"",
                Description = "On combat start, inflict the Opposing enemy with permanent Malfunction.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = true,

                Icon = ResourceLoader.LoadSprite("item_memoryofgriyadin"),
                OnUnlockUsesTHE = true,
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(getPermaMalfunction, 1, Targeting.Slot_Front),
                ],
                DoesPopUpInfo = true,
            };
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(evilSpaceStation.item, new ItemModdedUnlockInfo(evilSpaceStation.Item_ID, ResourceLoader.LoadSprite("item_memoryofgriyadin_locked", null, 32, null), "SorasToybox_Mercurie_Antagonist_ACH"));

            //unlock this
            string achievementID = "SorasToybox_Mercurie_Antagonist_ACH";
            string unlockID = "SorasToybox_Mercurie_Antagonist_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, evilSpaceStation.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [evilSpaceStation.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Memory of Griyadin", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");
        }
    }
}
