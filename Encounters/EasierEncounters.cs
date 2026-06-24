using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public static class SaltExtensions
    {
        public static T GetRandom<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
        public static int GetFirstIDFromArary(this object[] array, object search)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (search.Equals(array[i])) return i;
            }
            return -1;
        }
        public static bool ContainsID(this object[] array, int ID)
        {
            return ID >= 0 && ID < array.Length;
        }
    }
    public class AddTo
    {
        string bundle;

        public AddTo(string _bundle) => bundle = _bundle;

        //EXAMPLE HOW TO USE
        public void Example()
        {
            AddTo bundle1 = new AddTo("H_Zone_1_PretendEnemy_Bundle");
            bundle1.AddRandomGroup("Mung_EN", "Mung_EN", "Mung_EN", "Mung_EN", "Mung_EN");
            bundle1.AddRandomGroup("Mung_EN", "Mung_EN", "Mung_EN", "Mung_EN");
            bundle1.AddRandomGroup("Mung_EN", "Mung_EN", "Mung_EN");
            bundle1.AddRandomGroup("Mung_EN", "Mung_EN");
            bundle1.AddRandomGroup("Mung_EN");

            //example how to use quick access names
            bundle1.AddRandomGroup(Jumble.Red, Jumble.Yellow, Jumble.Blue, Jumble.Purple);
            bundle1.AddRandomGroup(Spoggle.Yellow, Spoggle.Blue, Spoggle.Red, Spoggle.Purple);
        }

        //RANDOM
        public void AddRandomGroup(string enemy1 = "", string enemy2 = "", string enemy3 = "", string enemy4 = "", string enemy5 = "")
        {
            List<string> ret = new List<string>();
            if (enemy1 != "") ret.Add(enemy1);
            if (enemy2 != "") ret.Add(enemy2);
            if (enemy3 != "") ret.Add(enemy3);
            if (enemy4 != "") ret.Add(enemy4);
            if (enemy5 != "") ret.Add(enemy5);
            if (ret.Count <= 0) return;
            AddRandomGroup(ret.ToArray());
        }
        public void SimpleAddGroup(int num1 = 0, string enemy1 = "", int num2 = 0, string enemy2 = "", int num3 = 0, string enemy3 = "", int num4 = 0, string enemy4 = "", int num5 = 0, string enemy5 = "")
        {
            List<string> ret = new List<string>();
            if (enemy1 != "") for (int i = 0; i < num1; i++) ret.Add(enemy1);
            if (enemy2 != "") for (int i = 0; i < num2; i++) ret.Add(enemy2);
            if (enemy3 != "") for (int i = 0; i < num3; i++) ret.Add(enemy3);
            if (enemy4 != "") for (int i = 0; i < num4; i++) ret.Add(enemy4);
            if (enemy5 != "") for (int i = 0; i < num5; i++) ret.Add(enemy5);
            if (ret.Count <= 0) return;
            AddRandomGroup(ret.ToArray());
        }

        public void AddRandomGroup(string[] enemies)
        {
            if (!MultiENExistInternal(enemies))
            {
                //if (IntoTheAbyss.DebugVer) Debug.LogWarning("Failed to add random group to " + bundle);
                return;
            }
            AddRandomGroup_Internal(new RandomEnemyGroup(enemies));
        }
        public void AddRandomGroup_Internal(RandomEnemyGroup group)
        {
            if (!BundleExist(bundle)) return;
            if (!BundleRandom(bundle)) return;
            List<RandomEnemyGroup> list2 = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle(bundle))._enemyBundles);
            list2.Add(group);
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle(bundle))._enemyBundles = list2;
        }
        //thought about it, not making static bundle methods.

        //DEBUGGING
        public static List<string> Printeds = new List<string>();
        public static bool EnemyExist(string name)
        {
            if (!LoadedAssetsHandler.LoadedEnemies.Keys.Contains(name) && LoadedAssetsHandler.LoadEnemy(name) == null) { if (!Printeds.Contains(name)) { Debug.LogWarning("Enemy: " + name + " is null"); Printeds.Add(name); } return false; }
            return LoadedAssetsHandler.GetEnemy(name) != null;
        }
        public static bool BundleExist(string name)
        {
            if (!LoadedAssetsHandler.LoadedEnemyBundles.Keys.Contains(name) && LoadedAssetsHandler.LoadEnemyBundle(name) == null) { Debug.LogWarning("Bundle: " + name + " is null"); return false; }
            return LoadedAssetsHandler.GetEnemyBundle(name) != null;
        }
        public static bool BundleRandom(string name, bool DoDebug = true)
        {
            if (!BundleExist(name)) return false;
            if (!(LoadedAssetsHandler.GetEnemyBundle(name) is RandomEnemyBundleSO) && DoDebug) Debug.LogWarning("Bundle: " + name + " is not random, checked for random");
            return LoadedAssetsHandler.GetEnemyBundle(name) is RandomEnemyBundleSO;
        }
        public static bool BundleStatic(string name)
        {
            if (!BundleExist(name)) return false;
            //if (IntoTheAbyss.DebugVer) if (BundleRandom(name, false)) Debug.LogWarning("Bundle: " + name + "is random, checked for static");
            return !BundleRandom(name, false);
        }
        public static bool MultiENExistInternal(string[] names)
        {
            foreach (string name in names)
            {
                if (!EnemyExist(name)) return false;
            }
            return true;
        }
    }

    //ENEMY NAMES QUICK ACCESS
    public static class Jumble
    {
        public static string Red => "JumbleGuts_Clotted_EN";
        public static string Yellow => "JumbleGuts_Waning_EN";
        public static string Blue => "JumbleGuts_Hollowing_EN";
        public static string Purple => "JumbleGuts_Flummoxing_EN";
        public static string Grey => "RusticJumbleguts_EN";
        public static string Gray => Grey;
        public static string Unstable => "JumbleGuts_Digital_EN";

        public static string Green => "FuckYouGuy_EN";

        public static string Gilded => "AffluentJumbleGuts_EN";

        public static string Irid => "AxiomaticJumbleGuts_EN";

        public static string Entropic => "SuperpositionedJumbleGuts_EN";

        public static string Clusterfuck => "SynestheticJumbleGuts_EN";

        public static string Fuck => Clusterfuck;

        public static string White => "JumbleGuts_Veiled_EN";

        public static string RedBlue => "JumbleGuts_Bonded_EN";
        public static string BlueRed => RedBlue;
        public static string YellowPurple => "JumbleGuts_Parasitic_EN";
        public static string PurpleYellow => YellowPurple;
        public static string BlueYellow => "JumbleGuts_Annoying_EN";
        public static string YellowBlue => BlueYellow;

        public static string PurpleRed => "JumbleGuts_Malignant_EN";
        public static string RedPurple => PurpleRed;
        public static string PurpleBlue => "JumbleGuts_Artistic_EN";
        public static string BluePurple => PurpleBlue;
        public static string YellowRed => "JumbleGuts_Waxing_EN";
        public static string RedYellow => YellowRed;



        public static string RedGrey => "JumbleGuts_Decanting_EN";
        public static string GreyRed => RedGrey;

        public static string YellowGrey => "JumbleGuts_Surging_EN";
        public static string GreyYellow => YellowGrey;

        public static string BlueGrey => "JumbleGuts_Wellspring_EN";
        public static string GreyBlue => BlueGrey;

        public static string PurpleGrey => "JumbleGuts_Elapsing_EN";
        public static string GreyPurple => PurpleGrey;

        //aapocrypha
        public static string Rainbow => "CoruscatingJumbleGuts_EN";
    }
    public static class Spoggle
    {
        public static string Yellow => "Spoggle_Spitfire_EN";
        public static string Blue => "Spoggle_Ruminating_EN";
        public static string Red => "Spoggle_Writhing_EN";
        public static string Purple => "Spoggle_Resonant_EN";
        public static string Grey => "MortalSpoggle_EN";
        public static string Gray => Grey;
        public static string Unstable => "Spoggle_Mechanical_EN";
        public static string Green => "MycotoxicSpoggle_EN";
        public static string Irid => "AkashicSpoggle_EN";
        public static string Entropic => "OccultatedSpoggle_EN";

        public static string BlueYellow => "IchthyosatedSpoggle_EN";
        public static string YellowBlue => BlueYellow;
        public static string PurpleBlue => "EclipsedSpoggle_EN";
        public static string BluePurple => PurpleBlue;
        public static string YellowRed => "AmphibiousSpoggle_EN";
        public static string RedYellow => YellowRed;

        public static string RedBlue => "FoamingSpoggle_EN";
        public static string BlueRed => RedBlue;
        public static string YellowPurple => "PoolingSpoggle_EN";
        public static string PurpleYellow => YellowPurple;
        public static string PurpleRed => "NecromanticSpoggle_EN";
        public static string RedPurple => PurpleRed;
        public static string BlueYellowSplit => "CellularSpoggle_EN";
        public static string YellowBlueSplit => BlueYellowSplit;
        public static string RedPurpleSplit => "DevotedSpoggle_EN";
        public static string PurpleRedSplit => RedPurpleSplit;
    }
    public static class Flower
    {
        public static string Yellow => "YellowFlower_EN";
        public static string Purple => "PurpleFlower_EN";
        public static string Red => "RedFlower_EN";
        public static string Blue => "BlueFlower_EN";
        public static string Grey => "GreyFlower_EN";
        public static string Gray => Grey;
    }
    public static class Noses
    {
        public static string Red => "ProlificNosestone_EN";
        public static string Blue => "ScatterbrainedNosestone_EN";
        public static string Yellow => "SweatingNosestone_EN";
        public static string Purple => "MesmerizingNosestone_EN";
        public static string Grey => "UninspiredNosestone_EN";
        public static string Gray => Grey;
    }
    public static class Colophon
    {
        public static string Red => "ColophonDefeated_EN";
        public static string Blue => "ColophonComposed_EN";
        public static string Yellow => "ColophonMaladjusted_EN";
        public static string Purple => "ColophonDelighted_EN";
        //ita
        public static string Green => "ColophonDisaffected_EN";
        public static string Grey => "ColophonImmaculate_EN";
        public static string Gray => Grey;
        //aapocrypha
        public static string RedBlueSplit => "ColophonDualistic_EN";
        public static string BlueRedSplit => RedBlueSplit;
        public static string Peppermint => "ColophonSaccharine_EN";
        public static string Peppermint2 => "ColophonSaccharineAlt_EN";
        public static string RedPurpleSplit => "ColophonHeretical_EN";
        public static string PurpleRedSplit => RedPurpleSplit;
    }
    public static class Bots
    {
        public static string Yellow => "YellowBot_EN";
        public static string Purple => "PurpleBot_EN";
        public static string Red => "RedBot_EN";
        public static string Blue => "BlueBot_EN";
        public static string Grey => "GreyBot_EN";
        public static string Gray => Grey;

    }

    public static class Signs
    {
        public static string Yellow => "YieldSign_EN";
        public static string Purple => "WildlifeSign_EN";
        public static string Red => "StopSign_EN";
        public static string Blue => "HospitalSign_EN";
        public static string Grey => "ParkingSign_EN";
        public static string Gray => Grey;
        public static string Green => "ExitSign_EN";
    }
    public static class Statues
    {
        public static string Up => "UpStairs_EN";
        public static string Down => "DownStairs_EN";
        public static string Cross => "Icon_EN";
        public static string Wolf => "Fleet_EN";

    }

    public static class Ecstasy
    {
        public static string Yellow => "Ecstasy_Yellow_EN";
        public static string Purple => "Ecstasy_Purple_EN";
        public static string Red => "Ecstasy_Red_EN";
        public static string Blue => "Ecstasy_Blue_EN";


        
        public static string[] List = [Red, Blue, Yellow, Purple];
        public static string Random => List.GetRandom();
    }
    public static class Logos
    {
        public static string Red => "CrimsonLogos_EN";
        public static string Blue => "CeruleanLogos_EN";
        public static string Yellow => "AureateLogos_EN";
        public static string Purple => "RegentLogos_EN";
        public static string Broken => "DiscordantLogos_EN";
    }

    public static class Symbols
    {
        public static string Red => "RedSymbol_EN";
        public static string Blue => "BlueSymbol_EN";
        public static string Yellow => "YellowSymbol_EN";
        public static string Purple => "PurpleSymbol_EN";
    }
    public static class HiddenBloatfinger
    {
        public static string OrpheumFake => "SculptorBirdSculpture_EN";
        public static string Orpheum => "BloatfingerHiddenOrpheum_EN";

        public static string[] OrpheumList = [OrpheumFake, Orpheum];
        public static string OrpheumRandom => OrpheumList.GetRandom();
    }
    public static class Anomalies
    {
        public static string Unbound => "UnboundAnomaly_EN";
        public static string Encased => "EncasedAnomaly_EN";
        public static string Sharpened => "SharpenedAnomaly_EN";

        public static string[] VariantList = [Encased, Sharpened];
        public static string Random => VariantList.GetRandom();

    }
    public static class Enlightened
    {
        public static string Vessel => "EnlightenedVessel_EN";
        public static string Spirit => "EnlightenedSpirit_EN";
    }
    public static class Frostbites
    {
        public static string Normal => "Frostbite_EN";
        public static string Tall => "Frostbite_Bipedal_EN";
        public static string Heart => "ExternalIncubator_EN";
        public static string Gilded => "Frostbite_Gilded_EN";
    }
    public static class Orguis
    {
        public static string Iridescent => "EphialtesOrguis_EN";
        public static string Irid => Iridescent;
        public static string Clusterfuck => "ApatelosOrguis_EN";
        public static string Entropic => "PanopticOrguis_EN";
        public static string White => "HamalatOrguis_EN";
    }
    public static class Martians
    {
        public static string Red => "RedMartian_EN";
        public static string Blue => "BlueMartian_EN";
        public static string Yellow => "YellowMartian_EN";
        public static string Purple => "PurpleMartian_EN";
        public static string Green => "GreenMartian_EN";
        public static string Grey => "GreyMartian_EN";
        public static string Gray => Grey;
    }
    public static class Aggregates
    {
        public static string Red => "RedAggregate_EN";
        public static string Blue => "BlueAggregate_EN";
        public static string Yellow => "YellowAggregate_EN";
        public static string Purple => "PurpleAggregate_EN";
        public static string Green => "GreenAggregate_EN";
        public static string Grey => "GreyAggregate_EN";
        public static string Gray => Grey;
        public static string Iridescent => "IridescentAggregate_EN";
        public static string Entropic => "EntropicAggregate_EN";
        public static string Clusterfuck => "ClusterfuckAggregate_EN";
        public static string White => "WhiteAggregate_EN";
    }
    public static class Enemies
    {
        public static string Skinning => "SkinningHomunculus_EN";
        public static string Shivering => "ShiveringHomunculus_EN";
        public static string Minister => "GigglingMinister_EN";
        public static string Sacrifice => "WrigglingSacrifice_EN";
        public static string Solvent => "LivingSolvent_EN";
        public static string Tank => "RealisticTank_EN";
        public static string Suckle => "SilverSuckle_EN";
        public static string Camera => "MechanicalLens_EN";
        public static string Unmung => "TeachaMantoFish_EN";
        public static string Mungling => "MunglingMudLung_EN";
        public static string Shooter => "SkeletonShooter_EN";
        public static string Shuffler => "Shawled_Shuffler_EN";
        public static string Tane => "Tanehineri_EN";

        public static string Swine => "UnculturedSwine_EN";
        public static string Polyp => "SpectralPolyp_EN";
        public static string Puker => "PetrifiedPuker_EN";
        public static string Phalaris => "GreatPhalaris_EN";
    }
}
