﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TableRPG
{
    public class WorldSelector : MonoBehaviour
    {
        [SerializeField]
        private WorldButtonController buttonPrefab;

        [SerializeField]
        private RectTransform menuButtonsArea;

        private const string sceneGame = "Game";

        #region MonoBehaviour

        private void Awake()
        {
            WorldButtonController.LoadWorld += LoadWorld;
            WorldPopupController.CreateWorldDirectory += RefreshWorldButtons;
        }

        private void OnDestroy()
        {
            WorldPopupController.CreateWorldDirectory -= RefreshWorldButtons;
            WorldButtonController.LoadWorld -= LoadWorld;
        }

        private void Start()
        {
            CreateWorldButtons();
        }

        #endregion

        #region private methods

        private void CreateWorldButtons()
        {
            var worldsPath = Paths.Worlds;

            if (!Directory.Exists(worldsPath))
            {
                Directory.CreateDirectory(worldsPath);
            }

            var worldsDirectories = Directory.EnumerateDirectories(worldsPath);

            foreach (var worldDirectory in worldsDirectories)
            {
                var completePath = $"{worldDirectory}/Config.dap";
                Debugs.Log(completePath);
                var worldConfig = LoadWorldFromPath(completePath);

                var button = Instantiate(this.buttonPrefab, this.menuButtonsArea);
                button.Init(worldConfig);
                WorldManagerController.Instance.AddWorldConfigData(worldConfig);
            }
        }

        private void RefreshWorldButtons()
        {
            CleanWorldButtons();
            CreateWorldButtons();
        }

        private void CleanWorldButtons()
        {
            foreach (Transform child in this.menuButtonsArea)
            {
                Destroy(child.gameObject);
            }
        }

        private WorldConfigData LoadWorldFromPath(string filePath)
        {
            var bf = new BinaryFormatter();

            FileStream file = File.Open(filePath, FileMode.Open);

            var json = (string)bf.Deserialize(file);

            var worldConfig = JsonUtility.FromJson<WorldConfigData>(json);

            file.Close();
            return worldConfig;
        }

        private void LoadWorld(string worldId)
        {
            CanvasLoadingController.Instance.EnableScene();
            SceneManager.LoadSceneAsync(sceneGame, LoadSceneMode.Single);
        }

        #endregion

        #region UI

        public void OnCreateWorld()
        {
            PopupManager.Instance.ShowWorldPopup();
        }

        #endregion
    }
}