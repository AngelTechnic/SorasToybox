using BrutalAPI.Items;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class NarakaEmblem
    {
        public static void Add()
        {
            ConsumeAllCasterHealthManaEffect spoggleLarp = ScriptableObject.CreateInstance<ConsumeAllCasterHealthManaEffect>();

            StatusEffect_Apply_Effect ecstasyByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ecstasyByPrevious._Status = StatusField.GetCustomStatusEffect("Ecstasy_ID");
            ecstasyByPrevious._MultPreviousExitValueForEntry = true;

            Ability bloodThirst = new Ability("ST_BloodThirstEmblem_A")
            {
                Name = "Blood Thirst",
                AbilitySprite = ResourceLoader.LoadSprite("narakaemblem_bloodthirst"),
                Description = "Consume all pigment of this party member's health color and gain equal amount of Ecstasy.",
                Visuals = Visuals.Devour,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(spoggleLarp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ecstasyByPrevious, 1, Targeting.Slot_SelfSlot),
                ],

            };
            bloodThirst.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume), "Status_Ecstasy"]);

            ExtraAbility_Wearable_SMS narakaEmblemAbilityWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            narakaEmblemAbilityWearable._extraAbility = bloodThirst.GenerateCharacterAbility();

            PerformEffect_Item narakaEmblem = new("ST_NarakaEmblem_ID")
            {
                Icon = ResourceLoader.LoadSprite("item_narakaemblem"),
                Name = "Naraka Emblem",
                Item_ID = "NarakaEmblem_TW",
                Flavour = "\"A disgrace to demons everywhere.\"",
                Description = "Gain \"Blood Thirst\" as an extra ability, powering up from your pain.",
                TriggerOn = TriggerCalls.Count,
                DoesPopUpInfo = false,
                EquippedModifiers = [narakaEmblemAbilityWearable],
                ShopPrice = 6,
                IsShopItem = false,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
            };

            //Construct shenanigans i think
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, narakaEmblemAbilityWearable];

            //Unlock this
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(narakaEmblem.item, new ItemModdedUnlockInfo(narakaEmblem.Item_ID, ResourceLoader.LoadSprite("item_narakaemblem_locked", null, 32, null), "ErebusBoss_ACH"));
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Naraka Emblem.");
            }
        }
    }
}
