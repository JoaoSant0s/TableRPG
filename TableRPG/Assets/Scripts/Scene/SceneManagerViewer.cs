using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class SceneManagerViewer : MonoBehaviour
    {

        [Header("Scene elements")]
        [SerializeField]
        private SceneButton sceneButtonPrefab;

        [Header("Objects references")]
        [SerializeField]
        private RectTransform sceneButtonArea;

        [Header("Controllers references")]
        [SerializeField]
        private SceneManagerController sceneManagerController;

        #region MonoBehaviour methods

        private void Awake()
        {
            SceneManagerController.CreateSceneButton += CreateSceneButton;
        }

        private void OnDestroy()
        {
            SceneManagerController.CreateSceneButton -= CreateSceneButton;
        }

        #endregion

        #region private methods        

        private void RefreshButtonPosition()
        {
            Canvas.ForceUpdateCanvases();
        }

        #endregion

        #region UI

        public void OnCreatScene()
        {
            SceneButton sceneButton = CreatScene();
            SceneController scene = this.sceneManagerController.Create();

            sceneButton.SceneControllerId(scene.Id);
        }

        #endregion

        #region private methods
        private SceneButton CreatScene()
        {
            SceneButton sceneButton = Instantiate(this.sceneButtonPrefab, this.sceneButtonArea, false);
            RefreshButtonPosition();
            return sceneButton;
        }

        private void CreateSceneButton(SceneController scene)
        {
            SceneButton sceneButton = CreatScene();

            sceneButton.SceneControllerId(scene.Id);
        }

        #endregion
    }
}
