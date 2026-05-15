using BepInEx;
using BepInEx.Bootstrap;
using BrutalAPI;
using SorasToybox.CustomPassives;
using SorasToybox.CustomStatusField;
using SorasToybox.Enemies;
using SorasToybox.Items;
using UnityEngine;

namespace SorasToybox //Mod namespace
{
    //Mod Name! It's called this vvvvv
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.0.5")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("000.saltenemies", BepInDependency.DependencyFlags.SoftDependency)]
    public class SorasToybox : BaseUnityPlugin //replace 'ModName' with your mod's name. EX. "BrutalOrchestraMod"
    {
        //I stole the crossmod class from asdfagi :sob:
        public static class CrossMod
        {
            public static bool Colophons = false;
            public static bool EnemyPack = false;
            public static bool GlitchsFreaks = false;
            public static bool SaltEnemies = false;
            public static bool HellIslandFell = false;
            public static bool IntoTheAbyss = false;
            public static bool StewSpecimens = false;
            public static bool Siren = false;
            public static bool Mythos = false;
            public static bool MarmoEnemies = false;
            public static bool UndivineComedy = false;
            public static bool Revelry = false;
            public static bool BismuthBoiler = false;
            public static bool LeonLegion = false;
            public static bool pigmentGilded = false;
            public static bool pigmentRainbow = false;
            public static bool pigmentPeppermint = false;
            public static bool pigmentPink = false;
            public static void Check()
            {
                foreach (var plugin in Chainloader.PluginInfos)
                {
                    var metadata = plugin.Value.Metadata;

                    if (metadata.GUID == "Tairbaz.ColophonConundrum") { Colophons = true; }
                    if (metadata.GUID == "TairbazPeep.EnemyPack") { EnemyPack = true; }
                    if (metadata.GUID == "AnimatedGlitch.GlitchsFreaks") { GlitchsFreaks = true; }
                    if (metadata.GUID == "000.saltenemies") { SaltEnemies = true; }
                    if (metadata.GUID == "Dui_Mauris_Football.Hell_Island_Fell") { HellIslandFell = true; }
                    if (metadata.GUID == "millieamp.intoTheAbyss") { IntoTheAbyss = true; }
                    if (metadata.GUID == "Stew.STEWS_SPECIMENS") { StewSpecimens = true; }
                    if (metadata.GUID == "AnimatedGlitch.Siren") { Siren = true; }
                    if (metadata.GUID == "Tairbaz.MythosFriends") { Mythos = true; }
                    if (metadata.GUID == "Marmo.MarmoEnemies") { MarmoEnemies = true; }
                    if (metadata.GUID == "roundqueen.roundsrevelry") { Revelry = true; }
                    if (metadata.GUID == "WolfaCola.UndivineComedy") { UndivineComedy = true; }
                    if (metadata.GUID == "Devron.BismuthBoiler") { BismuthBoiler = true; }
                    if (metadata.GUID == "millieamp.leonEnemies") { LeonLegion = true; }
                    if (metadata.GUID == "AnimatedGlitch.NumerousLads") { pigmentGilded = true; }
                    if (metadata.GUID == "Devron.UnluckyGuys") { pigmentRainbow = true; }
                    if (metadata.GUID == "embercoral.embercoralsMonsterMixtape") { pigmentPeppermint = true; }
                    //if (metadata.GUID == "Marmo.Sasha") { pigmentPink = true; }
                }
            }
        }
        public void Awake()
        {
            Logger.LogInfo("Wake up."); //sends a message to the logging console confirming your mod is able to read info in this bracket

            //to add a seperate file, simply put the name of the .cs file and put .Add(); after. 
            //Characters
            //YourCharacter.Add(); //change this to whatever filename your fool's data is using. EX. TechCH.Add();
            //Add custom stuff
            CustomStatus.Add();
            CustomPassive.Add();
            //Log custom stuff (Do config thing with it)
            Logger.LogInfo("Custom Effects in effect.");

            //Add items
            SentientArcanite.Add();
            Setset.Add();
            BastardNimbus.Add();
            //Log items (Do config thing with it)
            Logger.LogInfo("New Items in inventory.");

            //Add enemies
            Zizlet.Add();
            //Log enemies (Do config thing with it)
            Logger.LogInfo("New Toys to break.");
            
            //Final log
            Logger.LogInfo("Wake up.");
        }
    }
}
