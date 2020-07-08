using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class SceneButton : MonoBehaviour
    {
        public delegate void OnLoadContent(string mapID);
        public static OnLoadContent LoadContent;

        public delegate void OnDeleteContent(string mapID);
        public static OnDeleteContent DeleteContent;
        private string id;

        #region public methods                

        public void SceneControllerId(string mapId)
        {
            this.id = mapId;
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