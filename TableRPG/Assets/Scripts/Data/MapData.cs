﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [System.Serializable]
    public class MapData
    {
        [SerializeField]
        private int id;

        [SerializeField]
        private WallData wallData;

        public MapData(MapController controller)
        {
            this.id = controller.Id;
            this.wallData = controller.WallData;
        }

        #region getters and setters
        public int Id
        {
            get { return this.id; }
        }
        public WallData WallData
        {
            get { return this.wallData; }
        }
        #endregion
    }
}