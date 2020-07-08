using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class Paths
    {
        public static string Scenes
        {
            get { return $"{Application.persistentDataPath}/Scenes"; }
        }
    }
}