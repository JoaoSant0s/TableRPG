using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TableRPG
{
    public class WallPopupController : PopupController
    {        
        [Header("Wall UI Config")]

        [SerializeField]
        private WallUIInfoButtons buttonsUI;

        private Wall linkedWall;

        private void UpdateButtonsVisuals()
        {
            this.buttonsUI.EnableBlockLightButton(this.linkedWall.EnableShadowCaster2D);
            this.buttonsUI.EnableColliderButton(this.linkedWall.EnableBoxCollider2D);
        }        

        public bool IsSameWall(Wall wall){
            return this.linkedWall == wall;
        }

        public void ExtractWallInfo(Wall wall)
        {
            this.linkedWall = wall;
            UpdateButtonsVisuals();
        }

        #region UI
        
        public void OnBlockLight()
        {
            if (this.linkedWall == null) return;

            this.linkedWall.EnableShadowCaster2D = !this.linkedWall.EnableShadowCaster2D;
            UpdateButtonsVisuals();
        }

        public void OnEnableCollider()
        {
            if (this.linkedWall == null) return;

            this.linkedWall.EnableBoxCollider2D = !this.linkedWall.EnableBoxCollider2D;
            UpdateButtonsVisuals();
        }

        public void OnWallVisible()
        {
            //TODO
        }       

        #endregion
    }

    [System.Serializable]
    public class WallUIInfoButtons
    {
        public Color disabledColor;

        public Button blockLightButton;
        public Button enableColliderButton;
        public Button enableButton;        

        public void EnableBlockLightButton(bool value)
        {
            this.blockLightButton.image.color = value ? Color.white : this.disabledColor;
        }

        public void EnableColliderButton(bool value)
        {
            this.enableColliderButton.image.color = !value ? Color.white : this.disabledColor;
        }
    }
}