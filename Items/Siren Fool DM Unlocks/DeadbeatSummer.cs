using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class DeadbeatSummer
    {
        public static void Add()
        {
            CheckPassiveAbilityEffect amIParental = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            amIParental.m_PassiveID = Passives.Example_Parental_Vengeance.m_PassiveID;

            CheckPassiveAbilityEffect amIInfantile = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
            amIInfantile.m_PassiveID = Passives.Infantile.m_PassiveID;

            //basic number
            ExtraVariableForNextEffect blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            StatusEffect_Apply_Effect randomMisery = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            randomMisery._Status = StatusField.GetCustomStatusEffect("Misery_ID");
            randomMisery._RandomBetweenPrevious = true;

            StatusEffect_Apply_Effect randomEcstasy = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            randomEcstasy._Status = StatusField.GetCustomStatusEffect("Ecstasy_ID");
            randomEcstasy._RandomBetweenPrevious = true;

            TargetPerformEffectViaSubaction deadbeatSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            deadbeatSubAct.effects = 
            [
                Effects.GenerateEffect(amIInfantile, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(blank, 1, null, Effects.CheckPreviousEffectCondition(true, 1)),
                Effects.GenerateEffect(randomMisery, 3, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                Effects.GenerateEffect(amIParental, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(blank, 0, null, Effects.CheckPreviousEffectCondition(true, 1)),
                Effects.GenerateEffect(randomEcstasy, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
            ];

            PerformEffect_Item deadbeatSummer = new PerformEffect_Item("ST_DeadbeatSummer_ID")
            {
                Item_ID = "DeadbeatSummer_TW",
                IsShopItem = false,
                ShopPrice = 6,
                Name = "Deadbeat Summer",
                Flavour = "\"Youth is WASTED on the young!\"",
                Description = "At the start of each turn, inflict 1-3 Misery on all Infantile enemies, and 0-2 Ecstasy on all Parental enemies.",
                TriggerOn = TriggerCalls.OnTurnStart,
                DoesPopUpInfo = true,
                StartsLocked = true,
                OnUnlockUsesTHE = false,
                Effects =
                [
                    Effects.GenerateEffect(deadbeatSubAct, 1, Targeting.Unit_AllOpponents),
                ],
                Icon = ResourceLoader.LoadSprite("item_deadbeatsummer", null, 32, null),
            };

            //Unlock this
            string achievementID = "SorasToybox_Francis_Antagonist_ACH";
            string unlockID = "SorasToybox_Francis_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(deadbeatSummer.item, new ItemModdedUnlockInfo(deadbeatSummer.Item_ID, ResourceLoader.LoadSprite("item_deadbeatsummer_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, deadbeatSummer.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [deadbeatSummer.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Francis_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(deadbeatSummer.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Francis", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Francis_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Deadbeat Summer.");
            }
        }
    }
}
