using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class MapManagerViewer : MonoBehaviour
    {

        [SerializeField]
        private MapButton mapButtonPrefab;

        private MapButton lastButtonMap;

        #region MonoBehaviour methods

        private void Awake()
        {
            MapButton.ClickMapButton += ToggleButtonsSelection;
        }

        private void OnDestroy()
        {
            MapButton.ClickMapButton -= ToggleButtonsSelection;
        }

        #endregion

        #region private methods

        private void ToggleButtonsSelection(MapButton mapButton)
        {
            if (this.lastButtonMap != null)
            {
                this.lastButtonMap.CloseMenuButton();
            }

            mapButton.OpenMenuButton();

            this.lastButtonMap = mapButton;
        }

        #endregion

        #region UI

        public void OnCreateMap()
        {

        }
        #endregion
    }
}
