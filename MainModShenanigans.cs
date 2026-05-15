using BepInEx;
using BepInEx.Bootstrap;
using BrutalAPI;
global using SorasToybox.CustomPassives;
global using SorasToybox.CustomEffects;
global using SorasToybox.Enemies;
using SorasToybox.CustomStatusField;

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
    public class SorasToybox : BaseUnityPlugin 

        //I stole the crossmod class from asdfagi :sob:
        public static class CrossMod
        {
            public static bool IntoTheAbyss = false;

            public static void Check()
            {
                foreach (var plugin in Chainloader.PluginInfos)
                {
                    var metadata = plugin.Value.Metadata;

                    if (metadata.GUID == "millieamp.intoTheAbyss") { IntoTheAbyss = true; }
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
