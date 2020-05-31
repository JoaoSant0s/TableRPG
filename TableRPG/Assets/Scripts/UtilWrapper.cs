
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public static class UtilWrapper
{
    public static bool IsPointOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
