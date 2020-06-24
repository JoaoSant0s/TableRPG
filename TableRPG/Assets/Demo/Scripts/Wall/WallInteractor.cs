using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallInteractor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public delegate void OnChangeWallPosition(Vector3 position);
    public OnChangeWallPosition ChangeWallPosition;
    public OnChangeWallPosition StartChangeWallPosition;

    public delegate void OnEndWallManipulation();
    public static OnEndWallManipulation EndWallManipulation;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (StartChangeWallPosition != null) StartChangeWallPosition(newPosition);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (ChangeWallPosition != null) ChangeWallPosition(newPosition);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (ChangeWallPosition != null) ChangeWallPosition(newPosition);
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (!UtilWrapper.CheckLeftButton(eventData.button)) return;

        Vector3 newPosition = TransformPosition(eventData.position);

        if (ChangeWallPosition != null) ChangeWallPosition(newPosition);        
    }

    protected Vector3 TransformPosition(Vector2 position)
    {
        Vector3 newPosition = position.ScreenToWorldPoint();
        newPosition.z = transform.position.z;
        return newPosition;
    }
}
