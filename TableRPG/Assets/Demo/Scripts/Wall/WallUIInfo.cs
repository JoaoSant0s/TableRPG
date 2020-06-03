using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallUIInfo : MonoBehaviour
{
    [SerializeField]
    private WallUIInfoDrag dragElement;

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

    public void ExtractWallInfo(Wall wall)
    {
        Debug.Log(wall);
    }
}
