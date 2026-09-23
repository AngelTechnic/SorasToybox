using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class DivisionBell
    {
        public static void Add()
        {
            FieldEffect_Apply_Effect doResonanceRandomly = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            doResonanceRandomly._Field = StatusField.GetCustomFieldEffect("Resonance_ID");
            doResonanceRandomly._UseRandomBetweenPrevious = true;


            ExtraVariableForNextEffect blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            ExtraPassiveAbility_Wearable_SMS euphonyWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            euphonyWearable._extraPassiveAbility = Passives.GetCustomPassive("Euphony2_PA");

            SpawnEnemyAnywhereEffect shameFollows = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            shameFollows.enemy = LoadedAssetsHandler.GetEnemy("BurningShame_EN");
            shameFollows._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            shameFollows.givesExperience = false;

            PerformEffect_Item divisionBell = new PerformEffect_Item("ST_DivisionBell_ID")
            {
                Item_ID = "DivisionBell_SW",
                Name = "Division Bell",
                Flavour = "\"WHAT DO YOU WANT FROM ME?\"",
                Description = "Grants this party member Euphony (2) as a passive.\nWhen healing, there is a 75% chance to apply 0-3 Resonance to all party member positions.\nIf that fails, something emerges to criticize you.",
                TriggerOn = TriggerCalls.OnWillApplyHeal,
                Effects =
                [
                    Effects.GenerateEffect(blank, 0),
                    Effects.GenerateEffect(doResonanceRandomly, 3, Targeting.Unit_AllAllySlots, Effects.ChanceCondition(75)),
                    Effects.GenerateEffect(shameFollows, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
                EquippedModifiers = [euphonyWearable],
                StartsLocked = true,
                ShopPrice = 6,
                Icon = ResourceLoader.LoadSprite("item_divisionbell"),
                IsShopItem = true,
                OnUnlockUsesTHE = true,
            };

            //unlock this
            string achievementID = "SorasToybox_Agon_Antagonist_ACH";
            string unlockID = "SorasToybox_Agon_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(divisionBell.item, new ItemModdedUnlockInfo(divisionBell.Item_ID, ResourceLoader.LoadSprite("item_divisionbell_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, divisionBell.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [divisionBell.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Agon", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(divisionBell.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Agon", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Agon_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Division Bell.");
            }
        }
    }
}
