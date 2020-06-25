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
            MapManagerController.UpdateMapContent += UpdateVisual;
            MapManagerController.MapDefaultContent += DefaultVisual;
        }

        private void OnDestroy()
        {
            MapManagerController.UpdateMapContent -= UpdateVisual;
            MapManagerController.MapDefaultContent -= DefaultVisual;
        }

        #endregion

        private void UpdateVisual(MapController map = null)
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
