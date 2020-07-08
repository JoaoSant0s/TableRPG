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

        #region MonoBehaviour methods

        private void Start()
        {
            SceneManagerController.UpdateSceneContent += HideSubActions;
            SceneManagerController.CreateSceneButton += HideSubActions;
        }

        private void Awake()
        {
            SceneManagerController.UpdateSceneContent -= HideSubActions;
            SceneManagerController.CreateSceneButton -= HideSubActions;
        }

        private void Update()
        {
            if (HotKeysCollections.ButtonAlpha1)
            {
                ActiveWallAction();
            }
            else if (HotKeysCollections.ButtonAlpha2)
            {
                ActiveSubAction1();
            }
            else if (HotKeysCollections.ButtonAlpha3)
            {
                ActiveSubAction2();
            }
        }

        #endregion

        #region private methods

        private void HideSubActions(SceneController map = null)
        {
            if (!CanHideLastSubAction()) return;

            this.lastSubActionControllerActived.HideAndDestroySubAction();

            this.subActionsButtons.DefaultStateButtons();
            InvokeChangeWorldState(WorldState.NONE, false);
        }

        private void ActiveWallAction()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionWall);
            this.subActionsButtons.SelectButton(0, isHidding);

            InvokeChangeWorldState(WorldState.WALL, isHidding);
        }

        private void ActiveSubAction1()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest1);
            this.subActionsButtons.SelectButton(1, isHidding);

            InvokeChangeWorldState(WorldState.TEST_1, isHidding);
        }

        private void ActiveSubAction2()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest2);
            this.subActionsButtons.SelectButton(2, isHidding);

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
}