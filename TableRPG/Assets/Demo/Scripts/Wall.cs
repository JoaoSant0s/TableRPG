using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
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

    public void Scale(Vector3 direction)
    {
        var localScale = transform.localScale;
        localScale.x = direction.magnitude;
        transform.localScale = localScale;
    }
}
