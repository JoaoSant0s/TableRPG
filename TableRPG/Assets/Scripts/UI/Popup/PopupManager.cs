using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class PopupManager : MonoBehaviour
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
        private void Awake()
        {
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
            else if (HotKeysCollections.ButtonSpace)
            {
                CrealteTestPopup();
            }
        }

        public T ShowPopup<T>() where T : PopupController
        {
            var popupPrefabs = this.popupPrefabs.Find(context => context is T) as T;
            var popup = Instantiate(popupPrefabs, this.popupsAreas);
            InstantiatedPopups.Add(popup);
            return popup;
        }

        private void CrealteTestPopup()
        {
            ShowPopup<PopupController>();
        }
        private bool CheckCloseLastPopupHotKey()
        {
            return Input.GetKeyUp(KeyCode.Escape);
        }

        private void RemoveSeletedPopup<T>(T popup) where T : PopupController
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