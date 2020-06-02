using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class WallDragger : WallInteractor
{
    public void UpdateLocalPosition(float scale)
    {
        var localPosition = transform.localPosition;
        localPosition.x = scale / 2;
        transform.localPosition = localPosition;
    }
}
