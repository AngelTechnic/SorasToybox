using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;

namespace SorasToybox.Items.Vanilla_Fool_DM_Unlocks
{
    public class EntrenchingTool
    {
        public static void Add()
        {
            SwapToSidesEffect rocketJump = ScriptableObject.CreateInstance<SwapToSidesEffect>();



            DamagePercentModAndSecondaryEffect_Item bigSpoon = new DamagePercentModAndSecondaryEffect_Item("ST_EntrenchingTool_ID", 50, true, false, true)
            {
                Item_ID = "EntrenchingTool_SW",
                Name = "Entrenching Tool",
                Flavour = "\"Market Gardener\"",
                Description = "This party member now deals 50% more damage, but will move randomly Left or Right when using an ability.",
                IsShopItem = true,
                ShopPrice = 10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_entrenchingtool"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryTriggerOn = [TriggerCalls.OnAbilityWillBeUsed],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(rocketJump, 1, Targeting.Slot_SelfSlot),
                ],
            };

            //unlock this
            string achievementID = "SorasToybox_Boyle_Antagonist_ACH";
            string unlockID = "SorasToybox_Boyle_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(bigSpoon.item, new ItemModdedUnlockInfo(bigSpoon.Item_ID, ResourceLoader.LoadSprite("item_entrenchingtool_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, bigSpoon.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [bigSpoon.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Boyle_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Entrenching Tool", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Boyle", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");
        }
    }
}
