using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TableRPG
{
    public class SceneManagerController : MonoBehaviour
    {
        public delegate void OnUpdateSceneContent(SceneController map = null);
        public static OnUpdateSceneContent UpdateSceneContent;
        public static OnUpdateSceneContent CreateSceneButton;

        public delegate void OnSceneDefaultContent();
        public static OnSceneDefaultContent SceneDefaultContent;

        public List<SceneController> maps;

        private SceneController currentScene;

        #region monoBehaviour methods

        private void Awake()
        {
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
                if (this.maps == null)
                {
                    this.maps = new List<SceneController>();
                }
                return this.maps;
            }
        }
        #endregion

        #region public methods

        public SceneController Create()
        {
            SceneController map = new SceneController();

            SceneCollections.Add(map);

            if (UpdateSceneContent != null) UpdateSceneContent(map);

            return map;
        }

        public SceneController CurrentScene(){
            return this.currentScene;
        }

        public SceneController FindSceneById(string id)
        {
            SceneController map = SceneCollections.Find(context => context.Id.Equals(id));

            return map;
        }

        #endregion

        #region private methods        

        private void LoadSceneCollections()
        {
            var mapsPath = Paths.Scenes;

            var files = Directory.EnumerateFiles(mapsPath);

            foreach (var filePath in files)
            {
                LoadScene(filePath);
            }

            if (SceneDefaultContent != null) SceneDefaultContent();
        }

        private void LoadScene(string filePath)
        {
            Debugs.Log("Load Scene:", filePath);

            var mapData = LoadSceneFromPath(filePath);

            SceneController map = new SceneController(mapData);

            SceneCollections.Add(map);

            if (CreateSceneButton != null) CreateSceneButton(map);
        }

        private SceneData LoadSceneFromPath(string filePath)
        {
            var bf = new BinaryFormatter();

            FileStream file = File.Open(filePath, FileMode.Open);

            var json = (string)bf.Deserialize(file);

            var mapData = JsonUtility.FromJson<SceneData>(json);

            file.Close();
            return mapData;
        }


        private void LoadSceneContent(string id)
        {
            SceneController map = FindSceneById(id);

            if(this.currentScene == map) return;

            this.currentScene = map;

            if (UpdateSceneContent != null) UpdateSceneContent(map);
        }        

        private void DeleteSceneContent(string id)
        {
            //TODO
        }

        #endregion
    }
}