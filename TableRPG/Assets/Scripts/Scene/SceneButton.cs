using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class SceneButton : MonoBehaviour
    {
        public delegate void OnClickSceneButton(SceneButton id);
        public static OnClickSceneButton ClickSceneButton;

        public delegate void OnLoadContent(string mapID);
        public static OnLoadContent LoadContent;

        public delegate void OnDeleteContent(string mapID);
        public static OnDeleteContent DeleteContent;

        [Header("Components")]

        [SerializeField]
        private Animator animator;

        [Header("Animation names")]

        [SerializeField]
        private string openAnimation;

        [SerializeField]
        private string closeAnimation;

        [SerializeField]
        private bool isOpened;
        private string id;

        #region public methods        

        public void ToggleMenuButton()
        {
            if (this.isOpened)
            {
                this.animator.Play(this.closeAnimation);
            }
            else
            {
                this.animator.Play(this.openAnimation);
            }

            this.isOpened = !this.isOpened;
        }

        public void SceneControllerId(string mapId)
        {
            this.id = mapId;
        }

        #endregion


        #region UI

        public void OnClick()
        {
            if (ClickSceneButton == null) return;

            ClickSceneButton(this);
        }

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