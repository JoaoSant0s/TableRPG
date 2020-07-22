using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class PopupManager : ContextBehaviour<PopupManager>
    {
        [SerializeField]
        private RectTransform popupsAreas;

        [SerializeField]
        private List<PopupController> popupPrefabs;

        private List<PopupController> instantiatedPopups;

        public List<PopupController> InstantiatedPopups
        {
            get
            {
                if (this.instantiatedPopups == null)
                {
                    this.instantiatedPopups = new List<PopupController>();
                }
                return this.instantiatedPopups;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            PopupController.ClosePopup += RemoveSeletedPopup;
        }

        private void OnDestroy()
        {
            PopupController.ClosePopup -= RemoveSeletedPopup;
        }

        private void Update()
        {
            if (HotKeysCollections.ButtonEsc)
            {
                RemoveLastPopup();
            }            
        }

        #region Scene region

        public void ShowScenePopup(){
            ShowPopup<ScenePopupController>();            
        }

        #endregion

        #region World region

        public void ShowWorldPopup(){
            ShowPopup<WorldPopupController>();
        }

        #endregion

        #region Wall region

        public void ShowWallPopup(Wall wall)
        {
            CloseWallPopup(wall);
            var wallPopup = ShowPopup<WallPopupController>();
            wallPopup.ExtractWallInfo(wall);
        }
        public void CloseAllWallPopup()
        {
            var popups = InstantiatedPopups.FindAll(context => context is WallPopupController);
            for (int i = 0; i < popups.Count; i++)
            {
                RemovePopup(popups[i]);
            }
        }

        private void CloseWallPopup(Wall wall)
        {
            var popups = InstantiatedPopups.FindAll(context => context is WallPopupController);
            for (int i = 0; i < popups.Count; i++)
            {
                var popup = (WallPopupController)popups[i];
                if (popup.IsSameWall(wall))
                {
                    RemovePopup(popup);
                }
            }
        }
        
        #endregion

        public T ShowPopup<T>() where T : PopupController
        {
            var popupPrefabs = GetPopupPrefab<T>();
            var popup = Instantiate(popupPrefabs, this.popupsAreas);
            InstantiatedPopups.Add(popup);
            return popup;
        }

        private T GetPopupPrefab<T>() where T : PopupController
        {
            var popupPrefab = this.popupPrefabs.Find(context => context is T) as T;
            return popupPrefab;
        }

        private void CrealteTestPopup()
        {
            ShowPopup<PopupController>();
        }        

        public void RemoveSeletedPopup<T>(T popup) where T : PopupController
        {
            if (InstantiatedPopups.Count == 0) return;

            RemovePopup(popup);
        }

        private void RemoveLastPopup()
        {
            if (InstantiatedPopups.Count == 0) return;

            var popup = InstantiatedPopups[InstantiatedPopups.Count - 1];
            RemovePopup(popup);
        }

        private void RemovePopup<T>(T popup) where T : PopupController
        {
            InstantiatedPopups.Remove(popup);
            //TODO change to call a close animation
            Destroy(popup.gameObject);
        }
    }
}