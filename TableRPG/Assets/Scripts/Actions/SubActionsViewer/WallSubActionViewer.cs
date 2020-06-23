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
            this.subActionsButtons.SelectButton(0, true);
            Debug.Log("OnGenerateWall");
        }

        public void OnGenerateDoor()
        {
            this.subActionsButtons.SelectButton(1, true);
            Debug.Log("OnGenerateDoor");
        }

        #endregion
    }
}