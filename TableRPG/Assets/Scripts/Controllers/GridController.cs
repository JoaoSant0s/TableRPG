using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TableRPG
{
    public class GridController : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField]
        private int gridDrawExtent = 25;

        [SerializeField]
        private int gridSize = 1;

        [SerializeField]
        private Vector2 gridOffset;

        [Header("Line Render")]
        [SerializeField]
        private LineRenderer lindeRenderPrefab;

        #region monobehaviour methods

        private void OnValidate()
        {
            this.gridDrawExtent = Mathf.Max(25, this.gridDrawExtent);
            this.gridSize = Mathf.Min(Mathf.Max(1, this.gridSize), this.gridDrawExtent);
            RenderGrid();
        }

        private void Start()
        {
            RenderGrid();
        }

        #endregion

        #region private methods

        private void ClearGrid()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void RenderGrid()
        {
            ClearGrid();
            int lineCount = Mathf.RoundToInt((gridDrawExtent * 2) / this.gridSize);

            if (lineCount % 2 == 0) lineCount++;

            int halfLineCount = lineCount / 2;

            for (int i = 0; i < lineCount; i++)
            {
                int offset = i - halfLineCount;

                float xCoord = offset * this.gridSize;

                float yCoord0 = halfLineCount * this.gridSize;
                float yCoord1 = -yCoord0;

                Vector3 p0 = new Vector3(xCoord + this.gridOffset.x, yCoord0 + this.gridOffset.y, 0f);
                Vector3 p1 = new Vector3(xCoord + this.gridOffset.x, yCoord1 + this.gridOffset.y, 0f);

                var line = Instantiate(this.lindeRenderPrefab, transform);
                line.SetPosition(0, p0);
                line.SetPosition(1, p1);

                p0 = new Vector3(yCoord0 + this.gridOffset.x, xCoord + this.gridOffset.y, 0f);
                p1 = new Vector3(yCoord1 + this.gridOffset.x, xCoord + this.gridOffset.y, 0f);

                line = Instantiate(this.lindeRenderPrefab, transform);
                line.SetPosition(0, p0);
                line.SetPosition(1, p1);
            }
        }

        #endregion        
    }
}