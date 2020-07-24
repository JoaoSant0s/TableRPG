using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class TogglePopupContent : MonoBehaviour
    {
        [SerializeField]
        private RectTransform content;
        private bool isOpened;

        #region Monobehaviour
        private void Start()
        {
            this.isOpened = true;
        }
        #endregion

        #region UI
        public void OnToggleContent()
        {
            this.isOpened = !this.isOpened;

            this.content.gameObject.SetActive(this.isOpened);
        }
        #endregion
    }
}