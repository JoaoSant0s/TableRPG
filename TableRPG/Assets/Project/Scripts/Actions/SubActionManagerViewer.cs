using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private InputMenu control;

        #region MonoBehaviour methods        

        private void Awake()
        {
            this.control = new InputMenu();
        }
        private void Start()
        {
            SceneManagerController.UpdateSceneContent += HideSubActions;
            SceneManagerController.CreateSceneButton += HideSubActions;

            this.control.Menu.WallAction.performed += ctx => ActiveWallAction();
            this.control.Menu.OtherAction.performed += ctx => ActiveSubAction1();

            this.control.Menu.WallAction.Enable();
            this.control.Menu.OtherAction.Enable();
        }

        private void OnDestroy()
        {
            SceneManagerController.UpdateSceneContent -= HideSubActions;
            SceneManagerController.CreateSceneButton -= HideSubActions;

            this.control.Menu.WallAction.performed -= ctx => ActiveWallAction();
            this.control.Menu.OtherAction.performed -= ctx => ActiveSubAction1();
            
            this.control.Menu.WallAction.Disable();
            this.control.Menu.OtherAction.Disable();
        }

        #endregion

        #region private methods

        private void HideSubActions(SceneController scene = null)
        {
            if (!CanHideLastSubAction()) return;

            this.lastSubActionControllerActived.HideAndDestroySubAction();

            this.subActionsButtons.DefaultStateButtons();
            InvokeChangeWorldState(WorldState.NONE, false);
        }

        private void ActiveWallAction()
        {
            if (StaticState.InputFieldFocus) return;

            var isHidding = ToggleSubAction(this.subActionsCollections.subActionWall);
            this.subActionsButtons.SelectButton(0, isHidding);

            InvokeChangeWorldState(WorldState.WALL, isHidding);
        }

        private void ActiveSubAction1()
        {
            if (StaticState.InputFieldFocus) return;

            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest1);
            this.subActionsButtons.SelectButton(1, isHidding);

            InvokeChangeWorldState(WorldState.TEST_1, isHidding);
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
        #endregion
    }
}