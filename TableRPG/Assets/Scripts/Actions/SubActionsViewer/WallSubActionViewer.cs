using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class WallSubActionViewer : SubActionViewer
    {
        public delegate void OnChangeBuilder();
        public static OnChangeBuilder GenerateWall;
        public static OnChangeBuilder GenerateDoor;

        #region UI

        public void OnGenerateWall()
        {
            this.subActionsButtons.SelectButton(0, true);
            if (GenerateWall != null) GenerateWall();
        }

        public void OnGenerateDoor()
        {
            this.subActionsButtons.SelectButton(1, true);

            if (GenerateDoor != null) GenerateDoor();
        }

        #endregion
    }
}