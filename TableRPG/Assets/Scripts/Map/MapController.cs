using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TableRPG
{
    public class MapController
    {
        private WallData wallData;
        private string id;

        private string fileName;
        private string directoryPath;

        public MapController()
        {
            this.directoryPath = Paths.Maps;

            var hashDifference = Random.Range(int.MinValue, int.MaxValue);
            this.id = $"{GetHashCode()}_{hashDifference}";

            this.fileName = $"Map_{this.id}.dap";
            SaveData();
        }

        public MapController(MapData data)
        {
            this.directoryPath = Paths.Maps;

            this.id = data.Id;

            this.wallData = data.WallData;
            this.fileName = $"Map_{data.Id}.dap";
        }

        #region getters and setters

        public string Id
        {
            get { return this.id; }
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

        #endregion

        #region private methods

        private void SaveData()
        {
            MapData map = new MapData(this);

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