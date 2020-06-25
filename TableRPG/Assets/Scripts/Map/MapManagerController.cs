using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TableRPG
{
    public class MapManagerController : MonoBehaviour
    {
        public delegate void OnUpdateMapContent(MapController map = null);
        public static OnUpdateMapContent UpdateMapContent;
        public static OnUpdateMapContent CreateMapButton;

        public delegate void OnMapDefaultContent();
        public static OnMapDefaultContent MapDefaultContent;

        public List<MapController> maps;

        #region monoBehaviour methods

        private void Awake()
        {
            MapButton.LoadContent += LoadMapContent;
            MapButton.DeleteContent += DeleteMapContent;
        }

        private void Start()
        {
            LoadMapCollections();            
        }

        private void OnDestroy()
        {
            MapButton.LoadContent -= LoadMapContent;
            MapButton.DeleteContent -= DeleteMapContent;
        }

        #endregion

        #region getters and setters

        public List<MapController> MapCollections
        {
            get
            {
                if (this.maps == null)
                {
                    this.maps = new List<MapController>();
                }
                return this.maps;
            }
        }
        #endregion

        #region public methods

        public MapController Create()
        {
            MapController map = new MapController();

            MapCollections.Add(map);

            if (UpdateMapContent != null) UpdateMapContent(map);

            return map;
        }

        #endregion

        #region private methods        

        private void LoadMapCollections()
        {
            var mapsPath = Paths.Maps;

            var files = Directory.EnumerateFiles(mapsPath);

            foreach (var filePath in files)
            {
                LoadMap(filePath);
            }

            if (MapDefaultContent != null) MapDefaultContent();
        }

        private void LoadMap(string filePath)
        {
            Debugs.Log("Load Map:", filePath);

            var mapData = LoadMapFromPath(filePath);

            MapController map = new MapController(mapData);

            MapCollections.Add(map);

            if (CreateMapButton != null) CreateMapButton(map);
        }

        private MapData LoadMapFromPath(string filePath)
        {
            var bf = new BinaryFormatter();

            FileStream file = File.Open(filePath, FileMode.Open);

            var json = (string)bf.Deserialize(file);

            var mapData = JsonUtility.FromJson<MapData>(json);

            file.Close();
            return mapData;
        }

        private MapController FindMapById(int id)
        {
            MapController map = MapCollections.Find(context => context.Id == id);

            return map;
        }

        private void LoadMapContent(int id)
        {
            MapController map = FindMapById(id);

            if (UpdateMapContent != null) UpdateMapContent(map);
        }

        private void DeleteMapContent(int id)
        {
            //TODO
        }

        #endregion
    }
}