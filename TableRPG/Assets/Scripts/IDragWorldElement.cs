using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IDragWorldElement : WindowCanvas, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public delegate void OnDragElement(Vector3 position);
    public OnDragElement DragElement;
    public OnDragElement PrepareToDragElement;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (PrepareToDragElement != null) PrepareToDragElement(newPosition);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (DragElement != null) DragElement(newPosition);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (DragElement != null) DragElement(newPosition);
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (DragElement != null) DragElement(newPosition);
    }

    protected Vector3 TransformPosition(Vector2 position)
    {
        position = CorrectInpuLimits(position);
        
        Vector3 newPosition = position.ScreenToWorldPoint();
        newPosition.z = transform.position.z;
        return newPosition;
    }
}
