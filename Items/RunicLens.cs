using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace SorasToybox.Items
{
    public class RunicLens
    {
        public static void Add()
        {
            CasterStoreValueSetterAdvancedEffect SetTargeter = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            SetTargeter.m_unitStoredDataID = "TargeterStoredValue";
            SetTargeter._ignoreIfContains = false;
            SetTargeter.usePreviousExitValue = true;

            OpponentByStoredValueTargeting TargeterTargeting = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargeting._storedValueID = "TargeterStoredValue";
            TargeterTargeting.targetUnitAllySlots = false;
            TargeterTargeting.reduceByOne = true;

            PassivePopUpOnTargetEffect TargeterPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            TargeterPopup._name = "Targeter";
            TargeterPopup._sprite = "IconTargeter";
            TargeterPopup._isUnitCharacter = true;
            //getting visual for the lock on anim from SE
            AttackVisualsSO acquireAnim = null;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                acquireAnim = LoadedAssetsHandler.GetEnemyAbility("Mercenary_A").visuals;
            }
            else
            {
                acquireAnim = Visuals.Poke;
            }

            AnimationVisualsEffect TargetVisual = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TargetVisual._visuals = acquireAnim;
            TargetVisual._animationTarget = TargeterTargeting;



            PerformEffectPassiveAbility thresherEyeTargeterPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            thresherEyeTargeterPassive.name = "ST_RunicLensTargeter_PA";
            thresherEyeTargeterPassive._passiveName = "Targeter";
            thresherEyeTargeterPassive.m_PassiveID = "Targeter";
            thresherEyeTargeterPassive.passiveIcon = ResourceLoader.LoadSprite("IconTargeter");
            thresherEyeTargeterPassive._characterDescription = "At the start of combat and the end of each turn, this party member will remember the position of the enemy with the highest current health.";
            thresherEyeTargeterPassive._enemyDescription = "hey, no, this one is for party members. no. bad. nuh uh. no.";
            thresherEyeTargeterPassive._triggerOn = [TriggerCalls.OnCombatStart, TriggerCalls.OnTurnFinished];
            thresherEyeTargeterPassive.doesPassiveTriggerInformationPanel = true;
            thresherEyeTargeterPassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetGetSlotEffect>(), 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(TargetVisual),
            ];
            Passives.AddCustomPassiveToPool("ST_RunicLensTargeter_PA", "Targeter", thresherEyeTargeterPassive);


            AttackVisualsSO runicLensAnim = null;
            if (SorasToybox.CrossMod.AApocrypha)
            {
                runicLensAnim = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_AmdusiasMassAttack_A").visuals;
            }
            else
            {
                runicLensAnim = Visuals.Extrusion;
            }
            AnimationVisualsEffect runicLensVisualEffect = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            runicLensVisualEffect._visuals = runicLensAnim;
            runicLensVisualEffect._animationTarget = TargeterTargeting;

            DamageEffect runicLensDamage = ScriptableObject.CreateInstance<DamageEffect>();

            TargetPerformEffectViaSubaction runicLensSubact = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            runicLensSubact.effects =
                [
                    Effects.GenerateEffect(runicLensVisualEffect, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(runicLensDamage, 20, TargeterTargeting),
                ];


            ExtraPassiveAbility_Wearable_SMS runicLensTargeterWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            runicLensTargeterWearable._extraPassiveAbility = thresherEyeTargeterPassive;

            PerformEffect_Item runicLens = new("ST_RunicLens_ID")
            {
                Item_ID = "RunicLens_TW",
                IsShopItem = false,
                OnUnlockUsesTHE = true,
                EquippedModifiers = [runicLensTargeterWearable],
                Name = "Runic Lens",
                Flavour = "\"I’ve included the bill for my roof.\"",
                Description = "Gain Targeter as a passive.\nWhen performing an ability, there is a 20% chance to deal 20 damage to the Targeted position.",
                TriggerOn = TriggerCalls.OnAbilityUsed,
                Icon = ResourceLoader.LoadSprite("item_runiclens"),
                StartsLocked = true,
                ShopPrice = 15,
                DoesPopUpInfo = false,
                Effects =
                [
                    Effects.GenerateEffect(runicLensSubact, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(20)),
                ],
            };
            runicLens.item._ItemTypeIDs =
                [
                    ItemType_GameIDs.Magic.ToString(),
                ];

            //Unlock this
            string achievementID = "SorasToybox_Senek_Boundary_ACH";
            string unlockID = "SorasToybox_Senek_Boundary_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(runicLens.item, new ItemModdedUnlockInfo(runicLens.Item_ID, ResourceLoader.LoadSprite("item_runiclens_locked", null, 32, null), achievementID));


            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, runicLens.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [runicLens.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 32, null));
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(runicLens.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Katalixi_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Runic Lens.");
            }
        }
    }
}
