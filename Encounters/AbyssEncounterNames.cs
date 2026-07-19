using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public static class Abyss
    {
        public static bool Exists => LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3");

        public static class H
        {
            public static class Jumble
            {
                public static class Entropic
                {
                    public static string Med => "H_ZoneAbyss_SuperpositionedJumbleGuts_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_SuperpositionedJumbleGuts_Hard_EnemyBundle";
                }
                public static class Iridescent
                {
                    public static string Med => "H_ZoneAbyss_AxiomaticJumbleGuts_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_AxiomaticJumbleGuts_Hard_EnemyBundle";
                }
            }
            public static class Spoggle
            {
                public static class Entropic
                {
                    public static string Med => "H_ZoneAbyss_OccultatedSpoggle_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_OccultatedSpoggle_Hard_EnemyBundle";
                }
                public static class Iridescent
                {
                    public static string Med => "H_ZoneAbyss_AkashicSpoggle_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_AkashicSpoggle_Hard_EnemyBundle";
                }
            }
            public static class Eater
            {
                public static string Hard => "H_ZoneAbyss_Eater_Hard_EnemyBundle";
            }
            public static class Casual
            {
                public static string Hard => "H_ZoneAbyss_Casual_Hard_EnemyBundle";
            }
            public static class Kcolclock
            {
                public static string Hard => "H_ZoneAbyss_Kcolclock_Hard_EnemyBundle";
            }
            public static class WRK
            {
                public static string Easy => "H_ZoneAbyss_WRK_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_WRK_Medium_EnemyBundle";
            }
            public static class Bear
            {
                public static string Med => "H_ZoneAbyss_Bear_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Bear_Hard_EnemyBundle";
            }
            public static class Streetlight
            {
                public static string Easy => "H_ZoneAbyss_Streetlight_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_Streetlight_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Streetlight_Hard_EnemyBundle";
            }
            public static class Vindicta
            {
                public static string Med => "H_ZoneAbyss_Vindicta_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Vindicta_Hard_EnemyBundle";
            }
            public static class Faceless
            {
                public static string Med => "H_ZoneAbyss_Faceless_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Faceless_Hard_EnemyBundle";
            }
            public static class Transient
            {
                public static string Med => "H_ZoneAbyss_Transient_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Transient_Hard_EnemyBundle";
            }
            public static class Plato
            {
                public static string Med => "H_ZoneAbyss_Plato_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Plato_Hard_EnemyBundle";
            }
            public static class Symbol
            {
                public static class Red
                {
                    public static string Med => "H_ZoneAbyss_RedSymbol_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_RedSymbol_Hard_EnemyBundle";
                }
                public static class Blue
                {
                    public static string Med => "H_ZoneAbyss_BlueSymbol_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_BlueSymbol_Hard_EnemyBundle";
                }
                public static class Yellow
                {
                    public static string Med => "H_ZoneAbyss_YellowSymbol_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_YellowSymbol_Hard_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_ZoneAbyss_PurpleSymbol_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_PurpleSymbol_Hard_EnemyBundle";
                }
            }
            public static class Sibling
            {
                public static class Red
                {
                    public static string Med => "H_ZoneAbyss_RedSibling_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_RedSibling_Hard_EnemyBundle";
                }
                public static class Blue
                {
                    public static string Med => "H_ZoneAbyss_BlueSibling_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_BlueSibling_Hard_EnemyBundle";
                }
                public static class Yellow
                {
                    public static string Med => "H_ZoneAbyss_YellowSibling_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_YellowSibling_Hard_EnemyBundle";
                }
                public static class Purple
                {
                    public static string Med => "H_ZoneAbyss_PurpleSibling_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_PurpleSibling_Hard_EnemyBundle";
                }
            }
            public static class Lexem
            {
                public static string Easy => "H_ZoneAbyss_Lexem_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_Lexem_Medium_EnemyBundle";
            }
            public static class Reminder
            {
                public static class Osman
                {
                    public static string Med => "H_ZoneAbyss_Reminder_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_Reminder_Hard_EnemyBundle";
                }
                public static class Nowak
                {
                    public static string Med => "H_ZoneAbyss_Lingering_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_Lingering_Hard_EnemyBundle";
                }
                public static class Doula
                {
                    public static string Med => "H_ZoneAbyss_Nauseating_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_Nauseating_Hard_EnemyBundle";
                }
                public static class March
                {
                    public static string Med => "H_ZoneAbyss_Demented_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_Demented_Hard_EnemyBundle";
                }
                public static class Heaven
                {
                    public static string Med => "H_ZoneAbyss_Converted_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_Converted_Hard_EnemyBundle";
                }
                public static class Nobody
                {
                    public static string Med => "H_ZoneAbyss_Neglected_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_Neglected_Hard_EnemyBundle";
                }
            }
            public static class YesMan
            {
                public static string Med => "H_ZoneAbyss_YesMan_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_YesMan_Hard_EnemyBundle";
            }
            public static class EyePalm
            {
                public static string Easy => "H_ZoneAbyss_Medamaude_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_Medamaude_Medium_EnemyBundle";
            }
            public static class Receiver
            {
                public static string Easy => "H_ZoneAbyss_Receiver_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_Receiver_Medium_EnemyBundle";
            }
            public static class WanderFellow
            {
                public static string Easy => "H_ZoneAbyss_WanderFellow_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_WanderFellow_Medium_EnemyBundle";
            }
            public static class Malachai
            {
                public static string Hard => "H_ZoneAbyss_Malachai_Hard_EnemyBundle";
            }

            //aapocrypha
            public static class Simulacrum
            {
                public static string Hard => "H_ZoneAbyss_Simulacrum_Hard_EnemyBundle";
            }
            public static class MachineGnomes
            {
                public static string Med => "H_ZoneAbyss_MachineGnomes_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_MachineGnomes_Hard_EnemyBundle";
            }
            public static class Orguis
            {
                public static class Iridescent
                {
                    public static string Med => "H_ZoneAbyss_EphialtesOrguis_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_EphialtesOrguis_Hard_EnemyBundle";
                }
                public static class Irid
                {
                    public static string Med => Iridescent.Med;
                    public static string Hard => Iridescent.Hard;
                }
                public static class Clusterfuck
                {
                    public static string Med => "H_ZoneAbyss_ApatelosOrguis_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_ApatelosOrguis_Hard_EnemyBundle";
                }
                public static class Entropic
                {
                    public static string Med => "H_ZoneAbyss_PanopticOrguis_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_PanopticOrguis_Hard_EnemyBundle";
                }
                public static class White
                {
                    public static string Med => "H_ZoneAbyss_HamalatOrguis_Medium_EnemyBundle";
                    public static string Hard => "H_ZoneAbyss_HamalatOrguis_Hard_EnemyBundle";
                }
            }
            public static class BasicElemental
            {
                public static string Easy => "H_ZoneAbyss_BasicElemental_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_BasicElemental_Medium_EnemyBundle";
            }
            public static class BFElemental
            {
                public static string Easy => "H_ZoneAbyss_BFElemental_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_BFElemental_Medium_EnemyBundle";
            }
            public static class Aggregates
            {
            }
            public static class Crossword
            {
                public static string Easy => "H_ZoneAbyss_Crossword_Easy_EnemyBundle";
                public static string Med => "H_ZoneAbyss_Crossword_Medium_EnemyBundle";
            }

            //Toybox
            public static class YinimroG
            {
                public static string Med => "H_ZoneAbyss_GearYinimro_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_GearYinimro_Hard_EnemyBundle";
            }

            public static class YinimroP
            {
                public static string Med => "H_ZoneAbyss_ParadoxYinimro_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_ParadoxYinimro_Hard_EnemyBundle";
            }

            public static class Nowhere
            {
                public static string Med => "H_ZoneAbyss_NowhereMan_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_NowhereMan_Hard_EnemyBundle";
            }

            //Grace guys

            public static class Mime
            {
                public static string Med => "H_ZoneAbyss_Mime_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Mime_Hard_EnemyBundle";
            }

            public static class Kookoo
            {
                public static string Med => "H_ZoneAbyss_Kookoo_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Kookoo_Hard_EnemyBundle";
            }

            public static class Litany
            {
                public static string Med => "H_ZoneAbyss_Litany_Medium_EnemyBundle";
                public static string Hard => "H_ZoneAbyss_Litany_Hard_EnemyBundle";
            }
        }
    }
}
