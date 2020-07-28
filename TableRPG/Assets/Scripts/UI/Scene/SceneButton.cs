using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TableRPG
{
    public class SceneButton : MonoBehaviour
    {
        public delegate void OnLoadContent(string sceneID);
        public static OnLoadContent LoadContent;

        public delegate void OnDeleteContent(string sceneID);
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

        protected virtual void EnableSelection(string sceneId)
        {
            this.background.enabled = this.id.Equals(sceneId);
        }

        #endregion


        #region UI        

        public virtual void OnLoadScene()
        {
            if (LoadContent != null) LoadContent(this.id);
        }

        #endregion
    }
}