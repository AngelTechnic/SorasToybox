using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class FilmProjector
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS getGodray = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getGodray._extraPassiveAbility = Passives.GetCustomPassive("ST_Godray_PA");

            ExtraPassiveAbility_Wearable_SMS getDeathbound = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getDeathbound._extraPassiveAbility = Passives.GetCustomPassive("ST_Deathbound_PA");

            StatusEffect_Apply_Effect getLinked = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getLinked._Status = StatusField.Linked;


            PerformEffect_Item filmProjector = new PerformEffect_Item("ST_FilmProjector_ID", null, false)
            {
                Item_ID = "FilmProjector_TW",
                Name = "Film Projector",
                Flavour = "\"I AM doing this to hurt you. I thought you'd never ask.\"",
                Description = "Gain Deathbound and Godray as passives.\nOn performing an ability, inflict 2 Linked to the Opposing enemy.",
                Icon = ResourceLoader.LoadSprite("item_filmprojector"),
                TriggerOn = TriggerCalls.OnAbilityUsed,
                Effects = [
                    Effects.GenerateEffect(getLinked, 2, Targeting.Slot_Front),
                    ],
                EquippedModifiers = [getGodray, getDeathbound],
                IsShopItem = false,
                ShopPrice = 888,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
            };

            //unlock this
            string achievementID = "SorasToybox_Rags_Antagonist_ACH";
            string unlockID = "SorasToybox_Rags_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(filmProjector.item, new ItemModdedUnlockInfo(filmProjector.Item_ID, ResourceLoader.LoadSprite("item_filmprojector_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, filmProjector.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [filmProjector.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Rags", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Film Projector", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Rags", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Film Projector.");
            }
        }
    }
}
