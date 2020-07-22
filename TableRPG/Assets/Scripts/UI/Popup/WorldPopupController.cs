using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

namespace TableRPG
{
    public class WorldPopupController : PopupController
    {
        public delegate void OnCreateWorldDirectory();
        public static OnCreateWorldDirectory CreateWorldDirectory;

        [Header("General Info")]

        [SerializeField]
        private TMP_InputField worldName;

        #region UI

        public void OnCreateWorld()
        {
            var worldData = new WorldConfigData(this.worldName.text);

            if (!worldData.IsValid) return;

            var worldDirectoryPath = SaveWorldData(worldData);
            CreateBaseWorldDirectory(worldDirectoryPath);

            if (CreateWorldDirectory != null) CreateWorldDirectory();
            OnCloseButton();
        }

        #endregion

        #region private methods

        private string SaveWorldData(WorldConfigData worldData)
        {
            var directoryPath = Paths.WorldsCompletePath(worldData.DirectoryName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var completePath = $"{directoryPath}/Config.dap";

            Debugs.Log("Save map in Path:", completePath);

            var bf = new BinaryFormatter();

            FileStream file = File.Create(completePath);

            string json = JsonUtility.ToJson(worldData);

            bf.Serialize(file, json);

            file.Close();

            return directoryPath;
        }

        private void CreateBaseWorldDirectory(string worldDirectoryPath)
        {
            var scene = $"{worldDirectoryPath}/Scenes";

            if (!Directory.Exists(scene))
            {
                Directory.CreateDirectory(scene);
            }
        }

        #endregion
    }
}