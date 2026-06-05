using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox.CustomEffects;
using UnityEngine;

namespace SorasToybox.CustomPassives
{
    public class BonusSuiteWithRerollConditionPassiveAbility : BasePassiveAbilitySO
    {
        [Header("ExtraAttack Data")]
        public List<ExtraAbilityInfo> _suiteAbilities;

        public TriggerCalls[] _secondTriggerOn;

        public EffectorConditionSO[] _secondPerformConditions;

        public bool _secondDoesPerformPopUp = true;
        public override bool IsPassiveImmediate => true;

        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            if (args is List<string> list)
            {
                list.Add(_suiteAbilities[ChooseAbility()].ability?.name);
            }
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            List<string> addedOnes = new List<string>();
            foreach (ExtraAbilityInfo ability in _suiteAbilities)
            {
                //Debug.Log("added ability " + ability.ability.name);
                if (!addedOnes.Contains(ability.ability._abilityName))
                {
                    unit.AddExtraAbility(ability);
                    addedOnes.Add(ability.ability._abilityName);
                }
            }
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            foreach (ExtraAbilityInfo ability in _suiteAbilities)
            {
                //Debug.Log(ability.ability.name);
                unit.TryRemoveExtraAbility(ability);
            }
        }

        public int ChooseAbility()
        {
            return UnityEngine.Random.Range(0, _suiteAbilities.Count);
        }

        public override void CustomOnTriggerAttached(IPassiveEffector caller)
        {
            TriggerCalls[] secondTriggerOn = _secondTriggerOn;
            for (int i = 0; i < secondTriggerOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondTriggerOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.AddObserver(CustomTryTriggerPassive, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void CustomOnTriggerDettached(IPassiveEffector caller)
        {
            TriggerCalls[] secondTriggerOn = _secondTriggerOn;
            for (int i = 0; i < secondTriggerOn.Length; i++)
            {
                TriggerCalls triggerCalls = secondTriggerOn[i];
                if (triggerCalls != TriggerCalls.Count)
                {
                    CombatManager.Instance.RemoveObserver(CustomTryTriggerPassive, triggerCalls.ToString(), caller);
                }
            }
        }

        public override void FinalizeCustomTriggerPassive(object sender, object args, int triggerID)
        {
            if (sender is IPassiveEffector passiveEffector && !passiveEffector.Equals(null))
            {
                if (_secondDoesPerformPopUp)
                {
                    CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(passiveEffector.ID, passiveEffector.IsUnitCharacter, GetPassiveLocData().text, passiveIcon));
                }

                IUnit caster = sender as IUnit;

                if (caster is EnemyCombat unit)
                {
                    CombatStats stats = CombatManager.Instance._stats;
                    int indexCounter = 0;
                    int resultID = -1;
                    int resultIndex = -1;
                    List<int> suiteSlots = new List<int>();
                    foreach (ExtraAbilityInfo suiteAbil in _suiteAbilities)
                    {
                        suiteSlots.Add(unit.GetLastAbilityIDFromNameUsingAbilityName(suiteAbil.ability._abilityName));
                    }

                    foreach (TurnInfo thingy in stats.timeline.Round)
                    {
                        //thingy.turnUnit.HasAbilityID(thingy.abilitySlot);
                        Debug.Log("testing " + thingy.turnUnit.ID + " by comparison to " + unit.ID);
                        if (thingy.turnUnit.ID != unit.ID)
                        {
                            indexCounter++;
                            continue;
                        }
                        foreach (int suiteSlot in suiteSlots)
                        {
                            if (thingy.abilitySlot == suiteSlot)
                            {
                                resultID = thingy.abilitySlot;
                                resultIndex = indexCounter;
                                Debug.Log("settled on resultID " + resultID + " and resultIndex " + resultIndex);
                                break;
                            }
                        }
                        if (resultID == -1 || resultIndex == -1)
                        {
                            indexCounter++;
                            continue;
                        }
                        else { break; }
                    }

                    if (resultID != -1 && resultIndex != -1)
                    {
                        List<int> newSlots = new List<int>();
                        foreach (int suiteSlot in suiteSlots)
                        {
                            if (suiteSlot != resultID) { newSlots.Add(suiteSlot); }
                        }

                        TurnInfo rerollAbility = stats.timeline.Round[resultIndex];
                        int newID = newSlots[UnityEngine.Random.Range(0, newSlots.Count)];
                        rerollAbility.abilitySlot = newID;
                        stats.timeline.Round[resultIndex] = rerollAbility;
                        CombatManager.Instance.AddUIAction(new UpdateReRolledSlotTimelineUIAction(unit.ID, [resultIndex], [resultID], [newID]));
                    }
                }
            }
        }

        public void CustomTryTriggerPassive(object sender, object args)
        {
            if (!(sender is IPassiveEffector passiveEffector) || passiveEffector.Equals(null) || !passiveEffector.CanPassiveTrigger(m_PassiveID))
            {
                return;
            }

            if (_secondPerformConditions != null && !_secondPerformConditions.Equals(null))
            {
                EffectorConditionSO[] secondPerformConditions = _secondPerformConditions;
                for (int i = 0; i < secondPerformConditions.Length; i++)
                {
                    if (!secondPerformConditions[i].MeetCondition(passiveEffector, args))
                    {
                        return;
                    }
                }
            }

            CombatManager.Instance.AddSubAction(new PerformPassiveCustomAction(this, sender, args, 0));
        }
    }
}
