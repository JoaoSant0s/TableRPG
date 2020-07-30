using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TableRPG
{
    public class WorldButtonController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text worldName;

        public delegate void OnLoadWorld(string worldId);
        public static OnLoadWorld LoadWorld;
        private string worldId;

        public void Init(WorldConfigData worldConfig)
        {
            this.worldId = worldConfig.Id;
            this.worldName.text = worldConfig.WorldName;
        }

        #region UI

        public void OnClickWorldButton()
        {
            if (LoadWorld != null) LoadWorld(this.worldId);
        }

        #endregion
    }
}