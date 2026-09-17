using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Eternity
    {
        public static void Add()
        {
            StatusEffectCheckerEffect hasDP = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasDP._status = StatusField.DivineProtection;
            
            StatusEffect_Apply_Effect getMisery = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getMisery._Status = StatusField.GetCustomStatusEffect("Misery_ID");

            ExtraPassiveAbility_Wearable_SMS anointedWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            anointedWearable._extraPassiveAbility = Passives.Anointed2;

            TargetPerformEffectViaSubaction eternitySubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            eternitySubAct.effects =
                [
                    Effects.GenerateEffect(hasDP, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getMisery, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ];

            PerformEffect_Item eternity = new PerformEffect_Item("ST_Eternity_ID")
            {
                Item_ID = "ST_Eternity_TW",
                Name = "Eternity",
                Flavour = "\"You don't want this.\"",
                Description = "This party member is Anointed. At the end of your turn, inflict 2 Misery to all enemies with Divine Protection.",
                EquippedModifiers = [anointedWearable],
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(eternitySubAct, 1, Targeting.Unit_AllOpponents),
                ],
                Icon = ResourceLoader.LoadSprite("item_eternity", null, 32, null),
                IsShopItem = false,
                ShopPrice = 0,
                StartsLocked = true,
                OnUnlockUsesTHE = false,

            };

            //Unlock this
            string achievementID = "SorasToybox_Bimini_Antagonist_ACH";
            string unlockID = "SorasToybox_Bimini_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(eternity.item, new ItemModdedUnlockInfo(eternity.Item_ID, ResourceLoader.LoadSprite("item_eternity_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, eternity.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [eternity.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Bimini", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(eternity.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Bimini", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Bimini_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Eternity.");
            }
        }
    }
}
