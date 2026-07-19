using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BrutalAPI;
using SorasToybox.CustomStatusField;
using SorasToybox.CustomPassives;
using SorasToybox.CustomEffects;
using SorasToybox.CustomPigment;
using SorasToybox.MiscPatches;
using SorasToybox.Enemies;
using SorasToybox.Fools;
using SorasToybox.Items;
using HarmonyLib;
using UnityEngine;
using SorasToybox.Encounters;
using SorasToybox.Events;

namespace SorasToybox //Mod namespace
{
    //Mod Name! It's called this vvvvv
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.4.3")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.HardDependency)]
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
        public static ConfigEntry<bool> gardenantagonist;
        public static ConfigEntry<bool> altboundarymusic;
        public static ConfigEntry<bool> altforgottenmusic;
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
            gardenantagonist = config.Bind("Gameplay - Misc", "Garden Deathmatch Encounter", false, "Set this to true to add the Deathmatch to the Garden, in addition to the Abyss. Note that this is not the intended experience, and really should only be used for debugging and whatnot, or as a last resort if you're REALLY unlucky with Abyss spawns and still want to fight the Deathmatch.");
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
            if (CrossMod.Sofanthiels)
            {
                Slatecarnate.Add();
            }
            //ITA Crossmod
            if (CrossMod.IntoTheAbyss)
            {
                Litany.Add();
                GearYinimro.Add();
                BurningShame.Add();
                Deathmatch.Add();
            }


            //ITA AND Salt
            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                Dozer.Add();
                ParadoxYinimro.Add();
                Citygrazer.Add();
                NowhereMan.Add();
            }


            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies && LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Destabilized_ID"))
            {
                HumorIrid.Add();
            }

            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends)
            {
                Primus.Add();
                SuspiciousMung.Add();
            }
            //add encounters
            SEARCHEncounters.Add();


            if (CrossMod.IntoTheAbyss)
            {
                TestEncounter.Add();
                LitanyEncounters.Add();
                GearYinimroEncounters.Add();
                CompatAbyssEncounters.Add();
                DeathmatchEncounter.Add();
                if (gardenantagonist.Value)
                {
                    GardenDeathmatchEncounter.Add();
                }
            }

            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                DozerEncounters.Add();
                ParadoxYinimroEncounters.Add();
                GrazerEncounter.Add();
                NowhereManEncounters.Add();
            }


            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends)
            {
                SusMungEncounter.Add();
            }

            //Add fools
            if (CrossMod.IntoTheAbyss)
            {
                MercurieFool.Add();
            }
            KarmaFool.Add();
            JournalHandler.AddMiscSpeakers();
            if (journalmode.Value) 
            {
                JournalHandler.Add();
            }


            if (CrossMod.IntoTheAbyss)
            {
                MercurieFreeEvent.Add();
                KarmaFreeEvent.Add();
            }
            //for testing events
            //FreeFoolEventTester.Add();


            //Log enemies (Do config thing with it)





            //Add items
            SentientArcanite.Add();
            UrbanSurvival.Add();
            AMsSeveredHead.Add();

            //remember character order is: Mercurie, Karma
            //Osman Unlocks
            Milkshake.Add();
            Quesadilla.Add();

            //Heaven unlocks
            TempusBoostHarness.Add();
            HealthHat.Add();



            //Doula Unlocks
            if (CrossMod.EnemyPack)
            {
                FortyProof.Add();
                BastardNimbus.Add();
            }
            //March Unlocks
            if (CrossMod.GlitchsFreaks)
            {
                FriendlyGearYinimro.Add();
                ClockKing.Add();
                GhostPepper.Add();
            }

            //Nobody Unlocks
            if (CrossMod.IntoTheAbyss && LoadedAssetsHandler.LoadedEnemies.ContainsKey("Nobody_BOSS"))
            {
                WorldOfHassle.Add();
                CharybdisItem.Add();
            }
            //Katalixi Unlocks
            if (CrossMod.IntoTheAbyss && LoadedAssetsHandler.LoadedEnemies.ContainsKey("Nolocimes_Batretne_BOSS"))
            {
                LonelyHike.Add();
                PrincessItem.Add();
            }

            //Blue Skies Unlocks
            if (CrossMod.SaltEnemies)
            {
                MemoryOfArtrodus.Add();
                MegaHammer.Add();
            }
            //DM Unlocks go here I think.
            EntrenchingTool.Add();
            AuralViolation.Add();
            ProcessedSludge.Add();
            FoodChain.Add();
            LaughingGas.Add();
            AtomSmasher.Add();
            DriveItem.Add();
            BadPublicity.Add();
            FilmProjector.Add();
            LucifersHead.Add();


            //ST deathmatch unlocks
            MemoryOfGriyadin.Add();
            Setset.Add();


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
    }
}
