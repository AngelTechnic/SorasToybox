using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;
using UnityEngine;
using JetBrains.Annotations;

namespace SorasToybox.CustomOther
{
    public class CopyThatEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            Debug.Log($"Copy That | activated with value {entryVariable}");
            // SELECT TARGETS
            List<TargetSlotInfo> targetsList = targets.ToList();
            while (targetsList.Count > entryVariable)
            {
                int randomIndex = UnityEngine.Random.Range(0, targetsList.Count);
                targetsList.RemoveAt(randomIndex);
            }
            targets = targetsList.ToArray();

            // ABILITY COPYING
            List<CombatAbility> abilitiesToProcess = new List<CombatAbility>();

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit) 
                { 
                    if (target.Unit is CharacterCombat character)
                    {
                        if (character.CombatAbilities.Count > 0)
                        {
                            List<CombatAbility> targetAbilitiesCopy = new List<CombatAbility>();
                            targetAbilitiesCopy.AddRange(character.CombatAbilities);
                            foreach (CombatAbility abilityCopy in targetAbilitiesCopy) 
                            { 
                                if (abilityCopy.ability._abilityName == "Slap")
                                {
                                    targetAbilitiesCopy.Remove(abilityCopy);
                                    break;
                                }
                            }
                            while (targetAbilitiesCopy.Count > 2)
                            {
                                int randomIndex = UnityEngine.Random.Range(0, targetAbilitiesCopy.Count);
                                targetAbilitiesCopy.RemoveAt(randomIndex);
                            }
                            foreach (CombatAbility abilityCopy in targetAbilitiesCopy)
                            {
                                abilitiesToProcess.Add(abilityCopy);
                            }
                        }
                    }
                }
            }

            List<CombatAbility> abilitiesToAdd = new List<CombatAbility>();
            if (abilitiesToProcess.Count > 0)
            {
                foreach (CombatAbility abilityAdd in abilitiesToProcess)
                {
                    if (abilityAdd.ability.priority == null)
                    {
                        abilityAdd.ability.priority = Priority.Normal;
                    }
                    CombatAbility newThis = new CombatAbility(abilityAdd.ability, Rarity.Common);
                    abilitiesToAdd.Add(newThis);
                }
            }

            List<CombatAbility> finalToAdd = new List<CombatAbility>();
            if (caster is EnemyCombat casterEN)
            {
                CombatAbility comeagain = casterEN.Abilities[0];
                finalToAdd.Add(comeagain);
                exitAmount++;
                foreach (CombatAbility ability in abilitiesToAdd)
                {
                    finalToAdd.Add(ability);
                    exitAmount++;
                }
                casterEN.Abilities = finalToAdd;
                CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(casterEN.ID));
            }

            //PASSIVE COPYING
            try
            {
                if (caster is EnemyCombat enemy) foreach (TargetSlotInfo target in targets) if (target.HasUnit && target.Unit is CharacterCombat character) PassiveCopying(character, character.PassiveAbilities, enemy);
            } catch
            {
                Debug.Log("simulacrum passive copy failure");
            }

            //HEALTH COLOR/COLOUR COPYING
            List<ManaColorSO> newHealthColour = new List<ManaColorSO>();

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    newHealthColour.Add(target.Unit.HealthColor);
                }
            }
            if (newHealthColour.Count > 1)
            {
                if (!newHealthColour.Distinct().Skip(1).Any()) //if the list is all the same health colour, just use that one
                { 
                    caster.ChangeHealthColor(newHealthColour[0]); 
                }
                else //yes, pigments like PurplePurpleBluePurpleBlue are still allowed, because I think it is funny
                {
                    caster.ChangeHealthColor(Pigments.SplitPigment(newHealthColour.ToArray()));
                }
            } else {
                caster.ChangeHealthColor(newHealthColour[0]);
            }
            Debug.Log("Copy That - Health Colour: " + caster.HealthColor.name);

            return exitAmount > 0;
        }

        // directly from Salt Enemies
        public class RefreshEnemyInfoUIAction : CombatAction
        {
            public int ID;
            public RefreshEnemyInfoUIAction(int id)
            {
                ID = id;
            }
            public override IEnumerator Execute(CombatStats stats)
            {
                EnemyCombat yeah = null;
                foreach (EnemyCombat enemy in stats.EnemiesOnField.Values) if (enemy.ID == ID) yeah = enemy;
                if (yeah != null)
                {
                    foreach (int enID in stats.combatUI._enemiesInCombat.Keys)
                    {
                        EnemyCombatUIInfo enemyInfo;
                        if (stats.combatUI._enemiesInCombat.TryGetValue(enID, out enemyInfo))
                        {
                            if (enemyInfo.SlotID == yeah.SlotID)
                            {
                                enemyInfo.Abilities = yeah.Abilities;
                                //enemyInfo.UpdateAttacks(enemyInfo.Abilities.ToArray());
                                stats.combatUI.TryUpdateAllEnemyAttacks(yeah.ID, yeah.Abilities.ToArray());
                                stats.combatUI.TryUpdateEnemyIDInformation(enID);
                            }
                        }
                    }
                }
                yield return null;
            }
        }

        public static void PassiveCopying(CharacterCombat character, List<BasePassiveAbilitySO> passives, EnemyCombat enemy)
        {
            List<BasePassiveAbilitySO> passivesSanitized = [];
            string[] passiveWhitelist =
            [
                //Base Game
                nameof(PassiveType_GameIDs.Focus),
                nameof(PassiveType_GameIDs.Slippery),
                //nameof(PassiveType_GameIDs.Parental),
                //nameof(PassiveType_GameIDs.Infantile),
                nameof(PassiveType_GameIDs.Unstable),
                nameof(PassiveType_GameIDs.Constricting),
                nameof(PassiveType_GameIDs.Formless),
                nameof(PassiveType_GameIDs.Pure),
                nameof(PassiveType_GameIDs.Absorb),
                nameof(PassiveType_GameIDs.Withering),
                nameof(PassiveType_GameIDs.Confusion),
                nameof(PassiveType_GameIDs.Dying),
                nameof(PassiveType_GameIDs.Inanimate),
                nameof(PassiveType_GameIDs.Inferno),
                nameof(PassiveType_GameIDs.Enfeebled),
                nameof(PassiveType_GameIDs.Immortal),
                nameof(PassiveType_GameIDs.TwoFaced),
                nameof(PassiveType_GameIDs.Catalyst),
                nameof(PassiveType_GameIDs.Delicate),
                nameof(PassiveType_GameIDs.Leaky),
                nameof(PassiveType_GameIDs.PanicAttack),
                nameof(PassiveType_GameIDs.Transfusion),
                nameof(PassiveType_GameIDs.BoneSpurs),
                nameof(PassiveType_GameIDs.Infestation),
                nameof(PassiveType_GameIDs.Masochism),
                nameof(PassiveType_GameIDs.Cashout),
                //Enemy Pack
                "Aegis_PA",
                "FourFaced_PA",
                "Depression_PA",
                "Blazing_PA",
                "TheGarden_PA",
                "Classic_PA",
                //Colophon Conundrum
                "Pollute_PA",
                //Hell Island Fell
                "Impunity",
                "Sacrilege",
                "Humorous",
                "Connoisseur",
                "Sweeping",
                "Interpolated",
                "Grinding",
                "Billiard",
                "Mirage",
                "Conviction",
                //Ruinful Revelry
                "RR_Masochism",
                "Showstopper_ID",
                //Into the Abyss
                "Refinement_PA",
                "Scholar_PA",
                "Theft_PA",
                "Carnivorous_PA",
                "Mammal_PA",
                "Delicious_PA",
                "Collision_ID",
                "Marked_PA",
                "Contagious_PA",
                "IsBasil_PA",
                "Substitute_PA",
                "Lethargic_PA",
                "Stubborn_PA",
                "Chlorophyll_PA",
                "Illuminant_PA",
                "ITA_MissFaced_PA",
                "ThreeFacedRBX_PA",
                //Passive Shop
                "Masquerade",
                "Silly",
                "Interlocking_PA",
                "Avenger_ID",
                "Arsonist_ID",
                "Patriotic_PA",
                "Bulwark_PA",
                //Marmo (Marmo's Fools, Box of Beasts)
                "Marmo_Exchange_PA",
                "Marmo_Grating_PA",
                "Marmo_Everchanging_PA",
                "TwoFaced_Nuzzles_PA",
                "Marmo_Pyrophilia_PA",
                "Marmo_Bloodlust_PA",
                "Marmo_Impetus_PA",
                //Stew (Stew's Fool Mods, Stew's Specimens)
                "StSpCauterizing",
                "FeiCauterizing",
                "Skates",
                //Mythos Friends
                "Opportunist_PA",
                "Drenched_PA",
                "Rugous_PA",
                "Sadism_PA",
                "Fungus_PA",
                //Tairbaz's Fools
                "CleansingMucus_PA",
                "ThickSkin_PA",
                "FakeFocus_PA",
                //This Mod!
                "Shy_PA",
                "Confrontational_PA",
                //Salt Enemies
                "FreakOut_PA",
                "Jumpy_PA",
                "Numb_PA",
                "Lightweight_PA",
                "Desperate_PA",
                "Splatter_PA",
                "Enruptured_PA",
                "Incomprehensible_PA",
                "Burning_PA",
                "Waves_PA",
                "Violent_PA",
                "Whimsy_PA",
                "Pillar_PA",
                "SweethTooth_PA",
                "Rewrite_PA",
                "Warping_PA",
                "Nylon_PA",
                "Scary_PA",
                "Announcement_PA",
                "MissFaced_PA",
                "Marching_PA",
                "Rotary_PA",
                "Backlash_PA",
                "Punisher_PA",
                "Jittery_PA",
                "Weakness_PA",
                "Heterochromia_PA",
                "CCTV_PA",
                "Compulsory_PA",
                "Skinning_PA",
                "Horrifying_PA",
                "Closure_PA",
                "Unmasking_PA",
                "Evasive_PA",
                "Tank_Warning_PA",
                "Survival_Instinct_PA",
                "Coda_PA",
                "NoPause_PA",
                "Salt_Asphyxiation_PA",
                "Revenge_PA",
                "PickPocket_PA",
                "Fishing_PA",
            ];
            foreach (BasePassiveAbilitySO passive in passives)
            {
                if (passiveWhitelist.Contains(passive.m_PassiveID))
                {
                    if (passive.m_PassiveID == "Chlorophyll_PA")
                    {
                        BasePassiveAbilitySO chlorophyllCopy = passive;
                        chlorophyllCopy._enemyDescription = "This enemy is permanently inflicted with Photosynthesis.";
                        passivesSanitized.Add(chlorophyllCopy);
                    }
                    else
                    {
                        passivesSanitized.Add(passive);
                    }
                }
            }
            if (passivesSanitized.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, passivesSanitized.Count);
                enemy.AddPassiveAbility(passivesSanitized[randomIndex]);
            }

            return;
        }
    }
}
