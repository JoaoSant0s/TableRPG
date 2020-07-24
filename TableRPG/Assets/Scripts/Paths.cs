using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class Paths
    {
        public static string Scenes
        {
            get { return $"{Base}/Scenes"; }
        }

        public static string Worlds
        {
            get { return $"{Base}/Worlds"; }
        }

        public static string WorldsCompletePath(string worldDirectory){
            return $"{Worlds}/{worldDirectory}";
        }

        public static string ScenesCompletePath(string worldDirectory)
        {
            return $"{Worlds}/{worldDirectory}/Scenes";
        }

        private static string Base
        {
            get { return Application.persistentDataPath; }
        }
    }
}