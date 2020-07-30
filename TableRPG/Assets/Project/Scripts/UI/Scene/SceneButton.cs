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
            ScenePopupController.UpdateSceneName -= UpdateSceneName;
        }

        #endregion

        #region public methods   

        public virtual void Init()
        {
            SceneButton.LoadContent += EnableSelection;
            SceneManagerViewer.CreateSceneButton += EnableSelection;
            ScenePopupController.UpdateSceneName += UpdateSceneName;
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

        protected void UpdateSceneName(string sceneId, string sceneName)
        {
            if (!this.id.Equals(sceneId)) return;
            
            this.labelSceneName.text = sceneName;
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