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

        private string fileName;
        private string directoryPath;

        public SceneController()
        {
            this.directoryPath = Paths.Scenes;

            var hashDifference = Random.Range(int.MinValue, int.MaxValue);
            this.id = $"{GetHashCode()}_{hashDifference}";

            this.fileName = $"Scene_{this.id}.dap";
            SaveData();
        }

        public SceneController(SceneData data)
        {
            this.directoryPath = Paths.Scenes;

            this.id = data.Id;
            this.pinned = data.Pinned;

            this.wallData = data.WallData;
            this.fileName = $"Scene_{data.Id}.dap";
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
}