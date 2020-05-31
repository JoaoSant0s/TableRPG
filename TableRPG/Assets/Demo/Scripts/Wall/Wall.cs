using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Transform wallTransform;

    [SerializeField]
    private WallDragger dragElement;

    public Vector3 Position
    {
        get { return this.transform.position; }
    }

    private void Awake()
    {
        WallBuilder.WallInteractions += EnableInteractions;
        this.dragElement.ChangeWallPosition += ChangeWallPosition;
        EnableInteractions(false);
    }

    private void OnDestroy()
    {
        this.dragElement.ChangeWallPosition -= ChangeWallPosition;
        WallBuilder.WallInteractions -= EnableInteractions;
    }

    private void EnableInteractions(bool enable)
    {
        this.dragElement.gameObject.SetActive(enable);
    }

    public void Rotate(Vector3 direction)
    {
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Scale(float scale)
    {
        var localScale = this.wallTransform.localScale;
        localScale.x = scale;
        this.wallTransform.localScale = localScale;

        UpdateDragPosition(scale);
    }

    public void ChangeWallPosition(Vector3 position)
    {
        // var absVector = this.dragElement.LocalPosition.Abs();
        // var transPosition = position.ApplyOffset(absVector);

        transform.position = position;
    }

    private void UpdateDragPosition(float scale)
    {
        var localPosition = this.dragElement.transform.localPosition;
        localPosition.x = scale / 2;
        this.dragElement.transform.localPosition = localPosition;
    }
}
