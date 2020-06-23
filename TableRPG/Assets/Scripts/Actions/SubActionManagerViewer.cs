using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TableRPG
{
    [ExecuteAlways]
    public class SubActionManagerViewer : MonoBehaviour
    {
        [Header("External Componenets")]
        [SerializeField]
        private WorldController worldController;

        [Header("Sub Actions Collections")]
        [SerializeField]
        private SubActionsPrefabCollections subActionsCollections;

        [Header("Transform references")]
        [SerializeField]
        private RectTransform subActionsArea;

        [Header("Buttons")]
        [SerializeField]
        private SubActionsButtons subActionsButtons;

        private SubActionController lastSubActionControllerActived;

        #region MonoBehaviour methods

        private void Update()
        {
            if (CheckWallHotKeys())
            {
                ActiveWallAction();
            }
            else if (CheckAction1HotKeys())
            {
                ActiveSubAction1();
            }
            else if (CheckAction2HotKeys())
            {
                ActiveSubAction2();
            }
        }

        #endregion

        #region private methods

        private bool CheckWallHotKeys()
        {
            return Input.GetKeyUp(KeyCode.Alpha1);
        }

        private bool CheckAction1HotKeys()
        {
            return Input.GetKeyUp(KeyCode.Alpha2);
        }

        private bool CheckAction2HotKeys()
        {
            return Input.GetKeyUp(KeyCode.Alpha3);
        }

        private void ActiveWallAction()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionWall);
            this.subActionsButtons.SelectWallButton(isHidding);

            InvokeChangeWorldState(WorldState.WALL, isHidding);
        }

        private void ActiveSubAction1()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest1);
            this.subActionsButtons.SelectSubAction1Button(isHidding);

            InvokeChangeWorldState(WorldState.TEST_1, isHidding);
        }

        private void ActiveSubAction2()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest2);
            this.subActionsButtons.SelectSubAction2Button(isHidding);

            InvokeChangeWorldState(WorldState.TEST_2, isHidding);
        }

        private void InvokeChangeWorldState(WorldState state)
        {
            this.worldController.UpdateToState(state);
        }

        private void InvokeChangeWorldState(WorldState state, bool isHidding)
        {
            if (isHidding)
            {
                this.worldController.UpdateToState(state);
            }
            else
            {
                this.worldController.UpdateToState(WorldState.NONE);
            }
        }

        private bool ToggleSubAction(SubActionController comparebleSubAction)
        {
            if (CanHideLastSubAction())
            {
                this.lastSubActionControllerActived.HideAndDestroySubAction();

                if (this.lastSubActionControllerActived.Type == comparebleSubAction.Type) return false;
            }

            this.lastSubActionControllerActived = Instantiate(comparebleSubAction, this.subActionsArea, false);

            return true;
        }

        private bool CanHideLastSubAction()
        {
            return this.lastSubActionControllerActived != null;
        }

        #endregion

        #region UI

        public void OnActiveWallAction()
        {            
            ActiveWallAction();
        }

        public void OnActiveSubAction1()
        {            
            ActiveSubAction1();
        }

        public void OnActiveSubAction2()
        {            
            ActiveSubAction2();
        }
        #endregion
    }

    [System.Serializable]
    public class SubActionsButtons
    {
        [Header("Buttons images")]
        [SerializeField]
        public Image wallSelectImage;

        [SerializeField]
        public Image type1SelectImage;

        [SerializeField]
        public Image type2SelectImage;

        #region public methods        

        public void SelectWallButton(bool isHidding)
        {
            ActiveImageSelected(this.wallSelectImage, isHidding);
        }

        public void SelectSubAction1Button(bool isHidding)
        {
            ActiveImageSelected(this.type1SelectImage, isHidding);
        }

        public void SelectSubAction2Button(bool isHidding)
        {
            ActiveImageSelected(this.type2SelectImage, isHidding);
        }

        #endregion

        #region private methods       

        private void ActiveImageSelected(Image image, bool activing)
        {
            Image[] images = new Image[] { this.wallSelectImage, this.type1SelectImage, this.type2SelectImage };

            for (int i = 0; i < images.Length; i++)
            {
                Image comparableImage = images[i];

                comparableImage.enabled = (comparableImage == image && activing);
            }
        }

        #endregion
    }
}