using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{ 
    public class PocketPlasm
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect worldsSmallestShoggoth = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            worldsSmallestShoggoth._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            PerformEffect_Item pocketPlasm = new("ST_WorldsSmallestShoggoth_ID")
            {
                Item_ID = "PocketPlasm_TW",
                Name = "Pocket Plasm",
                Flavour = "\"Solve all your problems the way a bulldozer does.\"",
                Description = "Inflict 3 Whiplash to the Opposing enemy whenever you move.",
                TriggerOn = TriggerCalls.OnMoved,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_pocketplasm"),
                OnUnlockUsesTHE = false,
                Effects =
                [
                    Effects.GenerateEffect(worldsSmallestShoggoth, 3, Targeting.Slot_Front),
                ],
                IsEffectImmediate = true,
                ShopPrice = 0,
                IsShopItem = false,
            };

            //Unlock this
            string achievementID = "SorasToybox_Sggha_Antagonist_ACH";
            string unlockID = "SorasToybox_Sggha_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(pocketPlasm.item, new ItemModdedUnlockInfo(pocketPlasm.Item_ID, ResourceLoader.LoadSprite("item_pocketplasm_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, pocketPlasm.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [pocketPlasm.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Sggha_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(pocketPlasm.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Sggha", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Sggha_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added Pocket Plasm.");
            }
        }
    }
}
