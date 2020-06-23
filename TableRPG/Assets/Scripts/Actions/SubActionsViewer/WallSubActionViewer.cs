using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class WallSubActionViewer : SubActionViewer
    {
        #region UI

        public void OnGenerateWall()
        {
            Debug.Log("OnGenerateWall");
        }

        public void OnGenerateDoor()
        {
            Debug.Log("OnGenerateDoor");
        }

        #endregion
    }
}