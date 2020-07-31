using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class CameraVisual : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        [SerializeField]
        private CameraStatusValues statusValues;

        [SerializeField]
        private Color emptyColor;

        #region monoBehaviour methods

        private void Awake()
        {
            SceneManagerController.UpdateSceneContent += UpdateVisual;
            SceneManagerController.SceneDefaultContent += DefaultVisual;

            ScenePopupController.UpdateBackgrondColor += UpdateBackgrondColor;
        }

        private void OnDestroy()
        {
            SceneManagerController.UpdateSceneContent -= UpdateVisual;
            SceneManagerController.SceneDefaultContent -= DefaultVisual;

            ScenePopupController.UpdateBackgrondColor -= UpdateBackgrondColor;
        }

        #endregion

        private void UpdateBackgrondColor(Color color)
        {
            this.cam.backgroundColor = color;
        }

        private void UpdateVisual(SceneController scene = null)
        {
            var color = scene.BackgroundData.BackgroundColor;

            this.cam.backgroundColor = color;
        }

        private void DefaultVisual()
        {
            this.cam.backgroundColor = this.statusValues.emptyColor;
        }
    }

    [System.Serializable]
    public class CameraStatusValues
    {
        public Color sceneLoadedColor;

        public Color emptyColor;
    }
}
