using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomOther;
using SorasToybox.CustomEffects;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
//using static A_Apocrypha.Encounters.Orph.H;
//using static UnityEngine.GraphicsBuffer;

namespace SorasToybox.Fools
{
    public class JournalHandler
    {
        public static void Add()
        {
            UnityEngine.Debug.Log("Journal Mode | activated! beginning setup...");

            //JOURNAL TARGET LISTS
            //MERCURIE
            Dictionary<string, string> targetListMercurie = new Dictionary<string, string>();
            targetListMercurie.Add("SEARCH_EN", "SEARCH");
            targetListMercurie.Add("Lexem_EN", "Lexem");
            targetListMercurie.Add("AmalgamatedAssessor_BOSS", "Assessor");
            targetListMercurie.Add("Minotaur_EN", "Minotaur");
            targetListMercurie.Add("Deathmatch_BOSS", "Deathmatch");
            targetListMercurie.Add("Boulder_Inert_EN", "BoulderFirst");
            targetListMercurie.Add("Boulder_ToBuddy_EN", "BoulderFirst");
            targetListMercurie.Add("Boulder_ToChiglen_EN", "BoulderFirst");
            targetListMercurie.Add("Boulder_EN", "BoulderReal");
            targetListMercurie.Add("BoulderBuddy_EN", "BoulderBuddy");
            targetListMercurie.Add("ElChiglen_EN", "ElChiglen");
            targetListMercurie.Add("SandSifter_EN", "SandSifter");
            targetListMercurie.Add("SandSifterSummon_EN", "SandSifterSummon");
            targetListMercurie.Add("SuspiciousMung_EN", "SusMung");
            targetListMercurie.Add("SkullHermit_Hidden_EN", "Hermit1");
            targetListMercurie.Add("CobaltCurator_EN", "CobaltCurator");
            targetListMercurie.Add("EphialtesOrguis_EN", "EphialtesOrguis");
            targetListMercurie.Add("ApatelosOrguis_EN", "ApatelosOrguis");
            targetListMercurie.Add("PanopticOrguis_EN", "PanopticOrguis");
            targetListMercurie.Add("HamalatOrguis_EN", "HamalatOrguis");
            targetListMercurie.Add("ArtilleryWitch_EN", "ArtilleryWitch");
            targetListMercurie.Add("Tillinghast_BOSS", "Tillinghast");
            targetListMercurie.Add("DiscShaped_Liquivore_BOSS", "TerrorFromBeyond");
            targetListMercurie.Add("Tumbleweed_Liquivore_BOSS", "TerrorFromBeyond");
            targetListMercurie.Add("Ophidian_Liquivore_BOSS", "TerrorFromBeyond");

            Dictionary<string, Dictionary<string, string>> targetAltListMercurie = new Dictionary<string, Dictionary<string, string>>();

            if (LoadedAssetsHandler.GetCharacter("Soreka_CH") != null)
            {
                Dictionary<string, string> targetListMercurieSoreka = new Dictionary<string, string>();
                targetListMercurieSoreka.Add("AmalgamatedAssessor_BOSS", "AssessorSoreka");
                targetAltListMercurie.Add("Soreka_CH", targetListMercurieSoreka);
            }

            if (LoadedAssetsHandler.GetCharacter("Naudiz4_CH") != null)
            {
                Dictionary<string, string> targetListMercurieNaudiz = new Dictionary<string, string>();
                targetListMercurieNaudiz.Add("SandSifter_EN", "SandSifterNaudiz4");
                targetListMercurieNaudiz.Add("CobaltCurator_EN", "CobaltCuratorNaudiz4");
                targetAltListMercurie.Add("Naudiz4_CH", targetListMercurieNaudiz);
            }

            string mercurieDiaID = "Mercurie_Journal_Dialogue";
            YarnProgram mercurieYarn = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxMisc/MercurieJournalScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(mercurieDiaID, mercurieYarn);
            DialogueSO mercurieDialogue = Dialogues.CreateAndAddCustom_DialogueSO(mercurieDiaID, mercurieYarn, mercurieDiaID, "SorasToybox.Mercurie.Journal");


            //MERCURIE JOURNAL
            if (LoadedAssetsHandler.GetCharacter("Mercurie_CH") != null)
            {
                UnityEngine.Debug.Log("Journal Mode | Mercurie journal setup");
                JournalSetup("Mercurie_CH", "Mercurie", "Mercurie", ["she", "her", "her"], "Ministry-Issue Datapad", "Space-age. Woah.", "", targetListMercurie, targetAltListMercurie, mercurieDialogue);
            }

            //KARMA
            Dictionary<string, string> targetListKarma = new Dictionary<string, string>();
            targetListKarma.Add("Deathmatch_BOSS", "Deathmatch");
            targetListKarma.Add("Minotaur_EN", "Minotaur");
            targetListKarma.Add("Litany_EN", "Faceless");
            targetListKarma.Add("Faceless_EN", "Faceless");
            targetListKarma.Add("VoxPopuli_EN", "Faceless");
            targetListKarma.Add("Dozer_EN", "Dozer");
            targetListKarma.Add("DozerAwake_EN", "DozerAwake");
            targetListKarma.Add("Mime_EN", "Mime");
            targetListKarma.Add("Kookoo_EN", "Kookoo");
            targetListKarma.Add("Home_EN", "Home");
            targetListKarma.Add("Primus_EN", "Primus");
            targetListKarma.Add("Nolocimes_Batretne_BOSS", "Katalixi");
            targetListKarma.Add("SkullHermit_Hidden_EN", "Hermit1");
            targetListKarma.Add("RiftMiniboss_EN", "RiftMiniboss");
            targetListKarma.Add("ArtilleryWitch_EN", "ArtilleryWitch");


            Dictionary<string, Dictionary<string, string>> targetAltListKarma = new Dictionary<string, Dictionary<string, string>>();

            if (LoadedAssetsHandler.GetCharacter("Vane_CH") != null)
            {
                Dictionary<string, string> targetListKarmaVane = new Dictionary<string, string>();
                targetListKarmaVane.Add("Home_EN", "HomeVane");
                targetAltListKarma.Add("Vane_CH", targetListKarmaVane);
            }

            string karmaDiaID = "Karma_Journal_Dialogue";
            YarnProgram karmaYarn = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxMisc/KarmaJournalScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(karmaDiaID, karmaYarn);
            DialogueSO karmaDialogue = Dialogues.CreateAndAddCustom_DialogueSO(karmaDiaID, karmaYarn, karmaDiaID, "SorasToybox.Karma.Journal");

            //KARMA JOURNAL
            if (LoadedAssetsHandler.GetCharacter("Karma_CH") != null)
            {
                UnityEngine.Debug.Log("Journal Mode | Karma journal setup");
                JournalSetup("Karma_CH", "Karma", "Karma", ["she", "her", "her"], "Karma's Journal", "A palpable sense of ire emanates from the hardbook cover.", "", targetListKarma, targetAltListKarma, karmaDialogue);
            }
        }

            //AMBROSE
            /*Dictionary<string, string> targetListAmbrose = new Dictionary<string, string>();
            targetListAmbrose.Add("CrimsonLogos_EN", "LogosCrimson");
            targetListAmbrose.Add("CeruleanLogos_EN", "LogosCerulean");
            targetListAmbrose.Add("AureateLogos_EN", "LogosAureate");
            targetListAmbrose.Add("RegentLogos_EN", "LogosRegent");
            targetListAmbrose.Add("DiscordantLogos_EN", "LogosDisco");
            targetListAmbrose.Add("EphialtesOrguis_EN", "OrguisGeneric");
            targetListAmbrose.Add("ApatelosOrguis_EN", "OrguisGeneric");
            targetListAmbrose.Add("PanopticOrguis_EN", "OrguisGeneric");
            targetListAmbrose.Add("HamalatOrguis_EN", "OrguisGeneric");
            targetListAmbrose.Add("Blemmigan_EN", "Blemmigan");
            targetListAmbrose.Add("UttershroomSpore_EN", "Uttershroom");
            targetListAmbrose.Add("Minotaur_EN", "Minotaur");



            Dictionary<string, Dictionary<string, string>> targetAltListAmbrose = new Dictionary<string, Dictionary<string, string>>();

            if (LoadedAssetsHandler.GetCharacter("Soreka_CH") != null)
            {
                Dictionary<string, string> targetListAmbroseSoreka = new Dictionary<string, string>();
                targetListAmbroseSoreka.Add("EphialtesOrguis_EN", "OrguisIridescent");
                targetListAmbroseSoreka.Add("ApatelosOrguis_EN", "OrguisClusterfuck");
                targetListAmbroseSoreka.Add("PanopticOrguis_EN", "OrguisEntropic");
                targetListAmbroseSoreka.Add("HamalatOrguis_EN", "OrguisWhite");
                targetAltListAmbrose.Add("Soreka_CH", targetListAmbroseSoreka);
            }

            string ambroseDiaID = "Ambrose_Journal_Dialogue";
            YarnProgram ambroseYarn = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Misc/AmbroseJournalScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(ambroseDiaID, ambroseYarn);
            DialogueSO ambroseDialogue = Dialogues.CreateAndAddCustom_DialogueSO(ambroseDiaID, ambroseYarn, ambroseDiaID, "AApocrypha.Ambrose.Journal");

            //NAUDIZ 4
            Dictionary<string, string> targetListNaudiz = new Dictionary<string, string>();
            targetListNaudiz.Add("SandSifter_EN", "SandSifter");
            targetListNaudiz.Add("DuneThresher_EN", "DuneThresher");
            targetListNaudiz.Add("HazardHauler_Siren_EN", "HazardHauler");
            targetListNaudiz.Add("AmalgamatedAssessor_BOSS", "Assessor");
            targetListNaudiz.Add("Minotaur_EN", "Minotaur");
            targetListNaudiz.Add("Gammamite_EN", "Gammamite");
            targetListNaudiz.Add("TuringTarpit_EN", "TuringTarpit");
            targetListNaudiz.Add("Eater_Invis_EN", "InvisEater");
            targetListNaudiz.Add("CobaltCurator_EN", "CobaltCurator");

            Dictionary<string, Dictionary<string, string>> targetAltListNaudiz4 = new Dictionary<string, Dictionary<string, string>>();

            string naudizDiaID = "Naudiz4_Journal_Dialogue";
            YarnProgram naudizYarn = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Misc/Naudiz4JournalScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(naudizDiaID, naudizYarn);
            DialogueSO naudizDialogue = Dialogues.CreateAndAddCustom_DialogueSO(naudizDiaID, naudizYarn, naudizDiaID, "AApocrypha.Naudiz4.Journal");

            //WHITLOCK
            Dictionary<string, string> targetListWhitlock = new Dictionary<string, string>();
            targetListWhitlock.Add("Ungod_EN", "Ungod");
            targetListWhitlock.Add("Gizo_EN", "Gizo");
            targetListWhitlock.Add("NakedGizo_EN", "Gizo2");
            targetListWhitlock.Add("Blemmigan_EN", "Blemmigan");
            targetListWhitlock.Add("UttershroomSpore_EN", "Uttershroom");
            targetListWhitlock.Add("Knight_EN", "Chess");
            targetListWhitlock.Add("PawnA_EN", "Chess");
            targetListWhitlock.Add("Yin_EN", "Chess");
            targetListWhitlock.Add("Yang_EN", "Chess");
            targetListWhitlock.Add("Bloatfinger_EN", "Bloatfinger");
            targetListWhitlock.Add("StillLife_EN", "DeadGuy");
            targetListWhitlock.Add("Home_EN", "CallOfTheVoid");

            Dictionary<string, Dictionary<string, string>> targetAltListWhitlock = new Dictionary<string, Dictionary<string, string>>();

            if (LoadedAssetsHandler.GetCharacter("Kneynsberg_CH") != null)
            {
                Dictionary<string, string> targetListWhitlockKneynsberg = new Dictionary<string, string>();
                targetListWhitlockKneynsberg.Add("Blemmigan_EN", "BlemmiganKneynsberg");
                targetAltListWhitlock.Add("Kneynsberg_CH", targetListWhitlockKneynsberg);
            }

            string whitlockDiaID = "Whitlock_Journal_Dialogue";
            YarnProgram whitlockYarn = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Misc/WhitlockJournalScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(whitlockDiaID, whitlockYarn);
            DialogueSO whitlockDialogue = Dialogues.CreateAndAddCustom_DialogueSO(whitlockDiaID, whitlockYarn, whitlockDiaID, "AApocrypha.Whitlock.Journal");

            //AMBROSE JOURNAL
            if (LoadedAssetsHandler.GetCharacter("Ambrose_CH") != null)
            {
                Debug.Log("Journal Mode | Dr. Ambrose journal setup");
                JournalSetup("Ambrose_CH", "Ambrose", "Dr. Ambrose", ["he", "him", "his"], "Dr. Ambrose's Journal", "Overflowing with pages. Lightly singed.", "", targetListAmbrose, targetAltListAmbrose, ambroseDialogue);
            }
            //NAUDIZ 4 JOURNAL
            if (LoadedAssetsHandler.GetCharacter("Naudiz4_CH") != null)
            {
                Debug.Log("Journal Mode | Naudiz 4 journal setup");
                JournalSetup("Naudiz4_CH", "Naudiz4", "Naudiz 4", ["it", "it", "its"], "Naudiz-Class Analysis Module", "Broad-spectrum analysis!", "", targetListNaudiz, targetAltListNaudiz4, naudizDialogue);
            }
            //WHITLOCK JOURNAL
            if (LoadedAssetsHandler.GetCharacter("Whitlock_CH") != null)
            {
                Debug.Log("Journal Mode | Whitlock journal setup");
                JournalSetup("Whitlock_CH", "Whitlock", "Whitlock", ["she", "her", "her"], "Whitlock's Journal", "One journal, many faces.", "", targetListWhitlock, targetAltListWhitlock, whitlockDialogue);
            }
        }            */
        public static void JournalSetup(string foolID, string foolIDnoCH, string foolName, string[] pronouns, string itemName, string flavorText, string hint, Dictionary<string, string> targets, Dictionary<string, Dictionary<string, string>> altTargets, DialogueSO dialogue)
        {
            SpecificOpponentsByJournalListTargeting journalTargeting = ScriptableObject.CreateInstance<SpecificOpponentsByJournalListTargeting>();
            journalTargeting._journalList = targets;
            journalTargeting.getAllUnitSelfSlots = false;
            journalTargeting.targetUnitAllySlots = false;
            journalTargeting.slotOffsets = [0];

            StartJournalDialogueEffect consultEffect = ScriptableObject.CreateInstance<StartJournalDialogueEffect>();
            consultEffect._journalList = targets;
            consultEffect._altJournalList = altTargets;
            consultEffect._dialogue = dialogue;

            Ability consult = new Ability("Consult Journal", "SorasToybox_ConsultJournal_" + foolIDnoCH + "_A")
            {
                Description = foolName + " will consult " + pronouns[2] + " Journal to look up information about the Opposing enemy." +
                "\nValid targets are denoted by this ability's Intents.",
                AbilitySprite = ResourceLoader.LoadSprite("IconLaziestJournalModeIconEver"),
                Cost = [Pigments.Grey],
                //Visuals = CustomVisuals.StaticVisualsSO,
                //AnimationTarget = Targeting.AllUnits,
                Effects =
                [
                    Effects.GenerateEffect(consultEffect, 1, Targeting.Slot_Front),
                ],
                Rarity = Rarity.AbsurdlyRare,
                Priority = Priority.VeryFast
            };
            consult.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc)]);
            consult.AddIntentsToTarget(journalTargeting, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            ExtraAbility_Wearable_SMS journalWear = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            journalWear._extraAbility = consult.GenerateCharacterAbility(true);

            PerformEffect_Item journalItem = new PerformEffect_Item("Journal_" + foolID + "_ID", null, false)
            {
                Item_ID = "Journal_" + foolID + "_ExtraW",
                Name = itemName,
                Flavour = "\"" + flavorText + "\"",
                Description = foolName + " can use Consult Journal to offer " + pronouns[2] + " insights regarding the Opposing enemy." + hint,
                IsShopItem = false,
                ShopPrice = 1,
                StartsLocked = true,
                DoesPopUpInfo = false,
                Icon = ResourceLoader.LoadSprite("JournalItem" + foolIDnoCH),
                TriggerOn = TriggerCalls.OnMiscPlayerTurnStart,
                Effects =
                [
                ],
                EquippedModifiers = [journalWear],
            };
            ItemUtils.JustAddItemSoItCanBeLoaded(journalItem.item);
            LoadedAssetsHandler.GetCharacter(foolID).passiveAbilities.Add(GenerateJournalPassive(foolID, foolName, pronouns, journalItem.item, targets));
        }

        public static BasePassiveAbilitySO GenerateJournalPassive(string foolID, string foolName, string[] pronouns, BaseWearableSO item, Dictionary<string, string> targets)
        {
            // example: Ambrose_CH, Dr. Ambrose, [he, him, his], journalAmbrose.item
            IsCharacterEffectorCondition IsCharacter = ScriptableObject.CreateInstance<IsCharacterEffectorCondition>();
            IsCharacter._passIfTrue = true;

            CharacterHasItemEffectorCondition EmptyHanded = ScriptableObject.CreateInstance<CharacterHasItemEffectorCondition>();
            EmptyHanded._passIfTrue = false;

            CasterSetItemEffect journalSet = ScriptableObject.CreateInstance<CasterSetItemEffect>();
            journalSet._item = item;

            PerformEffectPassiveAbility journalPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            journalPassive.name = "JournalMode_" + foolID + "_PA";
            journalPassive._passiveName = "Journal Mode";
            journalPassive.m_PassiveID = "JournalMode";
            journalPassive.passiveIcon = ResourceLoader.LoadSprite("IconJournalMode");
            journalPassive._characterDescription = "If " + foolName + " has no item on combat start, " + pronouns[0] + " will receive a Journal, letting " + pronouns[1] + " provide information about certain enemies.";
            journalPassive._enemyDescription = "This enemy is well-read, but I doubt that it will want to tell you anything.";
            journalPassive._triggerOn = [TriggerCalls.OnCombatStart];
            journalPassive.conditions = [IsCharacter, EmptyHanded];
            journalPassive.doesPassiveTriggerInformationPanel = true;
            journalPassive.effects =
            [
                Effects.GenerateEffect(journalSet),
            ];

            return journalPassive;
        }

        public static void AddMiscSpeakers()
        {
            Sprite blankSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleDeathmatch = new SpeakerBundle();
            speakerBundleDeathmatch.bundleTextColor = new Color32(128, 0, 0, 255);
            speakerBundleDeathmatch.dialogueSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").dxSound;
            speakerBundleDeathmatch.portrait = blankSprite;
            Dialogues.CreateAndAddCustom_SpeakerData("Deathmatch", speakerBundleDeathmatch, true, false, new SpeakerEmote[0]);

            SpeakerBundle speakerBundleDozer = new SpeakerBundle();
            speakerBundleDozer.bundleTextColor = new Color32(255, 255, 0, 255);
            speakerBundleDozer.dialogueSound = LoadedAssetsHandler.GetEnemy("Dozer_EN").damageSound;
            speakerBundleDozer.portrait = blankSprite;
            Dialogues.CreateAndAddCustom_SpeakerData("Dozer", speakerBundleDozer, true, false, new SpeakerEmote[0]);

        }

    }
}
