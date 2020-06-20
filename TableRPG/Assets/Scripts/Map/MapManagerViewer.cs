using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class MapManagerViewer : MonoBehaviour
    {
        [Header("Map elements")]
        [SerializeField]
        private MapButton mapButtonPrefab;

        [Header("Objects references")]
        [SerializeField]
        private RectTransform mapButtonArea;

        [SerializeField]
        private GameObject buttonCreateMap;

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
            mapButton.ToggleMenuButton();
        }

        private void RefreshButtonPosition()
        {
            this.buttonCreateMap.SetActive(false);
            Canvas.ForceUpdateCanvases();
            this.buttonCreateMap.SetActive(true);
        }

        #endregion

        #region UI

        public void OnCreateMap()
        {
            Instantiate(this.mapButtonPrefab, this.mapButtonArea, false);
            RefreshButtonPosition();
        }

        #endregion
    }
}
