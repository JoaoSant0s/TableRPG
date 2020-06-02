using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallRotateScale : WallInteractor
{
    [SerializeField]
    private Vector3 offsetWall;

    private Vector3 offsetDrag;    

    public override void OnPointerDown(PointerEventData eventData)
    {
        Vector3 newPosition = TransformPosition(eventData.position);
        this.offsetDrag = Vector3.zero;

        if (StartChangeWallPosition != null) StartChangeWallPosition(newPosition);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 newPosition = TransformPosition(eventData.position);

        if (ChangeWallPosition != null) ChangeWallPosition(newPosition);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = TransformPosition(eventData.position);


        if (ChangeWallPosition != null) ChangeWallPosition(newPosition);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        Vector3 newPosition = TransformPosition(eventData.position);
        this.offsetDrag = this.offsetWall;

        if (ChangeWallPosition != null) ChangeWallPosition(newPosition);
    }

    public void UpdateElements(){
        this.offsetDrag = this.offsetWall;
    }

    public void UpdateLocalPosition(float scale)
    {
        var localPosition = transform.localPosition;
        localPosition.x = scale + this.offsetDrag.magnitude;
        transform.localPosition = localPosition;
    }
}
