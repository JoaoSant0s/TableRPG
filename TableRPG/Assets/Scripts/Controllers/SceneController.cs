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
        private string id;
        private bool pinned;
        private string sceneName;

        private string fileName;
        private string directoryPath;

        public SceneController(SceneInfo info)
        {
            this.directoryPath = Paths.Scenes;

            var hashDifference = Random.Range(int.MinValue, int.MaxValue);
            this.id = $"{GetHashCode()}_{hashDifference}";

            this.fileName = $"Scene_{this.id}.dap";

            SetSceneInfo(info);
            SaveData();
        }

        private void SetSceneInfo(SceneInfo info)
        {
            this.sceneName = info.SceneName;
        }

        public SceneController(SceneData data)
        {
            this.directoryPath = Paths.Scenes;

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
        private bool valid;

        public SceneInfo(string _sceneName)
        {
            if (CheckValidScene(_sceneName))
            {
                valid = false;
                return;
            }

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

        private bool CheckValidScene(string _sceneName)
        {
            return string.IsNullOrEmpty(_sceneName);
        }
    }
}