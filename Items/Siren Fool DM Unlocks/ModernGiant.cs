using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class ModernGiant
    {
        public static void Add()
        {
            TimelineLengthDamageEffect scienceMarchesOn = ScriptableObject.CreateInstance<TimelineLengthDamageEffect>();

            AnimationVisualsEffect ballisticGearVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            if (LoadedAssetsHandler.GetEnemy("WanderFellow_EN") != null)
            {
                ballisticGearVis._visuals = LoadedAssetsHandler.GetEnemyAbility("WFNowhere_A").visuals;
            }
            else
            {
                ballisticGearVis._visuals = Visuals.Parry;
            }

            Ability modernGiantAbility = new("ST_MarchOfProgress_A")
            {
                Name = "March Of Progress",
                Description = "Deal damage to the highest health enemy or enemies, equal to twice the length of the timeline.",
                AbilitySprite = ResourceLoader.LoadSprite("moderngiant_marchofprogress"),
                Cost = [Pigments.Yellow, Pigments.Yellow, Pigments.Red],
                Visuals = ballisticGearVis._visuals,
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Strongest,
                Effects =
                [
                    Effects.GenerateEffect(scienceMarchesOn, 2, Targeting.Spec_Unit_AllOpponents_Strongest),
                ],
            };
            modernGiantAbility.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Strongest, ["Damage_Timeline"]);

            ModernGiantCondition modernGiantCondition = ScriptableObject.CreateInstance<ModernGiantCondition>();

            //removing slap?
            AbilitiesUsageChange_Wearable_SMS modernGiantNoSlapWearable = ScriptableObject.CreateInstance<AbilitiesUsageChange_Wearable_SMS>();
            modernGiantNoSlapWearable._usesBasicAbility = false;

            //adding March of Progress?
            ExtraAbility_Wearable_SMS modernGiantAbilityWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            modernGiantAbilityWearable._extraAbility = modernGiantAbility.GenerateCharacterAbility();

            PerformEffect_Item modernGiant = new("ST_ModernGiant_ID")
            {
                Icon = ResourceLoader.LoadSprite("item_moderngiant"),
                Name = "Modern Giant",
                Item_ID = "ModernGiant_SW",
                Flavour = "\"Where's Don Quixote when you need- DARGH!\"",
                Description = "Apply 2 Celerity to the targets of damage effects.\nReplaces this character's basic ability with \"March Of Progress\", punishing enemies who get too big for their britches.",
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [modernGiantCondition],
                DoesPopUpInfo = false,
                EquippedModifiers = [modernGiantAbilityWearable, modernGiantNoSlapWearable],
                ShopPrice = 6,
                IsShopItem = true,
                StartsLocked = true,
                OnUnlockUsesTHE = false,
            };

            //Construct shenanigans i think
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, modernGiantAbilityWearable];

            //Unlock this
            string achievementID = "SorasToybox_Barron_Antagonist_ACH";
            string unlockID = "SorasToybox_Barron_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(modernGiant.item, new ItemModdedUnlockInfo(modernGiant.Item_ID, ResourceLoader.LoadSprite("item_moderngiant_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, modernGiant.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [modernGiant.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Barron_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(modernGiant.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Barron", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Barron_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Modern Giant.");
            }
        }
    }
}
