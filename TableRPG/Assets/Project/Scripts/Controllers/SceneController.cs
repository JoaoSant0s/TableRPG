using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TableRPG
{
    public class SceneController
    {
        private WallData wallData;
        private BackgroundData backgroundData;
        private GridData gridData;
        private string id;
        private bool pinned;
        private string sceneName;

        private string fileName;
        private string directoryPath;

        public SceneController(SceneInfo info)
        {
            var directoryName = WorldManagerController.Instance.DirectoryName;

            this.directoryPath = Paths.ScenesCompletePath(directoryName);

            var hashDifference = Random.Range(int.MinValue, int.MaxValue);
            this.id = $"{GetHashCode()}_{hashDifference}";

            this.fileName = $"Scene_{this.id}.dap";

            CreateScene(info);
        }

        private void CreateScene(SceneInfo info)
        {
            this.sceneName = info.sceneName;
            this.BackgroundData = new BackgroundData(info.backgroundTextureBytes, info.backgroundPixelsPerUnit);
            this.GridData = new GridData(info.gridType, info.gridDrawExtent, info.gridSize, info.gridOffset);

            SaveData();
        }

        public void SetSceneInfo(SceneInfo info)
        {
            this.sceneName = info.sceneName;

            this.BackgroundData.UpdateValues(info.backgroundTextureBytes, info.backgroundPixelsPerUnit);
            this.GridData.UpdateValues(info.gridType, info.gridDrawExtent, info.gridSize, info.gridOffset);

            SaveData();
        }

        public SceneController(SceneData data)
        {
            var directoryName = WorldManagerController.Instance.DirectoryName;

            this.directoryPath = Paths.ScenesCompletePath(directoryName);

            this.id = data.Id;
            this.fileName = $"Scene_{data.Id}.dap";

            SetSceneContent(data);
        }

        #region getters and setters

        public string Id
        {
            get { return this.id; }
        }
        public bool Pinned
        {
            get { return this.pinned; }
            set { this.pinned = value; }
        }

        public string SceneName
        {
            get { return this.sceneName; }
        }

        public WallData WallData
        {
            get
            {
                if (this.wallData == null)
                {
                    this.wallData = new WallData();
                }
                return this.wallData;
            }

            set
            {
                this.wallData = value;
            }
        }

        public BackgroundData BackgroundData
        {
            get
            {
                if (this.backgroundData == null)
                {
                    this.backgroundData = new BackgroundData();
                }
                return this.backgroundData;
            }

            set
            {
                this.backgroundData = value;
            }
        }

        public GridData GridData
        {
            get
            {
                if (this.gridData == null)
                {
                    this.gridData = new GridData();
                }
                return this.gridData;
            }
            set
            {
                this.gridData = value;
            }
        }
        #endregion


        #region public methods

        public void UpdateWallData(WallData data)
        {
            this.wallData = data;

            SaveData();
        }

        public override string ToString()
        {
            return this.id + " " + this.wallData;
        }

        public void SaveAllData()
        {
            SaveData();
        }

        #endregion

        #region private methods    

        private void SetSceneContent(SceneData data)
        {
            this.pinned = data.Pinned;

            this.sceneName = (data.SceneName != null) ? data.SceneName : "Default";
            this.backgroundData = data.BackgroundData;
            this.wallData = data.WallData;
            this.gridData = data.GridData;
        }

        private void SaveData()
        {
            SceneData scene = new SceneData(this);

            string json = JsonUtility.ToJson(scene);

            if (!Directory.Exists(this.directoryPath))
            {
                Directory.CreateDirectory(this.directoryPath);
            }

            var completePath = $"{this.directoryPath}/{this.fileName}";

            Debugs.Log("Save scene in Path:", completePath);

            var bf = new BinaryFormatter();

            FileStream file = File.Create(completePath);

            bf.Serialize(file, json);

            file.Close();
        }

        #endregion
    }

    [System.Serializable]
    public class SceneInfo
    {
        public string sceneName;
        public byte[] backgroundTextureBytes;
        public float backgroundPixelsPerUnit;

        public int gridType;
        public int gridDrawExtent;
        public int gridSize = 1;
        public Vector2 gridOffset = new Vector2(0, 0);

        public SceneInfo(SceneValues values)
        {
            this.sceneName = values.sceneName;

            this.backgroundTextureBytes = values.backgroundTextureBytes;
            this.backgroundPixelsPerUnit = float.Parse(values.backgroundPixelsPerUnit);
            this.gridType = values.gridType;
            this.gridDrawExtent = int.Parse(values.gridExtent);
            this.gridSize = int.Parse(values.gridSize);

            this.gridOffset = new Vector2(float.Parse(values.gridOffsetX.Replace(".", ",")), float.Parse(values.gridOffsetY.Replace(".", ",")));
        }

        public static bool CheckInvalidScene(SceneValues values)
        {
            Debugs.Log(values.sceneName, values.backgroundTextureBytes, values.backgroundPixelsPerUnit);
            Debugs.Log(values.gridOffsetX, values.gridOffsetY, values.gridSize, values.gridExtent);

            var info = string.IsNullOrEmpty(values.sceneName) || values.backgroundTextureBytes == null || string.IsNullOrEmpty(values.backgroundPixelsPerUnit);

            var grid = string.IsNullOrEmpty(values.gridOffsetX) || string.IsNullOrEmpty(values.gridOffsetY) || string.IsNullOrEmpty(values.gridSize) || string.IsNullOrEmpty(values.gridExtent);

            Debugs.Log(info, grid);

            return info || grid;
        }
    }
}