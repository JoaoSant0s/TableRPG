using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
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
            if (CanHideLastSubAction())
            {
                this.lastSubActionControllerActived.HideAndDestroySubAction();
            }

            InvokeChangeWorldState(WorldState.WALL);
        }

        private void ActiveSubAction1()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest1);

            InvokeChangeWorldState(WorldState.TEST_1, isHidding);
        }

        private void ActiveSubAction2()
        {
            var isHidding = ToggleSubAction(this.subActionsCollections.subActionTest2);

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