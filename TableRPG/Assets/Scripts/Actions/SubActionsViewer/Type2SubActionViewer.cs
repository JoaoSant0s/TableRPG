using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class Type2SubActionViewer : SubActionViewer
    {
        #region UI
        public void OnClickGeneric()
        {
            this.subActionsButtons.SelectButton(0, true);
            Debugs.Log("OnClickGeneric");
        }
        #endregion
    }
}