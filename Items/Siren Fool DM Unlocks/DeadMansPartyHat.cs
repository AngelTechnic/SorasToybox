using BrutalAPI.Items;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class DeadMansPartyHat
    {
        public static void Add()
        {
            SpecificUnitsByPassiveTargeting allEnemiesWithAbsorb = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            allEnemiesWithAbsorb._passive = Passives.Absorb;
            allEnemiesWithAbsorb.targetUnitAllySlots = true;
            allEnemiesWithAbsorb.slotOffsets = [0];

            AddPassiveEffect becomeInanimate = ScriptableObject.CreateInstance<AddPassiveEffect>();
            becomeInanimate._passiveToAdd = Passives.Inanimate;

            ExtraPassiveAbility_Wearable_SMS vandalWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            vandalWearable._extraPassiveAbility = Passives.GetCustomPassive("Vandal_PA");

            PerformEffect_Item deadMansPartyHat = new PerformEffect_Item("ST_DeadMansPartyHat_ID")
            {
                Item_ID = "DeadMansPartyHat_SW",
                IsShopItem = true,
                ShopPrice = 6,
                Name = "Dead Man's Party Hat",
                Flavour = "\"YAAAAY!\"",
                Description = "This party member is now a Vandal.\nOn combat start, render Inanimate all enemies with Absorb.",
                TriggerOn = TriggerCalls.OnCombatStart,
                EquippedModifiers = [vandalWearable],
                DoesPopUpInfo = true,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
                Effects =
                [
                    Effects.GenerateEffect(becomeInanimate, 1, allEnemiesWithAbsorb),
                ],
                Icon = ResourceLoader.LoadSprite("item_deadmanspartyhat", null, 32, null),
            };

            //Unlock this
            string achievementID = "SorasToybox_Izzy_Antagonist_ACH";
            string unlockID = "SorasToybox_Izzy_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(deadMansPartyHat.item, new ItemModdedUnlockInfo(deadMansPartyHat.Item_ID, ResourceLoader.LoadSprite("item_deadmanspartyhat_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, deadMansPartyHat.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [deadMansPartyHat.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Izzy_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(deadMansPartyHat.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Izzy", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Izzy_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Dead Man's Party Hat.");
            }
        }
    }
}
