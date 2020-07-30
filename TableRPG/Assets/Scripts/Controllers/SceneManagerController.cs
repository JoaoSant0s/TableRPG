using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TableRPG
{
    public class SceneManagerController : SingletonBehaviour<SceneManagerController>
    {
        public delegate void OnUpdateSceneContent(SceneController scene = null);
        public static OnUpdateSceneContent UpdateSceneContent;
        public static OnUpdateSceneContent CreateSceneButton;

        public delegate void OnSceneDefaultContent();
        public static OnSceneDefaultContent SceneDefaultContent;

        private List<SceneController> scenes;

        private SceneController currentScene;

        #region monoBehaviour methods

        protected override void Awake()
        {
            base.Awake();
            SceneButton.LoadContent += LoadSceneContent;
            SceneButton.DeleteContent += DeleteSceneContent;
        }

        private void Start()
        {
            LoadSceneCollections();
        }

        private void OnDestroy()
        {
            SceneButton.LoadContent -= LoadSceneContent;
            SceneButton.DeleteContent -= DeleteSceneContent;
        }

        #endregion

        #region getters and setters

        public List<SceneController> SceneCollections
        {
            get
            {
                if (this.scenes == null)
                {
                    this.scenes = new List<SceneController>();
                }
                return this.scenes;
            }
        }

        public SceneController CurrentScene
        {
            get{
                return this.currentScene;
            }            
        }
        #endregion

        #region public methods

        public SceneController Create(SceneInfo info)
        {
            SceneController scene = new SceneController(info);

            SceneCollections.Add(scene);

            if (UpdateSceneContent != null) UpdateSceneContent(scene);

            return scene;
        }        

        public SceneController FindSceneById(string id)
        {
            SceneController scene = SceneCollections.Find(context => context.Id.Equals(id));

            return scene;
        }

        public void LoadSceneContent(SceneController scene)
        {
            if (this.currentScene == scene) return;

            this.currentScene = scene;
            PopupManager.Instance.CloseAllPopups();

            if (UpdateSceneContent != null) UpdateSceneContent(scene);
        }

        #endregion

        #region private methods        

        private void LoadSceneCollections()
        {
            if (!WorldManagerController.Instance) return;
            var directoryName = WorldManagerController.Instance.DirectoryName;

            var scenesPath = Paths.ScenesCompletePath(directoryName);

            var files = Directory.EnumerateFiles(scenesPath);

            foreach (var filePath in files)
            {
                LoadScene(filePath);
            }

            if (SceneDefaultContent != null) SceneDefaultContent();
        }

        private void LoadScene(string filePath)
        {
            Debugs.Log("Load Scene:", filePath);

            var sceneData = LoadSceneFromPath(filePath);

            SceneController scene = new SceneController(sceneData);

            SceneCollections.Add(scene);

            if (CreateSceneButton != null) CreateSceneButton(scene);
        }

        private SceneData LoadSceneFromPath(string filePath)
        {
            var bf = new BinaryFormatter();

            FileStream file = File.Open(filePath, FileMode.Open);

            var json = (string)bf.Deserialize(file);

            var sceneData = JsonUtility.FromJson<SceneData>(json);

            file.Close();
            return sceneData;
        }


        private void LoadSceneContent(string id)
        {
            SceneController scene = FindSceneById(id);

            LoadSceneContent(scene);
        }

        private void DeleteSceneContent(string id)
        {
            //TODO
        }

        #endregion
    }
}