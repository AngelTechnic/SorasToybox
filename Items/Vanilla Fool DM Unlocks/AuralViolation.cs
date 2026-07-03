using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class AuralViolation
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getMisery = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getMisery._Status = StatusField.GetCustomStatusEffect("Misery_ID");

            PerformEffect_Item heScreamLoudAsFuck = new PerformEffect_Item("ST_AuralViolation_ID", null, false)
            {
                Item_ID = "AuralViolation_SW",
                Name = "Aural Violation",
                Flavour = "\"It's like caps lock for your vocal cords!\"",
                Description = "When this party member receives any damage, inflict 2 Misery on the Left, Opposing, and Right enemies.",
                Icon = ResourceLoader.LoadSprite("item_auralviolation"),
                TriggerOn = TriggerCalls.OnDamaged,
                OnUnlockUsesTHE = false,
                IsShopItem = true,
                ShopPrice = 4,
                StartsLocked = true,
                Effects =
                [
                    Effects.GenerateEffect(getMisery, 2, Targeting.Slot_FrontAndSides),
                ],
            };


            //unlock this
            string achievementID = "SorasToybox_Anton_Antagonist_ACH";
            string unlockID = "SorasToybox_Anton_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(heScreamLoudAsFuck.item, new ItemModdedUnlockInfo(heScreamLoudAsFuck.Item_ID, ResourceLoader.LoadSprite("item_auralviolation_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, heScreamLoudAsFuck.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [heScreamLoudAsFuck.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Anton", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Aural Violation", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Anton", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Aural Violation.");
            }
        }
    }
}
