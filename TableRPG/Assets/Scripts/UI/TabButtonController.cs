using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TableRPG
{
    public class TabButtonController : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        #region public methods
        public void EnableBackgroundImage(bool value)
        {
            this.image.enabled = value;
        }
        #endregion
    }
}