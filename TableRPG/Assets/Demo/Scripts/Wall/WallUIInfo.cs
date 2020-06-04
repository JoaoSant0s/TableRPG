using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallUIInfo : MonoBehaviour
{
    public delegate void OnRemoveWall(Wall wall);
    public static OnRemoveWall RemoveWall;

    [SerializeField]
    private WallUIInfoDrag dragElement;

    [SerializeField]
    private Wall linkedWall;

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
        this.linkedWall = wall;
        Debug.Log(wall);
    }

    #region UI

    public void OnClosePanel()
    {
        if (RemoveWall != null)
        {
            RemoveWall(this.linkedWall);
        }
    }
    #endregion UI
}
