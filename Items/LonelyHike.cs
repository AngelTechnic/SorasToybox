using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;
using UnityEngine.UIElements;

namespace SorasToybox.Items
{
    public class LonelyHike
    {
        public static void Add()
        {
            AddPassiveEffect getOvertuned = ScriptableObject.CreateInstance<AddPassiveEffect>();
            getOvertuned._passiveToAdd = Passives.GetCustomPassive("ST_Overtuned_PA");

            CheckHasUnitEffect nobodyHere = ScriptableObject.CreateInstance<CheckHasUnitEffect>();

            ConsumeItemEffect byebyesquiddy = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            PerformEffect_Item lonelyHike = new PerformEffect_Item("ST_LonelyHike_ID", null, false)
            {
                Item_ID = "LonelyHike_FishW",
                Name = "Lonely Hike",
                Flavour = "\"...energy...\"",
                Description = "On turn start, if this party member is the only one left, gain Overtuned as a passive and destroy this item.",
                TriggerOn = TriggerCalls.OnTurnStart,
                IsShopItem = false,
                ShopPrice = 2007,
                OnUnlockUsesTHE = true,
                Icon = ResourceLoader.LoadSprite("item_lonelyhike", null, 32, null),
                DoesPopUpInfo = false,
                Effects =
                [
                    Effects.GenerateEffect(nobodyHere, 1, Targeting.Unit_OtherAlliesSlots),
                    Effects.GenerateEffect(getOvertuned, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(byebyesquiddy, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                UsesSpecialUnlockText = true,
                SpecialUnlockID = UILocID.ItemFishLocationLabel,
            };

            //Unlock this
            string achievementID = "SorasToybox_Mercurie_Boundary_ACH";
            string unlockID = "SorasToybox_Mercurie_Boundary_Unlock";

            ItemUtils.AddItemToCustomStatsCategoryAndGamePool(lonelyHike.item, "Fish", "Fish",  new ItemModdedUnlockInfo(lonelyHike.Item_ID, ResourceLoader.LoadSprite("item_lonelyhike_locked", null, 32, null), achievementID));
            ItemUtils.AddItemFishingRodPool(lonelyHike.item, 2, lonelyHike.item.startsLocked);
            ItemUtils.AddItemCanOfWormsPool(lonelyHike.item, 2, lonelyHike.item.startsLocked);

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, lonelyHike.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [lonelyHike.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Lonely Hike", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Katalixi_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");
        }
    }
}
