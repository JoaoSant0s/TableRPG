using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class WindowProperties : MonoBehaviour
    {
        [Header("Windows Config")]
        [SerializeField]
        private Vector2 minWindowSize = new Vector2(75, 75);
        [SerializeField]
        private float resizeMultiplier = 1f;

        private RectTransform rectTransform;

        private Vector2 referenceSize;


        public RectTransform RectTransform
        {
            get
            {
                if (this.rectTransform == null)
                {
                    this.rectTransform = (RectTransform)transform;
                }
                return this.rectTransform;
            }
        }

        #region public methods

        private void Start()
        {
            RectTransform.sizeDelta = this.minWindowSize;
        }

        public void SetReferenceSize()
        {
            this.referenceSize = RectTransform.sizeDelta;
        }
        public void ResizeWindow(Vector2 diff)
        {
            diff *= this.resizeMultiplier;

            var size = this.referenceSize;

            size.x = Mathf.Max(this.minWindowSize.x, this.referenceSize.x - diff.x);
            size.y = Mathf.Max(this.minWindowSize.y, this.referenceSize.y + diff.y);

            RectTransform.sizeDelta = size;
        }

        #endregion
    }
}