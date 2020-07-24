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

        #region monoBehaviour methods

        private void Awake()
        {
            SceneManagerController.UpdateSceneContent += UpdateVisual;
            SceneManagerController.SceneDefaultContent += DefaultVisual;
        }

        private void OnDestroy()
        {
            SceneManagerController.UpdateSceneContent -= UpdateVisual;
            SceneManagerController.SceneDefaultContent -= DefaultVisual;
        }

        #endregion

        private void UpdateVisual(SceneController map = null)
        {
            this.cam.backgroundColor = this.statusValues.mapLoadedColor;
        }

        private void DefaultVisual()
        {
            this.cam.backgroundColor = this.statusValues.emptyColor;
        }
    }

    [System.Serializable]
    public class CameraStatusValues
    {
        public Color mapLoadedColor;

        public Color emptyColor;
    }
}
