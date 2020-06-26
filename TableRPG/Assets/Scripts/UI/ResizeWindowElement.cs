using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TableRPG
{
    public class ResizeWindowElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField]
        private WallUIInfo wallInfo;

        private Vector2 startPosition;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

            Vector3 newPosition = TransformPosition(eventData.position);
            this.wallInfo.SetReferenceSize();
            MouseConfig.SetResizeMouse();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

            this.startPosition = TransformPosition(eventData.position);            
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

            Vector2 newPosition = TransformPosition(eventData.position);

            var diff = this.startPosition - newPosition;
            this.wallInfo.ResizeWindow(diff);
        }
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

            MouseConfig.SetDefaultMouse();
        }

        protected Vector3 TransformPosition(Vector2 position)
        {
            Vector3 newPosition = position.ScreenToWorldPoint();
            newPosition.z = transform.position.z;
            return newPosition;
        }
    }
}