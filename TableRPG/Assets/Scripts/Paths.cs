using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class Paths
    {
        public static string Maps
        {
            get { return $"{Application.persistentDataPath}/Maps"; }
        }
    }
}