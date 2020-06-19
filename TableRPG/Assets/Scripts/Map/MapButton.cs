using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TableRPG
{
    public class MapButton : MonoBehaviour
    {
        public delegate void OnClickMapButton(MapButton id);
        public static OnClickMapButton ClickMapButton;

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

        #region Getters And Setters

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        #endregion

        #region public methods

        public void OpenMenuButton()
        {
            this.animator.Play(this.openAnimation);
        }

        public void CloseMenuButton()
        {
            this.animator.Play(this.closeAnimation);
        }
        #endregion


        #region UI

        public void OnClick()
        {
            if (this.isOpened) return;
            this.isOpened = true;

            if (ClickMapButton == null) return;

            ClickMapButton(this);
        }

        public void OnLoadMap()
        {
            Debug.Log("OnLoadMap");
        }

        #endregion

        #region Animation

        public void SetOpenState()
        {
            this.isOpened = true;
        }

        public void SetCloseState()
        {
            this.isOpened = false;
        }
        #endregion


    }
}