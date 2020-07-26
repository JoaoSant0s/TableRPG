using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TableRPG
{

    public struct GridValues
    {
        public int gridType;
        public int gridDrawExtent;
        public int gridSize;
        public Vector2 gridOffset;

        public GridValues(GridData data)
        {
            this.gridType = data.GridType;
            this.gridDrawExtent = data.GridDrawExtent;
            this.gridSize = data.GridSize;
            this.gridOffset = data.GridOffset;
        }
    }

    public class GridController : MonoBehaviour
    {
        [Header("Line Render")]
        [SerializeField]
        private LineRenderer lindeRenderPrefab;
        private GridValues gridValues;
        private SceneController currentScene;

        #region Getters and Setters
        public Vector2 GridOffset
        {
            get { return this.gridValues.gridOffset; }
        }

        public int GridSize
        {
            get { return this.gridValues.gridSize; }
        }

        public int GridDrawExtent
        {
            get { return this.gridValues.gridDrawExtent; }
        }
        #endregion

        #region monobehaviour methods        

        private void Awake()
        {
            SceneManagerController.UpdateSceneContent += GenereteGrid;
        }

        private void OnDestroy()
        {
            SceneManagerController.UpdateSceneContent -= GenereteGrid;
        }

        #endregion

        #region public methods

        public void UpdateGrid(GridValues _gridValues)
        {
            this.gridValues = _gridValues;

            RenderGrid();
            SaveSceneData();
        }

        #endregion

        #region private methods

        private void SaveSceneData()
        {
            if (this.currentScene == null) return;

            this.currentScene.GridData.UpdateValues(this.gridValues);
            this.currentScene.SaveAllData();
        }

        private void GenereteGrid(SceneController scene = null)
        {
            this.currentScene = scene;

            this.gridValues = new GridValues(scene.GridData);

            RenderGrid();
        }

        private void ClearGrid()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void RenderGrid()
        {
            transform.localPosition = GridOffset;
            
            Debugs.Log("RenderGrid", GridOffset);
            ClearGrid();
            int lineCount = Mathf.RoundToInt((GridDrawExtent * 2) / GridSize);

            if (lineCount % 2 == 0) lineCount++;

            int halfLineCount = lineCount / 2;

            for (int i = 0; i < lineCount; i++)
            {
                int offset = i - halfLineCount;

                float xCoord = offset * GridSize;

                float yCoord0 = halfLineCount * GridSize;
                float yCoord1 = -yCoord0;

                Vector3 p0 = new Vector3(xCoord, yCoord0, 0f);
                Vector3 p1 = new Vector3(xCoord, yCoord1, 0f);

                var line = Instantiate(this.lindeRenderPrefab, transform);
                line.SetPosition(0, p0);
                line.SetPosition(1, p1);

                p0 = new Vector3(yCoord0, xCoord, 0f);
                p1 = new Vector3(yCoord1, xCoord, 0f);

                line = Instantiate(this.lindeRenderPrefab, transform);
                line.SetPosition(0, p0);
                line.SetPosition(1, p1);
            }
        }

        #endregion        
    }
}