using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [CreateAssetMenu(fileName = "SubActionsPrefabCollections", menuName = "TableRPG/SubActionsPrefabCollections", order = 0)]
    public class SubActionsPrefabCollections : ScriptableObject
    {
        public SubActionController subActionWall;     
        public SubActionController subActionTest1;

        public SubActionController subActionTest2;
    }
}