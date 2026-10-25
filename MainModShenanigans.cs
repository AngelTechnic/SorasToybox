global using BrutalAPI;
global using UnityEngine;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;    
using HarmonyLib;
using MonoMod.RuntimeDetour;
using SorasToybox.CustomEffects;
using SorasToybox.CustomPassives;
using SorasToybox.CustomPigment;
using SorasToybox.CustomStatusField;
using SorasToybox.Encounters;
using SorasToybox.Enemies;
using SorasToybox.Events;
using SorasToybox.Fools;
using SorasToybox.Items;
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using SorasToybox.MiscPatches;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SorasToybox //Mod namespace
{
    //Mod Name! It's called this vvvvv
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.5.1")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("TairbazPeep.EnemyPack", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AnimatedGlitch.GlitchsFreaks", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AnimatedGlitch.Siren", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Tairbaz.MythosFriends", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("000.saltenemies", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("sofanthiel.sofanthielsfools", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("asdfagi.A_Apocrypha", BepInDependency.DependencyFlags.SoftDependency)]

    public class SorasToybox : BaseUnityPlugin
    {
        public static AssetBundle assetbundle;
        //I stole the crossmod class from asdfagi :sob:

        public static ConfigEntry<bool> journalmode;
        public static ConfigEntry<bool> extradebug;
        public static ConfigEntry<bool> freefooltester;
        public static ConfigEntry<bool> gardenantagonist;
        public static ConfigEntry<bool> altboundarymusic;
        public static ConfigEntry<bool> altforgottenmusic;

        //i stole the following from ITA directly
        public static void PCall(Action call)
        {
            try { call(); }
            catch (Exception ex)
            {
                try
                {
                    Debug.LogError(call.GetMethodInfo().ReflectedType + " " + call.GetMethodInfo().Name + " FUCKING FAILED TO GET ADDED");
                }
                catch
                {
                    Debug.LogError("some fucking function failed to get added");
                }

                Debug.LogError(ex.ToString() + ex.Message + ex.StackTrace);
            }
        }
        public static class NotificationHook
        {
            public static List<Action<string, object, object>> BeforeActions;
            public static List<Action<string, object, object>> AfterActions;
            public static void AddAction(Action<string, object, object> action, bool before = false)
            {
                if (BeforeActions == null) BeforeActions = new List<Action<string, object, object>>();
                if (AfterActions == null) AfterActions = new List<Action<string, object, object>>();
                if (before) BeforeActions.Add(action);
                else AfterActions.Add(action);
            }
            public static void PostNotification(Action<CombatManager, string, object, object> orig, CombatManager self, string notificationName, object sender, object args)
            {
                if (BeforeActions != null) foreach (Action<string, object, object> action in BeforeActions) action(notificationName, sender, args);
                orig(self, notificationName, sender, args);
                if (AfterActions != null) foreach (Action<string, object, object> action in AfterActions) action(notificationName, sender, args);
            }
            public static void Setup()
            {
                IDetour hook = new Hook(typeof(CombatManager).GetMethod(nameof(CombatManager.PostNotification), ~BindingFlags.Default), typeof(NotificationHook).GetMethod(nameof(PostNotification), ~BindingFlags.Default));
            }
        }
        public static class CrossMod
        {
            public static bool IntoTheAbyss = false;
            public static bool EnemyPack = false;
            public static bool GlitchsFreaks = false;
            public static bool Siren = false;
            public static bool MythosFriends = false;
            public static bool SaltEnemies = false;
            public static bool Sofanthiels = false;
            public static bool AApocrypha = false;
            public static void Check()
            {
                foreach (var plugin in Chainloader.PluginInfos)
                {
                    var metadata = plugin.Value.Metadata;

                    if (metadata.GUID == "000.saltenemies") { SaltEnemies = true; }
                    if (metadata.GUID == "millieamp.intoTheAbyss") { IntoTheAbyss = true; }
                    if (metadata.GUID == "TairbazPeep.EnemyPack") { EnemyPack = true; }
                    if (metadata.GUID == "AnimatedGlitch.GlitchsFreaks") { GlitchsFreaks = true;  }
                    if (metadata.GUID == "AnimatedGlitch.Siren") { Siren = true; }
                    if (metadata.GUID == "Tairbaz.MythosFriends") { MythosFriends = true; }
                    if (metadata.GUID == "sofanthiel.sofanthielsfools") { Sofanthiels = true; }
                    if (metadata.GUID == "asdfagi.A_Apocrypha") { AApocrypha = true; }
                }
            }
        }
        public void Awake()
        {
            Logger.LogInfo("Morning."); //sends a message to the logging console confirming your mod is able to read info in this bracket

            var harmony = new Harmony("Wavetamer.SorasToybox");
            harmony.PatchAll();

            //config shenanigans!
            ConfigFile config = new ConfigFile(System.IO.Path.Combine(Paths.ConfigPath, "SorasToybox.cfg"), true);
            journalmode = config.Bind("Gameplay - Misc", "Journal Mode", false, "EXPERIMENTAL - Set this to true to enable Journal Mode, giving certain fools the means to reveal more lore about themselves and their world of origin.");
            extradebug = config.Bind("Meta", "Extra Debug Logs", false, "Set this to true to fill the console with logs related to every small thing that gets added.");
            freefooltester = config.Bind("Meta", "Free Fool Event Tester", false, "Set this to true to enable the free fool event tester.");
            //gardenantagonist = config.Bind("Gameplay - Misc", "Garden Deathmatch Encounter", false, "Set this to true to add the Deathmatch to the Garden, in addition to the Abyss. Note that this is not the intended experience, and really should only be used for debugging and whatnot, or as a last resort if you're REALLY unlucky with Abyss spawns and still want to fight the Deathmatch.");
            altboundarymusic = config.Bind("Cosmetic Changes", "Alternate Katalixi Music", false, "Alternative music for Katalixi, from Into The Abyss. This is a personal thing I added for my own sake. If true, uses \"Side Quest\" by Magic Sword. If false, uses \"The End Is Now\" by Besauce.");
            altforgottenmusic = config.Bind("Cosmetic Changes", "Alternate Nobody Music", false, "Alternative music for Nobody, from another Nobody. This is a personal thing I added for my own sake. If true, uses \"MX\" by RENREN. If false, uses, uh, the default theme?");
            assetbundle = AssetBundle.LoadFromMemory(ResourceLoader.ResourceBinary("sorastoybox"));

            //CROSSMOD thank you
            CrossMod.Check();

            //to add a seperate file, simply put the name of the .cs file and put .Add(); after. 
            //Characters
            //YourCharacter.Add(); //change this to whatever filename your fool's data is using. EX. TechCH.Add();
            //Add custom stuff
            CustomStatus.Add();
            CustomPassives.CustomPassive.Add();
            CustomPigments.Add();
            //Log custom stuff (Do config thing with it)
            SaltExcessPassive.Add();

            Logger.LogInfo("Custom Effects in effect.");
 

            //Add enemies
            SEARCH.Add();
            TANGO.Add();
            if (CrossMod.Sofanthiels)
            {
                Slatecarnate.Add();
            }

            //Siren Crossmod
            if (CrossMod.Siren)
            {
                GeodeGertar.Add();
                DendriteDesibon.Add();
            }    
            //ITA Crossmod
            if (CrossMod.IntoTheAbyss)
            {
                Litany.Add();
                GearYinimro.Add();
                BurningShame.Add();
                Deathmatch.Add();
                Crashout.Add();
                FakeFaceless.Add();
            }


            //ITA AND Salt
            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                Dozer.Add();
                ParadoxYinimro.Add();
                //Citygrazer.Add();
                NowhereMan.Add();
            }


            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies && LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Destabilized_ID"))
            {
                HumorIrid.Add();
            }

            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends && CrossMod.SaltEnemies)
            {
                Primus.Add();
                SuspiciousMung.Add();
            }
            //add encounters
            SEARCHEncounters.Add();
            TANGOEncounters.Add();


            if (CrossMod.IntoTheAbyss)
            {
                //TestEncounter.Add();
                LitanyEncounters.Add();
                GearYinimroEncounters.Add();
                CrashoutEncounters.Add();
                CompatAbyssEncounters.Add();
                DeathmatchEncounter.Add();
                /*if (gardenantagonist.Value)
                {
                    GardenDeathmatchEncounter.Add();
                }*/
            }

            if (CrossMod.Siren)
            {
                DesibonEncounter.Add();
            }

            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                DozerEncounters.Add();
                ParadoxYinimroEncounters.Add();
                //GrazerEncounter.Add();
                NowhereManEncounters.Add();
            }


            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends)
            {
                SusMungEncounter.Add();
            }

            //Add fools
            MercurieFool.Add();
            KarmaFool.Add();
            SenekTasbeht.Add();
            SenekFool.Add();
            if (CrossMod.AApocrypha)
            {
                WhhvayFool.Add();
            }

            JournalHandler.AddMiscSpeakers();
            if (journalmode.Value) 
            {
                JournalHandler.Add();
            }


            if (CrossMod.IntoTheAbyss)
            {
                MercurieFreeEvent.Add();
                KarmaFreeEvent.Add();
                SenekFreeEvent.Add();
            }
            if (CrossMod.AApocrypha && CrossMod.Siren)
            {
                WhhvayFreeEvent.Add();
            }
            //for testing events
            if (freefooltester.Value)
            {
                FreeFoolEventTester.Add();
            }

            //oh boy.
            GazerFlavorEvent.Add();
            //AbyssBossEdits.Add();
            Heliotrope.Setup();
            Heliotrope.HeliotropeRoom();



            //The Buggening Cometh
            TagCharIfExists("LongLiver_CH", "Bug");
            TagCharIfExists("Bimini_CH", "Bug");
            TagCharIfExists("GallGagger_CH", "Bug");
            TagCharIfExists("Eunice_CH", "Bug");
            TagCharIfExists("DeathStealer_CH", "Bug");
            TagCharIfExists("Cross_CH", "Bug");
            if (CrossMod.IntoTheAbyss)
            {
                TagCharIfExists("Chae_CH", "Bug");
                TagCharIfExists("Soreka_CH", "Zoincaillan");
            }
            if (CrossMod.MythosFriends)
            {
                TagCharIfExists("Wuz_CH", "Bug");
                TagCharIfExists("992_CH", "Bug");
            }
            if (CrossMod.Siren)
            {
                TagCharIfExists("Izzy_CH", "Bug");
                TagCharIfExists("ButterflySwarm_CH", "Bug");
            }
            TagCharIfExists("Lich_CH", "Bug");
            TagCharIfExists("Six_CH", "Bug");
            if (CrossMod.Sofanthiels)
            {
                TagCharIfExists("Guwraso_CH", "Bug");
                TagCharIfExists("Regali_CH", "Bug");
            }

            TagEnemyIfExists("Keko_EN", "Bug");
            TagEnemyIfExists("Kekastle_EN", "Bug");
            TagEnemyIfExists("Bronzo_Bananas_Friendly_EN", "Friendly");
            TagEnemyIfExists("Ungod_Friendly_EN", "Friendly");
            
            TagEnemyIfExists("Kekingdom_EN", "Bug");
            TagEnemyIfExists("Keklung_EN", "Bug");
            if (CrossMod.EnemyPack)
            {
                TagEnemyIfExists("Kekapex_EN", "Bug");
                TagEnemyIfExists("Lipbug_EN", "Bug");
                TagEnemyIfExists("TheFountainofYouth_EN", "Bug");
                TagEnemyIfExists("GigglingUsurper_EN", "Friendly"); 
            }
            if (CrossMod.MythosFriends)
            {
                TagEnemyIfExists("WormthatWalks_EN", "Bug");
                TagEnemyIfExists("Chthonian_EN", "Bug");
            }
            if (CrossMod.GlitchsFreaks)
            {
                TagEnemyIfExists("Enno_EN", "Bug");
                TagEnemyIfExists("NotAn_EN", "Bug");
            }
            if (CrossMod.Siren)
            {
                TagEnemyIfExists("Tumult_EN", "Bug");
                TagEnemyIfExists("TumultShell_EN", "Bug");
                TagEnemyIfExists("Scuttlebunk_EN", "Bug");
                TagEnemyIfExists("PetrifiedPuker_EN", "Bug");
                TagEnemyIfExists("RustedRetcher_EN", "Bug");
                TagEnemyIfExists("Scuttlebunk_EN", "Bug");
                TagEnemyIfExists("LivingPiscina_EN", "Bug");
                TagEnemyIfExists("SwarmedSubfossil_BOSS", "Bug");
                TagEnemyIfExists("TorporousTardigrade_BOSS", "Bug");
            }
            //April's gang
            TagEnemyIfExists("Patch_Assistant_1_Red_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_2_Red_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_3_Red_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_4_Red_EN", "Friendly");

            TagEnemyIfExists("Patch_Assistant_1_Blue_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_2_Blue_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_3_Blue_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_4_Blue_EN", "Friendly");

            TagEnemyIfExists("Patch_Assistant_1_Yellow_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_2_Yellow_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_3_Yellow_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_4_Yellow_EN", "Friendly");

            TagEnemyIfExists("Patch_Assistant_1_Purple_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_2_Purple_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_3_Purple_EN", "Friendly");
            TagEnemyIfExists("Patch_Assistant_4_Purple_EN", "Friendly");

            TagEnemyIfExists("ScrapBomb_EN", "Friendly");
            TagEnemyIfExists("ImmortalFigures_EN", "Friendly");
            TagEnemyIfExists("TheSludge_EN", "Friendly");
            TagEnemyIfExists("NeuronActivator_EN", "Friendly");
            if (CrossMod.SaltEnemies)
            {
                TagEnemyIfExists("DeadPixel_EN", "Bug");
                TagEnemyIfExists("Chiito_EN", "Bug");
                TagEnemyIfExists("Spectre_EN", "Bug");
                TagEnemyIfExists("RedBot_EN", "Bug");
                TagEnemyIfExists("GreyBot_EN", "Bug");
            }


            if (CrossMod.IntoTheAbyss)
            {
                TagEnemyIfExists("ButterflyEffect_EN", "Bug");
                TagEnemyIfExists("Mistaken_EN", "Bug");
                TagEnemyIfExists("Mistake_EN", "Bug");
                TagEnemyIfExists("Omission_EN", "Bug");
                TagEnemyIfExists("Oversight_EN", "Bug");
                TagEnemyIfExists("Lexem_EN", "Robot");
                TagEnemyIfExists("Lexem_EN", "Zoincaillan");
                TagEnemyIfExists("ContainmentBlock_EN", "Friendly");
            }
            if (CrossMod.AApocrypha)
            {
                TagEnemyIfExists("ApatelosOrguis_EN", "Bug");
            }

            //Memento Mori enemies :]
            TagEnemyIfExists("Keklets_EN", "Bug");
            TagEnemyIfExists("Topkek_EN", "Bug");
            TagEnemyIfExists("MaleDreamEater_EN", "Bug");
            TagEnemyIfExists("FemaleDreamEater_EN", "Bug");

            Debug.Log("The buggening concludes.");




            //Add items
            //sirencrossmod check
            if (CrossMod.Siren)
            {
                StoneWateringCan.Add();
            }

            SentientArcanite.Add();
            UrbanSurvival.Add();
            AMsSeveredHead.Add();
            Everpresence.Add();

            //remember character order is: Mercurie, Karma, Whhvay
            //Osman Unlocks
            Milkshake.Add();
            Quesadilla.Add();
            Flyswatter.Add();
            if (CrossMod.AApocrypha)
            {
                AstralHilt.Add();
            }

            //Heaven unlocks
            TempusBoostHarness.Add();
            HealthHat.Add();
            CustomTooledCartridge.Add();
            PsidocFool.Add();//psidoc's normally spawned by a specific crossmod item but who give a shit
            if (CrossMod.AApocrypha)
            {
                Diagnopsis.Add();
            }



            //Doula Unlocks
            if (CrossMod.EnemyPack)
            {
                FortyProof.Add();
                BastardNimbus.Add();
                ShootingGallery.Add();
                if (CrossMod.AApocrypha)
                {
                    Latchknife.Add();
                }
            }
            //March Unlocks
            FriendlyGearYinimro.Add();
            if (CrossMod.GlitchsFreaks)
            {
                ClockKing.Add();
                GhostPepper.Add();
                CrumblingCookie.Add();
                if (CrossMod.AApocrypha)
                {
                    MurderersQuill.Add();
                }
            }

            //Nobody Unlocks
            if (CrossMod.IntoTheAbyss && LoadedAssetsHandler.LoadedEnemies.ContainsKey("Nobody_BOSS"))
            {
                WorldOfHassle.Add();
                CharybdisItem.Add();
                UnlicensedTonic.Add();
                if (CrossMod.AApocrypha)
                {
                    SelfishHighHeels.Add();
                }
            }
            //Katalixi Unlocks
            if (CrossMod.IntoTheAbyss && LoadedAssetsHandler.LoadedEnemies.ContainsKey("Nolocimes_Batretne_BOSS"))
            {
                LonelyHike.Add();
                PrincessItem.Add();
                RunicLens.Add();
                if (CrossMod.AApocrypha)
                {
                    ExtrudedPolygon.Add();
                }
            }

            //Blue Skies Unlocks
            if (CrossMod.SaltEnemies)
            {
                MemoryOfArtrodus.Add();
                MegaHammer.Add();
                BugSpray.Add();
                if (CrossMod.AApocrypha)
                {
                    Screwyoudriver.Add();
                }
            }
            //DM Unlocks go here I think.
            EntrenchingTool.Add();
            StretchMarks.Add();
            AuralViolation.Add();
            ProcessedSludge.Add();
            FoodChain.Add();    
            LaughingGas.Add();
            BrokenZipper.Add();
            ExposureTherapy.Add();
            AtomSmasher.Add();
            MyFace.Add();
            PunchBowl.Add();
            VenzaFool.Add();
            FishOutOfWater.Add();
            PlasticFork.Add();
            ToySaw.Add();
            DriveItem.Add();
            BadPublicity.Add();
            PassionlessSuffering.Add();
            DivisionBell.Add();
            FilmProjector.Add();
            WeatherCycle.Add();
            Identitypolitik.Add();
            Eternity.Add();
            LucifersHead.Add();
            SymbolOnFlesh.Add();
            BSTRD.Add();

            //Siren deathmatch unlocks
            if (CrossMod.Siren)
            {
                CapitalismInACan.Add();
                DeadbeatSummer.Add();
                DeadMansPartyHat.Add();
                ModernGiant.Add();
                RottenKintsugi.Add();
            }

            //Mythos Friends deathmatch unlocks
            if (CrossMod.MythosFriends)
            {
                Fishbones.Add();
                //WaterBalloons.Add();
                //WeatherBalloon.Add();
                //PocketPlasm.Add();
                HollowedFang.Add();
                //whateverbullshiticomeupwithforyhaele.Add();
                TrickBible.Add();
                NegativeSpaceWedgie.Add();
            }

            //ST deathmatch unlocks
            MemoryOfGriyadin.Add();
            Setset.Add();
            InfinityTasbeht.Add();
            Dartboard.Add();
            if (CrossMod.AApocrypha)
            {
                MemoryOfSyndomera.Add();
            }

            //Sofanthiels Deathmatch Unlocks
            if (CrossMod.Sofanthiels)
            {
                Roughly3SnatchedChains.Add();
            }

            //Log items (Do config thing with it)
            Logger.LogInfo("New Items in inventory.");


            if (SorasToybox.altboundarymusic.Value)
            {
                LoadedAssetsHandler.GetEnemyBundle("Katalixi_BOSS")._musicEventReference = "event:/SorasMusic/Enemies/Bosses/AltKatalixiMusic/SideQuest";
            }

            if (SorasToybox.altforgottenmusic.Value)
            {
                LoadedAssetsHandler.GetEnemyBundle("Nobody_BOSS")._musicEventReference = "event:/SorasMusic/Enemies/Bosses/AltNobodyMusic/MX";
            }

            //Final log
            Logger.LogInfo("Wake up.");

            //Broken Pigment glossary cuz why not
            //if statement to check if a keyword already exists?
            //{
               // LoadedDBsHandler.GlossaryDB.AddNewKeyword(new GlossaryKeywords("Broken Pigment", "Broken Pigment is a mostly inert pigment type produced by specific enemies or party members. Broken Pigment is useless for abilities, serving only to clog your pigment tray; however, should Overflow trigger, all existing Broken Pigment will shatter into nothing."));
            //}
            
        }
        static void TagCharIfExists(string ID, string unitType)
        {
            if (LoadedAssetsHandler.LoadedCharacters.ContainsKey(ID))
            {
                LoadedAssetsHandler.GetCharacter(ID).unitTypes.Add(unitType);
                if (extradebug.Value)
                {
                    Debug.Log($"Tags & Types | successfully tagged character {LoadedAssetsHandler.GetCharacter(ID)._characterName} ({ID}) as {unitType}");
                }

            }
        }
        static void TagEnemyIfExists(string ID, string unitType)
        {
            if (LoadedAssetsHandler.LoadedEnemies.ContainsKey(ID))
            {
                LoadedAssetsHandler.GetEnemy(ID).unitTypes.Add(unitType);
                if (extradebug.Value)
                {
                    Debug.Log($"Tags & Types | successfully tagged enemy {LoadedAssetsHandler.GetEnemy(ID)._enemyName} ({ID}) as {unitType}");
                }

            }
        }
    }
}
