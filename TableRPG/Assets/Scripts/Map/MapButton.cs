using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class MapButton : MonoBehaviour
    {
        public delegate void OnClickMapButton(MapButton id);
        public static OnClickMapButton ClickMapButton;

        public delegate void OnLoadContent(int mapID);
        public static OnLoadContent LoadContent;

        public delegate void OnDeleteContent(int mapID);
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
        private int id;

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

        public void MapControllerId(int mapId)
        {
            this.id = mapId;
        }

        #endregion


        #region UI

        public void OnClick()
        {
            if (ClickMapButton == null) return;

            ClickMapButton(this);
        }

        public void OnLoadMap()
        {            
            if (LoadContent != null) LoadContent(this.id);
        }

        public void OnDeleteMap()
        {            
            if (DeleteContent != null) DeleteContent(this.id);
        }

        #endregion
    }
}