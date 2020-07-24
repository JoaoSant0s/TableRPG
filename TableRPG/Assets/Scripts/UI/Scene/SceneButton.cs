using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        protected Image background;

        [SerializeField]
        protected TextMeshProUGUI labelSceneName;

        protected string id;

        #region monoBehaviour methods        

        protected virtual void OnDestroy()
        {
            SceneButton.LoadContent -= EnableSelection;
            SceneManagerViewer.CreateSceneButton -= EnableSelection;
        }

        #endregion

        #region public methods   

        public virtual void Init()
        {
            SceneButton.LoadContent += EnableSelection;
            SceneManagerViewer.CreateSceneButton += EnableSelection;
        }

        public bool EqualsId(string idRef)
        {
            return this.id.Equals(idRef);
        }

        public void SetSceneController(SceneController scene)
        {
            this.id = scene.Id;            
            this.labelSceneName.text = scene.SceneName;
        }

        #endregion

        #region private methods                

        protected void EnableSelection(string mapId)
        {
            this.background.enabled = this.id.Equals(mapId);
        }

        #endregion


        #region UI        

        public void OnLoadScene()
        {
            if (LoadContent != null) LoadContent(this.id);
        }

        #endregion
    }
}