using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class SceneManagerViewer : MonoBehaviour
    {

        [Header("Scene elements")]
        [SerializeField]
        private SceneButton mapButtonPrefab;

        [Header("Objects references")]
        [SerializeField]
        private RectTransform mapButtonArea;

        [SerializeField]
        private GameObject buttonCreateScene;

        [Header("Controllers references")]
        [SerializeField]
        private SceneManagerController mapManagerController;

        #region MonoBehaviour methods

        private void Awake()
        {
            SceneButton.ClickSceneButton += ToggleButtonsSelection;
            SceneManagerController.CreateSceneButton += CreateSceneButton;
        }

        private void OnDestroy()
        {
            SceneButton.ClickSceneButton -= ToggleButtonsSelection;
            SceneManagerController.CreateSceneButton -= CreateSceneButton;
        }

        #endregion

        #region private methods

        private void ToggleButtonsSelection(SceneButton mapButton)
        {
            mapButton.ToggleMenuButton();
        }

        private void RefreshButtonPosition()
        {
            this.buttonCreateScene.SetActive(false);
            Canvas.ForceUpdateCanvases();
            this.buttonCreateScene.SetActive(true);
        }

        #endregion

        #region UI

        public void OnCreatScene()
        {
            SceneButton mapButton = CreatScene();
            SceneController map = this.mapManagerController.Create();

            mapButton.SceneControllerId(map.Id);
        }

        #endregion

        #region private methods
        private SceneButton CreatScene()
        {
            SceneButton mapButton = Instantiate(this.mapButtonPrefab, this.mapButtonArea, false);
            RefreshButtonPosition();
            return mapButton;
        }

        private void CreateSceneButton(SceneController map)
        {
            SceneButton mapButton = CreatScene();

            mapButton.SceneControllerId(map.Id);
        }

        #endregion
    }
}
