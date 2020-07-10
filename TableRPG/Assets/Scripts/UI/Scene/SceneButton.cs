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
        protected Image background;

        protected string id;

        #region monoBehaviour methods        

        protected virtual void OnDestroy()
        {
            SceneButton.LoadContent -= EnableSelection;
        }

        #endregion

        #region public methods   

        public virtual void Init()
        {
            SceneButton.LoadContent += EnableSelection;
        }

        public bool EqualsId(string idRef)
        {
            return this.id.Equals(idRef);
        }

        public void SceneControllerId(string mapId)
        {
            this.id = mapId;
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