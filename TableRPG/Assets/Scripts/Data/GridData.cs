using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    [System.Serializable]
    public class GridData
    {
        [SerializeField]
        private int gridType = 0;

        [SerializeField]
        private int gridDrawExtent = 100;

        [SerializeField]
        private int gridSize = 1;

        [SerializeField]
        private Vector2 gridOffset = new Vector2(0, 0);

        public GridData() { }

        public GridData(int _gridType, int _gridDrawExtent, int _gridSize, Vector2 _gridOffset)
        {
            this.gridType = _gridType;
            this.gridDrawExtent = _gridDrawExtent;
            this.gridSize = _gridSize;
            this.gridOffset = _gridOffset;
        }

        #region Getters and Setters

        public int GridType
        {
            get { return this.gridType; }
        }

        public int GridDrawExtent
        {
            get { return this.gridDrawExtent; }
        }

        public int GridSize
        {
            get { return this.gridSize; }
        }

        public Vector2 GridOffset
        {
            get { return this.gridOffset; }
        }

        #endregion

        #region publics methods

        public void UpdateValues(GridValues values)
        {
            this.gridType = values.gridType;
            this.gridDrawExtent = values.gridDrawExtent;
            this.gridSize = values.gridSize;
            this.gridOffset = values.gridOffset;
        }

        #endregion
    }
}