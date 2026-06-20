using BepInEx;
using BepInEx.Bootstrap;
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
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using SorasToybox.Events;

namespace SorasToybox //Mod namespace
{
    //Mod Name! It's called this vvvvv
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.3.6")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.SoftDependency)]
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
                BurningShame.Add();
                Deathmatch.Add();
            }


            //ITA AND Salt
            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                Dozer.Add();
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
            try
            {
                SEARCHEncounters.Add();
            }
            catch
            {
                Debug.Log("SEARCH encounters failed to load.");
            }

            if (CrossMod.IntoTheAbyss)
            {
                try
                {
                    TestEncounter.Add();
                }
                catch
                {
                    Debug.Log("Test encounter failed to load.");
                }

                try
                {
                    LitanyEncounters.Add();
                }
                catch 
                {
                    Debug.Log("Litany encounters failed to load.");
                }
                try
                {
                    DeathmatchEncounter.Add();
                }
                catch
                {
                    Debug.Log("\"THAT\" encounter failed to load.");
                }

            }
            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                try
                {
                    DozerEncounters.Add();
                }
                catch
                {
                    Debug.Log("Dozer encounters failed to load.");
                }

            }


            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends)
            {
                try
                {
                    SusMungEncounter.Add();
                }
                catch
                {
                    Debug.Log("Mung encounter failed to load.");
                }

            }

            //Add fools
            if (CrossMod.IntoTheAbyss)
            {
                MercurieFool.Add();
            }
            KarmaFool.Add();
            JournalHandler.AddMiscSpeakers();
            JournalHandler.Add();

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
            AMsSeveredHead.Add();

            //remember character order is: Mercurie, Karma
            //Osman Unlocks
            Milkshake.Add();

            //Doula Unlocks
            if (CrossMod.EnemyPack)
            {
                FortyProof.Add();
                BastardNimbus.Add();
            }
            //March Unlocks
            if (CrossMod.GlitchsFreaks)
            {
                GhostPepper.Add();
            }
            //Blue Skies Unlocks
            if (CrossMod.SaltEnemies)
            {
                MemoryOfArtrodus.Add();
                MegaHammer.Add();
            }
            //DM Unlocks go here I think.
            EntrenchingTool.Add();

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
