using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector3 ApplyOffset(this Vector3 vector, Vector3 offset)
    {
        vector.x = (vector.x >= 0) ? vector.x + offset.x : vector.x - offset.x;
        vector.y = (vector.y >= 0) ? vector.y + offset.y : vector.y - offset.y;
        vector.z = (vector.z >= 0) ? vector.z + offset.z : vector.z - offset.z;

        return vector;
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        vector.x = Mathf.Abs(vector.x);
        vector.y = Mathf.Abs(vector.y);
        vector.z = Mathf.Abs(vector.z);
        return vector;
    }
}
