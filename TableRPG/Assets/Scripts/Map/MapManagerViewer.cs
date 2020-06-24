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

        [Header("Controllers references")]
        [SerializeField]
        private MapManagerController mapManagerController;

        #region MonoBehaviour methods

        private void Awake()
        {
            MapButton.ClickMapButton += ToggleButtonsSelection;
            MapManagerController.CreateMapButton += CreateMapButton;
        }

        private void OnDestroy()
        {
            MapButton.ClickMapButton -= ToggleButtonsSelection;
            MapManagerController.CreateMapButton -= CreateMapButton;
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
            MapButton mapButton = CreateMap();
            MapController map = this.mapManagerController.Create();

            mapButton.MapControllerId(map.Id);
        }

        #endregion

        #region private methods
        private MapButton CreateMap()
        {
            MapButton mapButton = Instantiate(this.mapButtonPrefab, this.mapButtonArea, false);
            RefreshButtonPosition();
            return mapButton;
        }

        private void CreateMapButton(MapController map)
        {
            MapButton mapButton = CreateMap();

            mapButton.MapControllerId(map.Id);
        }

        #endregion
    }
}
