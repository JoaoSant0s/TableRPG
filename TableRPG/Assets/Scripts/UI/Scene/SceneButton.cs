using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TableRPG
{
    public class SceneButton : MonoBehaviour
    {
        public delegate void OnLoadContent(string mapID);
        public static OnLoadContent LoadContent;

        public delegate void OnDeleteContent(string mapID);
        public static OnDeleteContent DeleteContent;

        [Header("Components")]
        [SerializeField]
        private Image background;

        private string id;

        #region monoBehaviour methods

        private void Awake()
        {
            SceneButton.LoadContent += EnableSelection;
        }

        private void OnDestroy()
        {
            SceneButton.LoadContent -= EnableSelection;
        }

        #endregion

        #region public methods                

        public void SceneControllerId(string mapId)
        {
            this.id = mapId;
        }

        #endregion

        #region private methods                

        public void EnableSelection(string mapId)
        {
            this.background.enabled = this.id.Equals(mapId);
        }

        #endregion


        #region UI        

        public void OnLoadScene()
        {
            if (LoadContent != null) LoadContent(this.id);
        }

        public void OnDeleteScene()
        {
            if (DeleteContent != null) DeleteContent(this.id);
        }

        #endregion
    }
}