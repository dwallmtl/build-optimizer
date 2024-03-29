﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using POEApi.Model;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System;

namespace Procurement
{
    public static class ApplicationState
    {
        public static string Version = "Procurement Libraries v1.2.0";
        public static POEModel Model = new POEModel();
        public static Dictionary<string, Stash> Stash = new Dictionary<string, Stash>();
        public static Dictionary<string, List<Item>> Inventory = new Dictionary<string, List<Item>>();
        public static List<Character> Characters = new List<Character>();
        public static List<string> Leagues = new List<string>();
        public static System.Drawing.Text.PrivateFontCollection FontCollection = new System.Drawing.Text.PrivateFontCollection();
        private static Character currentCharacter = null;

        public static event PropertyChangedEventHandler LeagueChanged;
        public static event PropertyChangedEventHandler CharacterChanged;
        public static  EventHandler EquippedGearChanged;

        private static string currentLeague = string.Empty;

        public static Character CurrentCharacter
        {
            get { return currentCharacter; }
            set
            {
                currentCharacter = value;
                if (CharacterChanged != null)
                    CharacterChanged(Model, new PropertyChangedEventArgs("CurrentCharacter"));
            }
        }

        public static string CurrentLeague
        {
            get { return currentLeague; }
            set
            {
                currentLeague = value;
                Characters = Model.GetCharacters().Where(c => c.League == value).ToList();
                CurrentCharacter = Characters.First();
                if (LeagueChanged != null)
                    LeagueChanged(Model, new PropertyChangedEventArgs("CurrentLeague"));
            }
        }

        public static void SetDefaults()
        {
            string favoriteLeague = Settings.UserSettings["FavoriteLeague"];
            if (!string.IsNullOrEmpty(favoriteLeague))
                CurrentLeague = favoriteLeague;

            string defaultCharacter = Settings.UserSettings["FavoriteCharacter"];
            if (defaultCharacter != string.Empty && Characters.Count(c => c.Name == defaultCharacter) == 1)
            {
                CurrentCharacter = Characters.First(c => c.Name == defaultCharacter);
                return;
            }

            CurrentCharacter = Characters.First();
            CurrentLeague = CurrentCharacter.League;
        }

        public static void InitializeFont(byte[] fontBytes)
        {
            IntPtr data = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, data, fontBytes.Length);
            FontCollection.AddMemoryFont(data, fontBytes.Length);
            Marshal.FreeCoTaskMem(data);
        }
    }

    public class GearChangedEventArgs : EventArgs
    {
        public bool CharacterChanged;
    }
}
