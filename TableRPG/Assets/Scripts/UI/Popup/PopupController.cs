using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class PopupController : WindowProperties
    {
        public delegate void OnClosePopup(PopupController popup);
        public static OnClosePopup ClosePopup;

        [Header("Popup Controller")]

        [SerializeField]
        private PopupDragArea dragElement;

        private Vector3 offset;

        private void Awake()
        {
            this.dragElement.PrepareToDragElement += SaveDragOffset;
            this.dragElement.DragElement += ChangeInfoPosition;
        }

        private void OnDestroy()
        {
            this.dragElement.PrepareToDragElement -= SaveDragOffset;
            this.dragElement.DragElement -= ChangeInfoPosition;
        }

        private void SaveDragOffset(Vector3 position)
        {
            this.offset = position - transform.position;
        }

        private void ChangeInfoPosition(Vector3 position)
        {
            transform.position = position - this.offset;
        }

        #region UI
        public void OnCloseButton()
        {
            if (ClosePopup != null) ClosePopup(this);
        }
        #endregion
    }
}