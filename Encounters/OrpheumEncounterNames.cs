using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;

namespace SorasToybox.Encounters
{
    public static class Orph
    {
        //easymode
        public static class MusicMan
        {
            public static string Easy => "Zone02_MusicMan_Easy_EnemyBundle";
            public static string Med => "Zone02_MusicMan_Medium_EnemyBundle";
        }
        public static class Scrungie
        {
            public static string Hard => "Zone02_Scrungie_Hard_EnemyBundle";
        }
        public static class Jumble
        {
            public static class Blue
            {
                public static string Med => "Zone02_JumbleGuts_Hollowing_Medium_EnemyBundle";
            }
            public static class Purple
            {
                public static string Med => "Zone02_JumbleGuts_Flummoxing_Medium_EnemyBundle";
            }
            
        }
        public static class Spoggle
        {
            public static class Red
            {
                public static string Med => "Zone02_Spoggle_Writhing_Medium_EnemyBundle";
            }
            public static class Purple
            {
                public static string Med => "Zone02_Spoggle_Resonant_Medium_EnemyBundle";
            }
        }
        public static class Revola
        {
            public static string Hard => "Zone02_Revola_Hard_EnemyBundle";
        }

        //hawthorne
        public static class Enigma
        {
            public static string Easy => "Zone02_Enigma_Easy_EnemyBundle";
        }
        public static class Sigil
        {
            public static string Med => "Zone02_Sigil_Medium_EnemyBundle";
        }
        public static class Rabies
        {
            public static string Med => "Zone02_Lyssarabhas_Medium_EnemyBundle";
        }
        public static class Bot
        {
            public static class Red
            {
                public static string Med => "Zone02_RedBot_Medium_EnemyBundle";
            }
            public static class Yellow
            {
                public static string Med => "Zone02_YellowBot_Medium_EnemyBundle";
            }
            public static class Blue
            {
                public static string Med => "Zone02_BlueBot_Medium_EnemyBundle";
            }
            public static class Purple
            {
                public static string Med => "Zone02_PurpleBot_Medium_EnemyBundle";
            }
        }
        public static class Evileye
        {
            public static string Hard => "Zone02_Evileye_Hard_EnemyBundle";
        }
        public static class Shooter
        {
            public static string Easy => "Zone02_SkeletonShooter_Easy_EnemyBundle";
            public static string Med => "Zone02_SkeletonShooter_Medium_EnemyBundle";
        }

        //HARDmode
        public static class H
        {
            public static class MusicMan
            {
                public static string Easy => "H_Zone02_MusicMan_Easy_EnemyBundle";
                public static string Med => "H_Zone02_MusicMan_Medium_EnemyBundle";
            }
            public static class Scrungie
            {
                public static string Med => "H_Zone02_Scrungie_Medium_EnemyBundle";
            }
            public static class Jumble
            {
                public static class Blue
                {
                    public static string Med => "H_Zone02_JumbleGuts_Hollowing_Medium_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_Zone02_JumbleGuts_Flummoxing_Medium_EnemyBundle";
                }

                //marmo
                public static class Unstable
                {
                    public static string Easy => "Marmo_Zone02_Digital_JumbleGuts_Easy";
                    public static string Hard => "Marmo_Zone02_Digital_JumbleGuts_Hard";
                }
                //ita
                public static class Gilded
                {
                    public static string Med => "H_Zone02_JumbleGuts_Affluent_Medium_EnemyBundle";
                }
                public static class YellowGrey
                {
                    public static string Med => "H_Zone02_JumbleGuts_Surging_Medium_EnemyBundle";
                }
                public static class GreyYellow
                {
                    public static string Med => "H_Zone02_JumbleGuts_Surging_Medium_EnemyBundle";
                }
                //aapocrypha
                public static class Rainbow
                {
                    public static string Med => "H_Zone02_CoruscatingJumbleGuts_Medium_EnemyBundle";
                }
            }
            public static class Spoggle
            {
                public static class Red
                {
                    public static string Med => "H_Zone02_Spoggle_Writhing_Medium_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_Zone02_Spoggle_Resonant_Medium_EnemyBundle";
                }

                //marmo
                public static class Unstable
                {
                    public static string Easy => "Marmo_Zone02_Mechanical_Spoggle_Easy";
                    public static string Hard => "Marmo_Zone02_Mechanical_Spoggle_Hard";
                }

                //aapocrypha
                public static class RedPurpleSplit
                {
                    public static string Med => "H_Zone02_DevotedSpoggle_Medium_EnemyBundle";
                }
                public static class PurpleRedSplit
                {
                    public static string Med => "H_Zone02_DevotedSpoggle_Medium_EnemyBundle";
                }
                public static class BlueYellowSplit
                {
                    public static string Med => "H_Zone02_CellularSpoggle_Medium_EnemyBundle";
                }
                public static class YellowBlueSplit
                {
                    public static string Med => "H_Zone02_CellularSpoggle_Medium_EnemyBundle";
                }
            }
            public static class Maniskin
            {
                public static string Hard => "H_Zone02_InnerChild_Hard_EnemyBundle";
            }
            public static class Sacrifice
            {
                public static string Hard => "H_Zone02_WrigglingSacrifice_Hard_EnemyBundle";
            }
            public static class Revola
            {
                public static string Hard => "H_Zone02_Revola_Hard_EnemyBundle";
            }
            public static class Conductor
            {
                public static string Med => "H_Zone02_Conductor_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Conductor_Hard_EnemyBundle";
            }

            //hawthorne
            public static class Enigma
            {
                public static string Easy => "H_Zone02_Enigma_Easy_EnemyBundle";
                public static string Med => "H_Zone02_Enigma_Medium_EnemyBundle";
            }
            public static class Something
            {
                public static string Easy => "H_Zone02_Something_Easy_EnemyBundle";
                public static string Med => "H_Zone02_Something_Medium_EnemyBundle";
            }
            public static class Crow
            {
                public static string Med => "H_Zone02_Crow_Medium_EnemyBundle";
            }
            public static class Freud
            {
                public static string Med => "H_Zone02_Freud_Medium_EnemyBundle";
            }
            public static class Camera
            {
                public static string Med => "H_Zone02_MechanicalLens_Medium_EnemyBundle";
            }
            public static class Delusion
            {
                public static string Easy => "H_Zone02_Delusion_Easy_EnemyBundle";
                public static string Med => "H_Zone02_Delusion_Medium_EnemyBundle";
            }
            public static class Flower
            {
                public static class Yellow
                {
                    public static string Easy => "H_Zone02_YellowFlower_Easy_EnemyBundle";
                    public static string Med => "H_Zone02_YellowFlower_Medium_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_Zone02_PurpleFlower_Medium_EnemyBundle";
                }
            }
            public static class Sigil
            {
                public static string Med => "H_Zone02_Sigil_Medium_EnemyBundle";
            }
            public static class Solvent
            {
                public static string Easy => "H_Zone02_Solvent_Easy_EnemyBundle";
            }
            public static class WindSong
            {
                public static string Med => "H_Zone02_WindSong_Medium_EnemyBundle";
            }
            public static class DeadGod
            {
                public static string Hard => "Salt_DeadGod_Orpheum_Bundle";
            }
            public static class Tortoise
            {
                public static string Hard => "H_Zone02_StalwartTortoise_Hard_EnemyBundle";
            }
            public static class Butterfly
            {
                public static string Med => "H_Zone02_SpectreWitchFamiliar_Medium_EnemyBundle";
            }
            public static class Rabies
            {
                public static string Med => "H_Zone02_Lyssarabhas_Medium_EnemyBundle";
            }
            public static class Maw
            {
                public static string Med => "H_Zone02_Maw_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Maw_Hard_EnemyBundle";
            }
            public static class Bot
            {
                public static class Red
                {
                    public static string Med => "H_Zone02_RedBot_Medium_EnemyBundle";
                }
                public static class Yellow
                {
                    public static string Med => "H_Zone02_YellowBot_Medium_EnemyBundle";
                }
                public static class Blue
                {
                    public static string Med => "H_Zone02_BlueBot_Medium_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_Zone02_PurpleBot_Medium_EnemyBundle";
                }
            }
            public static class Crystal
            {
                public static string Med => "H_Zone02_CrystallineCorpseEater_Medium_EnemyBundle";
            }
            public static class Dragon
            {
                public static string Hard => "H_Zone02_TheDragon_Hard_EnemyBundle";
            }
            public static class Evileye
            {
                public static string Med => "H_Zone02_Evileye_Medium_EnemyBundle";
            }
            public static class YellowAngel
            {
                public static string Med => "H_Zone02_YellowAngel_Medium_EnemyBundle";
            }
            public static class Shooter
            {
                public static string Easy => "H_Zone02_SkeletonShooter_Easy_EnemyBundle";
                public static string Med => "H_Zone02_SkeletonShooter_Medium_EnemyBundle";
            }
            public static class Wednesday
            {
                public static string Med => "H_Zone02_Wednesday_Medium_EnemyBundle";
            }
            public static class Solitaire
            {
                public static string Med => "H_Zone02_Solitaire_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Solitaire_Hard_EnemyBundle";
            }
            public static class Foxtrot
            {
                public static string Easy => "H_Zone02_Foxtrot_Easy_EnemyBundle";
            }
            public static class Author
            {
                public static string Med => "H_Zone02_Author_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Author_Hard_EnemyBundle";
            }
            public static class Insider
            {
                public static string Med => "H_Zone02_Insider_Medium_EnemyBundle";
            }
            public static class Nameless
            {
                public static string Med => "H_Zone02_Nameless_Med_EnemyBundle";
            }
            public static class Untitled
            {
                public static string Hard => "H_Zone02_UntitledEN_Hard_EnemyBundle";
            }
            public static class Nume
            {
                public static string Med => "H_Zone02_Nume_Med_EnemyBundle";
            }
            public static class Whale
            {
                public static string Easy => "H_Zone02_TheWhale_Easy_EnemyBundle";
                public static string Med => "H_Zone02_TheWhale_Med_EnemyBundle";
            }
            public static class ReverseFalseHydra
            {
                public static string Hard => "H_Zone02_ReverseFalseHydra_Med_EnemyBundle";
            }


            //marmo
            public static class Errant
            {
                public static string Med => "Marmo_Errant_Medium_Bundle";
                public static string Hard => "Marmo_Errant_Hard_Bundle";
            }
            //tay
            public static class Shuffler
            {
                public static string Easy => "RR_Zone02_Shawled_Shuffler_Easy_EnemyBundle";
                public static string Med => "RR_Zone02_Shawled_Shuffler_Medium_EnemyBundle";
            }

            //colophon

            public static class Colophon
            {
                public static class Purple
                {
                    public static string Med => "DelightedMedium";
                }
                public static class Yellow
                {
                    public static string Med => "MaladjustedMedium";
                }
                //ita
                public static class Green
                {
                    public static string Med => "DisaffectedMedium";
                }
                //aapocrypha
                public static class Peppermint
                {
                    public static string Hard => "H_Zone02_ColophonSaccharine_Hard_EnemyBundle";
                }
                public static class RedPurpleSplit
                {
                    public static string Med => "H_Zone02_ColophonHeretical_Medium_EnemyBundle";
                }
                public static class PurpleRedSplit
                {
                    public static string Med => "H_Zone02_ColophonHeretical_Medium_EnemyBundle";
                }
            }

            //undivine
            public static class ClayChild
            {
                public static string Easy => "ClayChildEasy";
                public static string Med => "ClayChildMedium";
            }
            public static class Clergy
            {
                public static string Med => "ClergyMedium";
                public static string Hard => "ClergyHard";
            }
            public static class Sonoduct
            {
                public static string Hard => "SonoHard";
            }

            //glitch
            public static class Dancer
            {
                public static string Easy => "BDancerEasy";
                public static string Med => "BDancerMed";
            }
            public static class Frostbite
            {
                public static string Easy => "FrostbiteEasy";
                public static string Med => "FrostbiteMed";
            }

            //psi
            public static class Suckler
            {
                public static string Easy => "SucklerEasy";
                public static string Med => "SucklerMed";
            }

            //dui
            public static class Moone
            {
                public static string Easy => "H_Zone02_Moone_Easy_EnemyBundle";
                public static string Med => "H_Zone02_Moone_Medium_EnemyBundle";
            }
            public static class Heehoo
            {
                public static string Med => "H_Zone02_Heehoo_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Heehoo_Hard_EnemyBundle";
            }
            public static class Thunderdome
            {
                public static string Med => "H_Zone02_Thunderdome_Medium_EnemyBundle";
            }

            //mythos
            public static class Byakhee
            {
                public static string Med => "ByakheeMedium";
            }
            public static class Vampire
            {
                public static string Med => "StarVampireMedium";
            }

            //aapocrypha
            public static class Anomaly
            {
                public static class Unbound
                {
                    public static string Easy => "H_Zone02_UnboundAnomaly_Easy_EnemyBundle";
                    public static string Med => "H_Zone02_UnboundAnomaly_Medium_EnemyBundle";
                }
                public static class Encased
                {
                    public static string Med => "H_Zone02_EncasedAnomaly_Medium_EnemyBundle";
                }
                public static class Sharpened
                {
                    public static string Med => "H_Zone02_SharpenedAnomaly_Medium_EnemyBundle";
                }
            }
            public static class SculptorBird
            {
                public static string Med => "H_Zone02_SculptorBird_Medium_EnemyBundle";
            }
            public static class RiftMiniboss
            {
                public static string Hard => "H_Zone02_Rift_Hard_EnemyBundle";
            }
            public static class Bloatfinger
            {
                public static string Med => "H_Zone02_Bloatfinger_Medium_EnemyBundle";
            }
            public static class Uttershroom
            {
                public static string Med => "H_Zone02_Uttershroom_Medium_EnemyBundle";
            }
            public static class Aggregates
            {
                public static class Yellow
                {
                    public static string Easy => "H_Zone02_YellowAggregate_Easy_EnemyBundle";
                    public static string Med => "H_Zone02_YellowAggregate_Medium_EnemyBundle";
                }
            }
            public static class CobaltCurator
            {
                public static string Med => "H_Zone02_CobaltCurator_Medium_EnemyBundle";
            }


            //ita

            public static class ELamia
            {
                public static string Med => "H_Zone02_ELamia_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_ELamia_Hard_EnemyBundle";
            }

            public static class Rosebush
            {
                public static string Med => "H_Zone02_Rosebush_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Rosebush_Hard_EnemyBundle";
            }

            public static class Receiver
            {
                public static string Med => "H_Zone02_Receiver_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Receiver_Hard_EnemyBundle";
            }

            public static class Omission
            {
                public static string Med => "H_Zone02_Omission_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Omission_Hard_EnemyBundle";
            }

            public static class Starman
            {
                public static string Med => "H_Zone02_Starman_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Starman_Hard_EnemyBundle";
            }

            public static class Foppy
            {
                public static string Med => "H_Zone02_Foppy_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Foppy_Hard_EnemyBundle";
            }

            public static class Mistaken
            {
                public static string Med => "H_Zone02_Mistaken_Medium_EnemyBundle";
            }

            public static class Doll
            {
                public static string Med => "H_Zone02_Doll_Medium_EnemyBundle";
            }

            public static class Transient
            {
                public static string Med => "H_Zone02_Transient_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Transient_Hard_EnemyBundle";
            }

            public static class Poser
            {
                public static string Med => "H_Zone02_Poser_Medium_EnemyBundle";
                public static string Hard => "H_Zone02_Poser_Hard_EnemyBundle";
            }

            public static class Dollhouse
            {
                public static string Hard => "H_Zone02_Dollhouse_Hard_EnemyBundle";
            }


            //bismuth boiler

            public static class Feaster
            {
                public static string Med => "FeasterMedium";
            }

            //toybox gang
            public static class SERVICE
            {
                public static string Med => "SERVICEMedium";
            }

        }
    }
}