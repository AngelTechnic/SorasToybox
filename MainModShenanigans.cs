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
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.1.6")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("000.saltenemies", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("frozenhawk.sofanthielsFools", BepInDependency.DependencyFlags.SoftDependency)]
    public class SorasToybox : BaseUnityPlugin
    {
        //I stole the crossmod class from asdfagi :sob:
        public static class CrossMod
        {
            public static bool IntoTheAbyss = false;
            public static bool SaltEnemies = false;
            public static bool Sofanthiels = false;
            public static void Check()
            {
                foreach (var plugin in Chainloader.PluginInfos)
                {
                    var metadata = plugin.Value.Metadata;

                    if (metadata.GUID == "millieamp.intoTheAbyss") { IntoTheAbyss = true; }
                    if (metadata.GUID == "000.saltenemies") { SaltEnemies = true; }
                    if (metadata.GUID == "frozenhawk.sofanthielsFools") { Sofanthiels = true; }
                }
            }
        }
        public void Awake()
        {
            Logger.LogInfo("Morning."); //sends a message to the logging console confirming your mod is able to read info in this bracket



            //to add a seperate file, simply put the name of the .cs file and put .Add(); after. 
            //Characters
            //YourCharacter.Add(); //change this to whatever filename your fool's data is using. EX. TechCH.Add();
            //Add custom stuff
            CustomStatus.Add();
            CustomPassive.Add();
            CustomPigments.Add();
            //Log custom stuff (Do config thing with it)
            Logger.LogInfo("Custom Effects in effect.");

            //CROSSMOD thank you
            CrossMod.Check();


            //Add items
            SentientArcanite.Add();
            Setset.Add();
            //BastardNimbus.Add();
            //Log items (Do config thing with it)
            Logger.LogInfo("New Items in inventory.");

            //Add fools
            KarmaFool.Add();
            Logger.LogInfo("She's here.");

            //Add enemies
            SEARCH.Add();
            if (CrossMod.Sofanthiels)
            {
                Slatecarnate.Add();
            }

            //Add encounters
            TestEncounter.Add();

      
            //Log enemies (Do config thing with it)
            Logger.LogInfo("New Toys to play with.");
            
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
