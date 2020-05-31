using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Transform wallTransform;

    [SerializeField]
    private Transform dragElement;

    public Vector3 Position
    {
        get { return this.transform.position; }
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

    private void UpdateDragPosition(float scale)
    {
        var localPosition = this.dragElement.localPosition;
        localPosition.x = scale / 2;
        this.dragElement.localPosition = localPosition;
    }
}
