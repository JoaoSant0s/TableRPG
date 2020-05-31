using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class WallDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public delegate void OnChangeWallPosition(Vector3 position);
    public OnChangeWallPosition ChangeWallPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        UpdatePosition(eventData.position);
    }

    public Vector3 LocalPosition
    {
        get { return this.transform.localPosition; }
    }

    private void UpdatePosition(Vector2 position)
    {
        Vector3 auxPositon = TransformPosition(position);
        auxPositon.z = transform.position.z;
        ChangeWallPosition(auxPositon);

        //transform.position = auxPositon;
    }

    private Vector3 TransformPosition(Vector2 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }

}
