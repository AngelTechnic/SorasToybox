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

namespace SorasToybox //Mod namespace
{
    //Mod Name! It's called this vvvvv
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.3.3")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("TairbazPeep.EnemyPack", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AnimatedGlitch.GlitchsFreaks", BepInDependency.DependencyFlags.SoftDependency)]
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
            Logger.LogInfo("Salt Enemies Crossmod: " + CrossMod.SaltEnemies);
            Logger.LogInfo("Into The Abyss Crossmod: " + CrossMod.IntoTheAbyss);
            Logger.LogInfo("Sofanthiels Crossmod: " + CrossMod.Sofanthiels);

            //to add a seperate file, simply put the name of the .cs file and put .Add(); after. 
            //Characters
            //YourCharacter.Add(); //change this to whatever filename your fool's data is using. EX. TechCH.Add();
            //Add custom stuff
            CustomStatus.Add();
            CustomPassive.Add();
            CustomPigments.Add();
            //Log custom stuff (Do config thing with it)
            Logger.LogInfo("Custom Effects in effect.");


            //Add fools
            if (CrossMod.IntoTheAbyss)
            {
                MercurieFool.Add();
            }
            KarmaFool.Add();
            Logger.LogInfo("She's here.");

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

            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends)
            {
                Primus.Add();
                SuspiciousMung.Add();
            }
            //Add encounters
            TestEncounter.Add();

            if (CrossMod.IntoTheAbyss)
            {
                LitanyEncounters.Add();
                DeathmatchEncounter.Add();
            }
            if (CrossMod.IntoTheAbyss && CrossMod.SaltEnemies)
            {
                DozerEncounters.Add();
            }

            if (CrossMod.IntoTheAbyss && CrossMod.MythosFriends)
            {
                SusMungEncounter.Add();
            }

            
            //Log enemies (Do config thing with it)
            Logger.LogInfo("New Toys to play with.");




            //Add items
            SentientArcanite.Add();
            Setset.Add();
            //Doula Unlocks
            if (CrossMod.EnemyPack)
            {
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
                MegaHammer.Add();
            }

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
