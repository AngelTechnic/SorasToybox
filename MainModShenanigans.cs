using BepInEx;
using BrutalAPI;
using UnityEngine;
using SorasToybox.CustomStatusField;
using SorasToybox.CustomPassives;
using SorasToybox.Items;

namespace SorasToybox //Mod namespace
{
    //Mod Name! It's called this vvvvv
    [BepInPlugin("Wavetamer.SorasToybox", "Sora's Toybox", "0.0.4")] //my name, the mod name, and THE mod name. amnd the version which i will forget to change lmao
    //HARD DEPENDENCIES: The following is a list of required dependencies:
    [BepInDependency("BrutalOrchestra.BrutalAPI", BepInDependency.DependencyFlags.HardDependency)]
    //SOFT DEPENDENCIES: The following is a list of dependencies this mod CAN rely on, but does not require:
    [BepInDependency("millieamp.intoTheAbyss", BepInDependency.DependencyFlags.SoftDependency)]
    public class SorasToybox : BaseUnityPlugin //replace 'ModName' with your mod's name. EX. "BrutalOrchestraMod"
    {
        public void Awake()
        {
            Logger.LogInfo("Wake up."); //sends a message to the logging console confirming your mod is able to read info in this bracket

            //to add a seperate file, simply put the name of the .cs file and put .Add(); after. 
            //Characters
            //YourCharacter.Add(); //change this to whatever filename your fool's data is using. EX. TechCH.Add();
            CustomStatus.Add();
            CustomPassive.Add();
            SentientArcanite.Add();
            Setset.Add();
        }
    }
}
