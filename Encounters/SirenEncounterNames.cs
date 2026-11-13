using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public static class Siren
    {
        public static bool Exists => LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheSiren_Zone1");

        public static class H
        {
            public static class Ecstasy
            {
                public static class Red
                {
                    public static string Med => "H_ZoneSiren_Ecstasy13_Medium_EnemyBundle";
                }
                public static class Blue
                {
                    public static string Med => "H_ZoneSiren_Ecstasy09_Medium_EnemyBundle";
                }
                public static class Yellow
                {
                    public static string Med => "H_ZoneSiren_Ecstasy02_Medium_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_ZoneSiren_Ecstasy87_Medium_EnemyBundle";
                }
            }
            public static class Piscina
            {
                public static string Hard => "PiscinaHard";
            }
            public static class Tumult
            {
                public static string Easy => "TumultEasy";
                public static string Med => "TumultMed";
            }
            public static class Boiler
            {
                public static string Easy => "BoilerEasy";
                public static string Med => "BoilerMed";
            }
            public static class Tassnn
            {
                public static string Easy => "TassnnEasy";
                public static string Med => "TassnnMed";
            }
            public static class Olmic
            {
                public static string Med => "OlmicMed";
                public static string Hard => "OlmicHard";
            }
            public static class Phalaris
            {
                public static string Hard => "PhalarisHard";
            }
            public static class Scuttlebunk
            {
                public static string Med => "ScuttlebunkMed";
                public static string Hard => "ScuttlebunkHard";
            }

            //ita
            public static class Soothsayer
            {
                public static string Med => "H_ZoneSiren_Soothsayer_Medium_EnemyBundle";
            }

            public static class Erelim
            {
                public static string Med => "H_ZoneSiren_Erelim_Medium_EnemyBundle";
            }

            public static class Fanatic
            {
                public static string Med => "H_ZoneSiren_Fanatic_Medium_EnemyBundle";
            }
            public static class Orphan
            {
                public static string Med => "H_ZoneSiren_Orphan_Medium_EnemyBundle";
            }

            public static class Jumble
            {
                public static class BlueGrey
                {
                    public static string Med => "H_ZoneSiren_JumbleGuts_Wellspring_Medium_EnemyBundle";
                }
                public static class GreyBlue
                {
                    public static string Med => "H_ZoneSiren_JumbleGuts_Wellspring_Medium_EnemyBundle";
                }
            }

            //hif
            public static class OneShooter
            {
                public static string Med => "H_ZoneSiren_OneShooter_Medium_EnemyBundle";
            }

            //aapocrypha
            public static class SculptorBird
            {
                public static string Med => "H_ZoneSiren_SculptorBird_Medium_EnemyBundle";
            }
            public static class WinterLantern
            {
                public static string Med => "H_ZoneSiren_WinterLantern_Medium_EnemyBundle";
            }
            public static class HazardHauler
            {
                public static string Med => "H_ZoneSiren_HazardHauler_Medium_EnemyBundle";
            }
            public static class Aggregates
            {
                public static class Blue
                {
                    public static string Easy => "H_ZoneSiren_BlueAggregate_Easy_EnemyBundle";
                    public static string Med => "H_ZoneSiren_BlueAggregate_Medium_EnemyBundle";
                }
            }

            //toybox
            public static class Freezer
            {
                public static string Easy => "H_ZoneSiren_Freezer_Easy_EnemyBundle";
                public static string Med => "H_ZoneSiren_Freezer_Medium_EnemyBundle";
            }
        }
    }
}