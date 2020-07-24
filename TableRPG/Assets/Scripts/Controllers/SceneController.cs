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

            SetSceneInfo(info);
            SaveData();
        }

        private void SetSceneInfo(SceneInfo info)
        {
            this.sceneName = info.SceneName;
            this.BackgroundData = new BackgroundData(info.BackgroundTextureBytes, info.BackgroundPixelsPerUnit);
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
        }

        private void SaveData()
        {
            SceneData map = new SceneData(this);

            string json = JsonUtility.ToJson(map);

            if (!Directory.Exists(this.directoryPath))
            {
                Directory.CreateDirectory(this.directoryPath);
            }

            var completePath = $"{this.directoryPath}/{this.fileName}";

            Debugs.Log("Save map in Path:", completePath);

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
        private string sceneName;
        private byte[] backgroundTextureBytes;

        private float backgroundPixelsPerUnit;
        private bool valid;

        public SceneInfo(string _sceneName, byte[] _backgroundTextureBytes, string _backgroundPixelsPerUnit)
        {
            if (CheckValidScene(_sceneName, _backgroundTextureBytes, _backgroundPixelsPerUnit))
            {
                valid = false;
                return;
            }

            this.backgroundTextureBytes = _backgroundTextureBytes;
            this.backgroundPixelsPerUnit = float.Parse(_backgroundPixelsPerUnit);
            this.sceneName = _sceneName;

            valid = true;
        }

        public bool Valid
        {
            get { return this.valid; }
        }

        public string SceneName
        {
            get
            {
                return this.sceneName;
            }
        }

        public byte[] BackgroundTextureBytes
        {
            get
            {
                return this.backgroundTextureBytes;
            }
        }

        public float BackgroundPixelsPerUnit
        {
            get { return this.backgroundPixelsPerUnit; }
        }

        private bool CheckValidScene(string _sceneName, byte[] _backgroundTextureBytes, string _backgroundPixelsPerUnit)
        {
            Debugs.Log(_sceneName, _backgroundTextureBytes.Length, _backgroundPixelsPerUnit);

            return string.IsNullOrEmpty(_sceneName) || _backgroundTextureBytes == null || string.IsNullOrEmpty(_backgroundPixelsPerUnit);
        }
    }
}